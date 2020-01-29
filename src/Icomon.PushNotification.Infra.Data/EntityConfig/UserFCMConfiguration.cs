using System.Data.Entity.ModelConfiguration;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Infra.Data.EntityConfig
{
    public class UserFCMConfiguration : EntityTypeConfiguration<UserFCM>
    {
        public UserFCMConfiguration()
        {
            ToTable("UserFCM");

            HasKey(u => u.Id);

            Property(u => u.IdApp)
                .IsRequired();

            Property(u => u.Re)
                .IsRequired()
                .HasMaxLength(23);

            Property(u => u.Token)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
