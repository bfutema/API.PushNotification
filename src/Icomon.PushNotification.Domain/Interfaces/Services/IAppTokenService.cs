using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Services
{
    public interface IAppTokenService
    {
        Response Add(AppToken appToken);
        Response Update(AppToken appToken);
        Response Delete(int id);
        Response Find(int id);
    }
}
