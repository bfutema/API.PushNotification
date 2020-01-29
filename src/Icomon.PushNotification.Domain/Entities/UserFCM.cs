using Icomon.PushNotification.Domain.Entities.Generic;

namespace Icomon.PushNotification.Domain.Entities
{
    public class UserFCM : Entity
    {
        public int IdApp { get; set; }
        public string Re { get; set; }
        public string Token { get; set; }
    }
}
