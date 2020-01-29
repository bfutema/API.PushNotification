using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Application.Interfaces
{
    public interface IUserFCMAppService
    {
        Response Add(UserFCM userFCM);
        Response Update(UserFCM userFCM);
        Response Find(int idApp, string re);
    }
}
