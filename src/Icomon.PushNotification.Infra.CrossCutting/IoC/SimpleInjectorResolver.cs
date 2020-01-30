using SimpleInjector;
using SimpleInjector.Integration.Web;

using Icomon.PushNotification.Domain.Services;
using Icomon.PushNotification.Application.Services;
using Icomon.PushNotification.Application.Interfaces;
using Icomon.PushNotification.Domain.Interfaces.Services;
using Icomon.PushNotification.Domain.Interfaces.Repository;
using Icomon.PushNotification.Infra.Data.Repositories.Dapper;

namespace Icomon.PushNotification.Infra.CrossCutting.IoC
{
    public class SimpleInjectorResolver
    {
        public SimpleInjectorResolver(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            #region Application

            container.Register<IUserFCMAppService, UserFCMAppService>(Lifestyle.Scoped);
            container.Register<INotificationAppService, NotificationAppService>(Lifestyle.Scoped);

            #endregion

            #region Domain

            container.Register<IUserFCMService, UserFCMService>(Lifestyle.Scoped);
            container.Register<INotificationService, NotificationService>(Lifestyle.Scoped);

            #endregion

            #region Infra

            container.Register<IUserFCMRepository, UserFCMRepository>(Lifestyle.Scoped);
            container.Register<INotificationRepository, NotificationRepository>(Lifestyle.Scoped);

            #endregion
        }
    }
}
