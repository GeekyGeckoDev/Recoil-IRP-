using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Agent.Collectors.Abstractions
{
    public interface IEventCollector
    {
        string CollectorType { get; }

        //Task StartAsync(
        //    ChannelWriter<IncomingSecurityEvent> writer,
        //    CancellationToken cancellationToken);
    }
}
