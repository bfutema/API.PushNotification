using System.Data.Entity.ModelConfiguration;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Infra.Data.EntityConfig
{
    public class AppTokenConfiguration : EntityTypeConfiguration<AppToken>
    {
        public AppTokenConfiguration()
        {
            ToTable("AppToken");

            HasKey(at => at.Id);

            Property(at => at.IdApp)
                .IsRequired();

            Property(at => at.AuthorizationFCM)
                .IsRequired()
                .HasColumnType("NVARCHAR(MAX)");
        }
    }
}
