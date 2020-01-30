using Newtonsoft.Json;

namespace API.PushNotification.Models.Response
{
    public class NotificationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Titulo { get; set; }

        [JsonProperty("body")]
        public string Corpo { get; set; }

        [JsonIgnore]
        public string IdFCM { get; set; }

        [JsonIgnore]
        public bool Recebido { get; set; }

        [JsonProperty("icon")]
        public string Icone { get; set; }

        [JsonProperty("sound")]
        public string Som { get; set; }

        [JsonProperty("color")]
        public string Cor { get; set; }

        [JsonProperty("clickAction")]
        public string ClickAction { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}