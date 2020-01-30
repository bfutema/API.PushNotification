using System.Data.Entity.ModelConfiguration;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Infra.Data.EntityConfig
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            ToTable("Notification");

            HasKey(n => n.Id);

            Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(30);

            Property(n => n.Corpo)
                .IsRequired()
                .HasMaxLength(100);

            Property(n => n.IdFCM)
                .IsOptional()
                .HasMaxLength(150);

            Property(n => n.Recebido)
                .IsRequired()
                .HasColumnType("BIT");

            Property(n => n.Icone)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.Som)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.Cor)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.ClickAction)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.Tag)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.Link)
                .IsOptional()
                .HasMaxLength(100);

            Property(n => n.JsonData)
                .IsOptional()
                .HasColumnType("NVARCHAR(MAX)");
        }
    }
}
