using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Repository
{
    public interface IAppTokenRepository
    {
        int Add(AppToken appToken);
        void Update(AppToken appToken);
        void Delete(int id);
        AppToken Find(int id);
    }
}
