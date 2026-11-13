using Domain.Entities.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Persistence.Configuration.Monitoring
{
    public class SecurityEventConfiguration
    : IEntityTypeConfiguration<SecurityEvent>
    {
        public void Configure(EntityTypeBuilder<SecurityEvent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExternalEventId)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.EventType)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Source)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.OccurredAt)
                .IsRequired();

            builder.Property(x => x.ReceivedAt)
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasMaxLength(200);

            builder.Property(x => x.IpAddress)
                .HasMaxLength(45);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasOne<MonitoredAsset>()
                .WithMany()
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AgentRegistration>()
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate ingestion from the same agent.
            builder.HasIndex(x => new { x.AgentId, x.ExternalEventId })
                .IsUnique();

            builder.HasIndex(x => x.OccurredAt);

            builder.HasIndex(x => new { x.AssetId, x.OccurredAt });

            builder.Property(x => x.Metadata)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<Dictionary<string, object>>(
                        v,
                        JsonSerializerOptions.Default
                    ) ?? new Dictionary<string, object>()
                )
                .HasColumnType("nvarchar(max)");
        }
    }
}
