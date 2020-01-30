using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Application.Interfaces;
using Icomon.PushNotification.Domain.Interfaces.Services;

namespace Icomon.PushNotification.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly INotificationService _notificationService;
        public NotificationAppService(INotificationService notificationService) { _notificationService = notificationService; }

        public Response Add(Notification notification)
        {
            return _notificationService.Add(notification);
        }

        public Response Update(Notification notification)
        {
            return _notificationService.Update(notification);
        }

        public Response Delete(int id)
        {
            return _notificationService.Delete(id);
        }

        public Response Find(int id)
        {
            return _notificationService.Find(id);
        }
    }
}
