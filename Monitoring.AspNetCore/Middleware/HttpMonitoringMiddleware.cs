using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Monitoring.AspNetCore.Models;
using Monitoring.AspNetCore.Options;
using Monitoring.AspNetCore.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Monitoring.Contracts.Events;
using Monitoring.SDK.Events;

namespace Monitoring.AspNetCore.Middleware
{
    public class HttpMonitoringMiddleware
    {
     
        private readonly IMonitoringEventQueue _eventQueue;
        private readonly RequestDelegate _next;
        private readonly HttpMonitoringOptions _options;

        public HttpMonitoringMiddleware(
            RequestDelegate next,
            IOptions<HttpMonitoringOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        private static HttpRequestMonitoringEvent CreateMonitoringEvent(
    CapturedRequest request,
    CapturedResponse response,
    long durationMilliseconds)
        {
            return new HttpRequestMonitoringEvent
            {
                Method = request.Method,
                Path = request.Path,
                QueryString = request.QueryString,
                Host = request.Host,
                IpAddress = request.IpAddress,

                ResponseStatus = response.StatusCode,

                RequestBody = request.Body,
                ResponseBody = response.Body,

                RequestHeaders = request.Headers,
                ResponseHeaders = response.Headers,

                StartTime = request.StartTime,
                EndTime = response.FinishTime,

                DurationMilliseconds = durationMilliseconds
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (ShouldExclude(context))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            var requestLog = await LogRequestAsync(context);

            var responseLog = await LogResponseAsync(context);

            stopwatch.Stop();

            var monitoringEvent = CreateMonitoringEvent(
                requestLog,
                responseLog,
                stopwatch.ElapsedMilliseconds);

            // Next step: submit monitoringEvent to the local SDK event queue.
        }

        private bool ShouldExclude(HttpContext context)
        {
            var requestPath = context.Request.Path.Value ?? string.Empty;

            if (_options.ExcludedPaths.Length == 0)
                return false;

            foreach (var excludedPath in _options.ExcludedPaths)
            {
                if (string.IsNullOrWhiteSpace(excludedPath))
                    continue;

                if (_options.UseRegexForExcludedPaths)
                {
                    if (Regex.IsMatch(
                        requestPath,
                        excludedPath,
                        RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
                else if (requestPath.StartsWith(
                             excludedPath,
                             StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<CapturedRequest> LogRequestAsync(HttpContext context)
        {
            var request = context.Request;

            var capturedRequest = new CapturedRequest
            {
                Method = request.Method,
                Path = request.Path.Value ?? string.Empty,
                QueryString = request.QueryString.Value ?? string.Empty,
                Host = request.Host.Value,
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                StartTime = DateTimeOffset.UtcNow,

                Headers = request.Headers
                    .Where(header =>
                        !_options.ExcludedRequestHeaders.Contains(
                            header.Key,
                            StringComparer.OrdinalIgnoreCase))
                    .ToDictionary(
                        header => header.Key,
                        header => header.Value.ToString())
            };

            if (_options.CaptureRequestBody)
            {
                capturedRequest.Body = await CaptureRequestBodyAsync(context);
            }

            return capturedRequest;
        }

        private async Task<string?> CaptureRequestBodyAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.ContentLength is null or 0 ||
                request.ContentLength > _options.MaxRequestBodySize)
            {
                return null;
            }

            if (request.ContentType is null ||
                (!request.ContentType.Contains("application/json",
                    StringComparison.OrdinalIgnoreCase) &&
                 !request.ContentType.Contains("text/",
                    StringComparison.OrdinalIgnoreCase) &&
                 !request.ContentType.Contains("application/xml",
                    StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            request.EnableBuffering();

            using var reader = new StreamReader(
                request.Body,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return body;
        }

        private async Task<CapturedResponse> LogResponseAsync(HttpContext context)
        {
            var response = context.Response;
            var originalBody = response.Body;

            var captureStream = new ResponseCaptureStream(
                originalBody,
                _options.MaxResponseBodySizeBytes);

            try
            {
                response.Body = captureStream;

                await _next(context);

                var capturedBytes = captureStream.GetCapturedBytes();

                string? responseBody = null;

                if (IsTextContentType(response.ContentType))
                {
                    responseBody = Encoding.UTF8.GetString(capturedBytes);
                }

                return new CapturedResponse
                {
                    StatusCode = response.StatusCode,
                    Headers = response.Headers.ToDictionary(
                        header => header.Key,
                        header => header.Value.ToString()),
                    Body = responseBody,
                    FinishTime = DateTimeOffset.UtcNow
                };
            }
            finally
            {
                response.Body = originalBody;
                await captureStream.DisposeAsync();
            }
        }

        private static bool IsTextContentType(string? contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                return false;

            return contentType.StartsWith(
                       "text/",
                       StringComparison.OrdinalIgnoreCase)
                   || contentType.Contains(
                       "json",
                       StringComparison.OrdinalIgnoreCase)
                   || contentType.Contains(
                       "xml",
                       StringComparison.OrdinalIgnoreCase);
        }
    }


}
