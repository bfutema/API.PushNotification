using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.PushNotification.Models.Request
{
    public class PushRequest
    {
        [Required(ErrorMessage = "é obrigatório.")]
        [JsonProperty("notification")]
        public NotificationRequest Notification { get; set; }

        //[Required(ErrorMessage = "é obrigatório.")]
        //[JsonProperty("authFCM")]
        //public string AuthorizationFCM { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "apenas números.")]
        [JsonProperty("re")]
        public string Re { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [JsonProperty("idApp")]
        public int IdApp { get; set; }
    }
}