using Icomon.PushNotification.Domain.Entities.Generic;

namespace Icomon.PushNotification.Domain.Entities
{
    public class Notification : Entity
    {
        public string Titulo { get; set; }
        public string Corpo { get; set; }
        public string IdFCM { get; set; }
        public bool Recebido { get; set; }

        public string Icone { get; set; }
        public string Som { get; set; }
        public string Cor { get; set; }
        public string ClickAction { get; set; }
        public string Tag { get; set; }
        public string Link { get; set; }

        public string JsonData { get; set; }
    }
}
