using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Services
{
    public interface IUserFCMService
    {
        Response Add(UserFCM userFCM);
        Response Update(UserFCM userFCM);
        Response Find(int idApp, string re);
        Response Find(int id);
    }
}
