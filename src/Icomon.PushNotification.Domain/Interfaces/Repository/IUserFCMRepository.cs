using Icomon.PushNotification.Domain.Entities;

namespace Icomon.PushNotification.Domain.Interfaces.Repository
{
    public interface IUserFCMRepository
    {
        int Add(UserFCM userFCM);
        void Update(UserFCM userFCM);
        UserFCM Find(int idApp, string re);
    }
}
