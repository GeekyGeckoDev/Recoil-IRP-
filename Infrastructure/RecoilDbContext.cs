using Domain.Entities;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RecoilDbContext : IdentityDbContext<IdentityUser>
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



        public override int SaveChanges()
        {
            SetAuditTimestamps();
            return base.SaveChanges();
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
