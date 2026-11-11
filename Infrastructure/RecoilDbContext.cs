using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure
{
    public class RecoilDbContext : DbContext
    {
        public RecoilDbContext(DbContextOptions<RecoilDbContext> options)
            : base(options)
        {

        }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<AlertConfig> AlertConfigs { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Evidence> Evidence { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public DbSet<IncidentImpactChange> IncidentImpactChanges { get; set; }

        public DbSet<IncidentTimelineEvent> IncidentTimelineEvents { get; set; }

        public DbSet<Playbook> Playbooks { get; set; }

        public DbSet<PlaybookStep> PlaybookSteps { get; set; }

        public DbSet<ResponseTask> ResponseTasks { get; set; }

        public DbSet<WebhookRequestLog> WebhookRequestLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evidence>()
                .Property(e => e.Metadata)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(
                        v, (JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
                );

            modelBuilder.Entity<Incident>()
              .Property(i => i.CustomFields)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                  v => JsonSerializer.Deserialize<Dictionary<string, string>>(
                      v, (JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
              );
        }



        public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
        {
            SetAuditTimestamps();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditTimestamps()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog)
                    continue;

                if (entry.State == EntityState.Added &&
                    entry.Properties.Any(p => p.Metadata.Name == "CreatedAt"))
                {
                    entry.Property("CreatedAt").CurrentValue = now;
                }

                if (entry.State is EntityState.Added or EntityState.Modified &&
                    entry.Properties.Any(p => p.Metadata.Name == "ModifiedAt"))
                {
                    entry.Property("ModifiedAt").CurrentValue = now;
                }
            }
        }

    }
}
