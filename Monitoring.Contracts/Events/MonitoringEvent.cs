using Monitoring.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;



using System.Text.Json.Serialization;

namespace Monitoring.Contracts.Events
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$eventType")]
    [JsonDerivedType(typeof(HttpRequestMonitoringEvent), "http-request")]
    [JsonDerivedType(typeof(ExceptionMonitoringEvent), "exception")]
    [JsonDerivedType(typeof(ApplicationLogMonitoringEvent), "application-log")]
    public abstract class MonitoringEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
    }
}
