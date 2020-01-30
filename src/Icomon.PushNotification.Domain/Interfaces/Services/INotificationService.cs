using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Response Add(Notification notification);
        Response Update(Notification notification);
        Response Delete(int id);
        Response Find(int id);
    }
}
