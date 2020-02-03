using Icomon.PushNotification.Domain.Entities.Generic;

namespace Icomon.PushNotification.Domain.Entities
{
    public class AppToken : Entity
    {
        public int IdApp { get; set; }
        public string AuthorizationFCM { get; set; }
    }
}
