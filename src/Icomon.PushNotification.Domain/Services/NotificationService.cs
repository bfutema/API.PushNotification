using System;

using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Services;
using Icomon.PushNotification.Domain.Interfaces.Repository;

namespace Icomon.PushNotification.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository) { _notificationRepository = notificationRepository; }

        public Response Add(Notification notification)
        {
            Response res = new Response();

            try
            {
                int id = _notificationRepository.Add(notification);

                res.success = true;
                res.message = "Notificação cadastrada com sucesso!";
                res.data = id;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }

        public Response Update(Notification notification)
        {
            Response res = new Response();

            try
            {
                _notificationRepository.Update(notification);

                res.success = true;
                res.message = "Notificação marcada como entregue com sucesso!";
                res.data = null;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }

        public Response Delete(int id)
        {
            Response res = new Response();

            try
            {
                _notificationRepository.Delete(id);

                res.success = true;
                res.message = "Notificação excluída com sucesso!";
                res.data = null;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }

        public Response Find(int id)
        {
            Response res = new Response();

            try
            {
                var notification = _notificationRepository.Find(id);

                res.success = true;
                res.message = "Notificação encontrada!";
                res.data = notification;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }
    }
}
