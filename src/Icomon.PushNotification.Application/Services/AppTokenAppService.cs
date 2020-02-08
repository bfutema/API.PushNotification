using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Application.Interfaces;
using Icomon.PushNotification.Domain.Interfaces.Services;

namespace Icomon.PushNotification.Application.Services
{
    public class AppTokenAppService : IAppTokenAppService
    {
        private readonly IAppTokenService _appTokenService;
        public AppTokenAppService(IAppTokenService appTokenService) { _appTokenService = appTokenService; }

        public Response Add(AppToken appToken)
        {
            return _appTokenService.Add(appToken);
        }

        public Response Update(AppToken appToken)
        {
            return _appTokenService.Update(appToken);
        }

        public Response Delete(int id)
        {
            return _appTokenService.Delete(id);
        }

        public Response Find(int id)
        {
            return _appTokenService.Find(id);
        }
    }
}
