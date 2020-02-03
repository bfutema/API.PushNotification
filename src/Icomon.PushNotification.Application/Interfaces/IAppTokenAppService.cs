using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Application.Interfaces
{
    public interface IAppTokenAppService
    {
        Response Add(AppToken appToken);
        Response Update(AppToken appToken);
        Response Delete(int id);
        Response Find(int id);
    }
}
