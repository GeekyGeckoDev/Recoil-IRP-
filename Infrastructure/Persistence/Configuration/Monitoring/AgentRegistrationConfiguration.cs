using Domain.Entities.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration.Monitoring
{
    public class AgentRegistrationConfiguration
    : IEntityTypeConfiguration<AgentRegistration>
    {
        public void Configure(EntityTypeBuilder<AgentRegistration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.AgentVersion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.OperatingSystem)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(x => x.RegisteredAt)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne<MonitoredAsset>()
                .WithMany()
                .HasForeignKey(x => x.AssetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.AssetId);

            builder.HasIndex(x => new { x.AssetId, x.IsActive });
        }
    }
}
