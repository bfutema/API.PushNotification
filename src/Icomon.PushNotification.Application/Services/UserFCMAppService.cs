using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Application.Interfaces;
using Icomon.PushNotification.Domain.Interfaces.Services;

namespace Icomon.PushNotification.Application.Services
{
    public class UserFCMAppService : IUserFCMAppService
    {
        private readonly IUserFCMService _userFCMService;
        public UserFCMAppService(IUserFCMService userFCMService) { _userFCMService = userFCMService; }

        public Response Add(UserFCM userFCM)
        {
            return _userFCMService.Add(userFCM);
        }
        public Response Update(UserFCM userFCM)
        {
            return _userFCMService.Update(userFCM);
        }
        public Response Find(int idApp, string re)
        {
            return _userFCMService.Find(idApp, re);
        }
    }
}
