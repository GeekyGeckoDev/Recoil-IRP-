using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Monitoring.Contracts.Events;
using Monitoring.SDK.Events;
using Monitoring.SDK.Options;
using Monitoring.SDK.Queues;
using Monitoring.SDK.Uploading;

namespace Monitoring.SDK.Background
{
    namespace Monitoring.SDK.Background
    {
        public class MonitoringEventProcessor : BackgroundService
        {
            private readonly IMonitoringEventQueue _eventQueue;
            private readonly IMonitoringEventUploader _eventUploader;
            private readonly MonitoringSdkOptions _options;
            private readonly ILogger<MonitoringEventProcessor> _logger;

            public MonitoringEventProcessor(
                IMonitoringEventQueue eventQueue,
                IMonitoringEventUploader eventUploader,
                IOptions<MonitoringSdkOptions> options,
                ILogger<MonitoringEventProcessor> logger)
            {
                _eventQueue = eventQueue;
                _eventUploader = eventUploader;
                _options = options.Value;
                _logger = logger;
            }




            protected override async Task ExecuteAsync(
                CancellationToken stoppingToken)
            {
                var batch = new List<HttpRequestMonitoringEvent>();

                await using var enumerator = _eventQueue
                    .ReadAllAsync(stoppingToken)
                    .GetAsyncEnumerator(stoppingToken);

                Task<bool>? readTask = null;
                Task? batchTimerTask = null;
                CancellationTokenSource? batchTimerCts = null;

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        readTask ??= enumerator.MoveNextAsync().AsTask();

                        // Only start waiting for the timer after the first event.
                        var completedTask = batchTimerTask is null
                            ? await Task.WhenAny(readTask)
                            : await Task.WhenAny(readTask, batchTimerTask);

                        if (completedTask == readTask)
                        {
                            if (!await readTask)
                                break;

                            batch.Add(enumerator.Current);
                            readTask = null;

                            // Start the timer when the first event enters the batch.
                            if (batch.Count == 1)
                            {
                                batchTimerCts = CancellationTokenSource
                                    .CreateLinkedTokenSource(stoppingToken);

                                batchTimerTask = Task.Delay(
                                    _options.UploadInterval,
                                    batchTimerCts.Token);
                            }

                            if (batch.Count >= _options.BatchSize)
                            {
                                await ProcessBatchAsync(batch, stoppingToken);
                                batch.Clear();

                                batchTimerCts?.Cancel();
                                batchTimerCts?.Dispose();

                                batchTimerCts = null;
                                batchTimerTask = null;
                            }
                        }
                        else
                        {
                            // The batch timer expired.
                            await batchTimerTask!;

                            await ProcessBatchAsync(batch, stoppingToken);
                            batch.Clear();

                            batchTimerCts?.Dispose();
                            batchTimerCts = null;
                            batchTimerTask = null;
                        }
                    }
                }
                finally
                {
                    batchTimerCts?.Cancel();
                    batchTimerCts?.Dispose();
                }
            }


            private async Task ProcessBatchAsync(
                List<HttpRequestMonitoringEvent> batch,
                CancellationToken cancellationToken)
            {
                var uploaded = await UploadWithRetryAsync(
                    batch,
                    cancellationToken);

                if (!uploaded)
                {
                    _logger.LogError(
                        "Monitoring batch of {EventCount} events was discarded after {MaxAttempts} failed upload attempts.",
                        batch.Count,
                        _options.MaxRetryAttempts);
                }
            }


            private async Task<bool> UploadWithRetryAsync(
                List<HttpRequestMonitoringEvent> batch,
                CancellationToken cancellationToken)
            {
                for (int attempt = 1;
                     attempt <= _options.MaxRetryAttempts;
                     attempt++)
                {
                    try
                    {
                        await _eventUploader.UploadBatchAsync(
                            batch,
                            cancellationToken);

                        _logger.LogDebug(
                            "Successfully uploaded monitoring batch containing {EventCount} events.",
                            batch.Count);

                        return true;
                    }
                    catch (OperationCanceledException)
                        when (cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                    catch (Exception exception)
                    {
                        _logger.LogWarning(
                            exception,
                            "Monitoring batch upload failed. Attempt {Attempt}/{MaxAttempts}.",
                            attempt,
                            _options.MaxRetryAttempts);

                        if (attempt == _options.MaxRetryAttempts)
                            return false;

                        var delay = CalculateRetryDelay(attempt);

                        await Task.Delay(delay, cancellationToken);
                    }
                }

                return false;
            }


            private TimeSpan CalculateRetryDelay(int attempt)
            {
                var multiplier = Math.Pow(2, attempt - 1);

                return TimeSpan.FromMilliseconds(
                    _options.InitialRetryDelay.TotalMilliseconds * multiplier);
            }



        }


    }
}
