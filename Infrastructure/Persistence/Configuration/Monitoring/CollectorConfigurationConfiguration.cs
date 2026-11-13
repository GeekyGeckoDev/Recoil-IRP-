using Domain.Entities.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration.Monitoring
{
    public class CollectorConfigurationConfiguration
    : IEntityTypeConfiguration<CollectorConfiguration>
    {
        public void Configure(EntityTypeBuilder<CollectorConfiguration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CollectorType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(100);

            builder.Property(x => x.IsEnabled)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.HasOne<AgentRegistration>()
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // An agent should only have one configuration
            // per collector type.
            builder.HasIndex(x => new { x.AgentId, x.CollectorType })
                .IsUnique();
        }
    }
}
