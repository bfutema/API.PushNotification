using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Infra.Data.EntityConfig;

namespace Icomon.PushNotification.Infra.Data.Context
{
    public class PushNotificationContext : DbContext
    {
        public PushNotificationContext() : base("API.PushNotification") { }

        public DbSet<AppToken> AppTokens { get; set; }
        public DbSet<UserFCM> Usuarios { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(_ => _.Name == "Id")
                .Configure(_ => _.IsKey());

            modelBuilder.Properties<string>()
                .Configure(_ => _.HasColumnType("VARCHAR"));

            modelBuilder.Properties<string>()
                .Configure(_ => _.HasMaxLength(100));

            modelBuilder.Properties<DateTime>()
                .Configure(_ => _.HasColumnType("DATETIME"));

            // Tables Configurations
            modelBuilder.Configurations.Add(new UserFCMConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new AppTokenConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    entry.Property("DataAlteracao").CurrentValue = null;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
