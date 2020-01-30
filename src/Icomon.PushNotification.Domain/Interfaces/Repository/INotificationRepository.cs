using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Repository
{
    public interface INotificationRepository
    {
        int Add(Notification notification);
        void Update(Notification notification);
        void Delete(int id);
        Notification Find(int id);
    }
}
