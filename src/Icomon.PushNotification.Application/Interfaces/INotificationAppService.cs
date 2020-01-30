using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Application.Interfaces
{
    public interface INotificationAppService
    {
        Response Add(Notification notification);
        Response Update(Notification notification);
        Response Delete(int id);
        Response Find(int id);
    }
}
