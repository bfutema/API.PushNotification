using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.PushNotification.Models.Request
{
    public class NotificationRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [MaxLength(30, ErrorMessage = "deve ter no máximo {1} digítos")]
        [JsonProperty("title")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [MaxLength(150, ErrorMessage = "deve ter no máximo {1} digítos")]
        [JsonProperty("body")]
        public string Corpo { get; set; }

        public string IdFCM { get; set; }
        public bool Status { get; set; }

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

        [JsonProperty("payload")]
        public object JsonData { get; set; }
    }
}