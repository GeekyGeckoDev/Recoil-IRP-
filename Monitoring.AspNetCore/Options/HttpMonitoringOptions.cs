using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.AspNetCore.Options;

public sealed class HttpMonitoringOptions
{
    public string[] ExcludedPaths { get; set; } = [];

    public bool CaptureRequestBody { get; set; }

    public bool CaptureResponseBody { get; set; }

    public int MaxRequestBodySizeBytes { get; set; } = 16 * 1024;

    public int MaxResponseBodySizeBytes { get; set; } = 16 * 1024;

    public bool UseRegexForExcludedPaths { get; set; } = false;

    public int MaxRequestBodySize { get; set; } = 16 * 1024;

    public string[] ExcludedRequestHeaders { get; set; } =
    [
        "Authorization",
    "Cookie",
    "Set-Cookie"
    ];
}