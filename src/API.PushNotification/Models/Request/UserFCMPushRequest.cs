using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using API.PushNotification.Validators.Attributes;

namespace API.PushNotification.Models.Request
{
    public class UserFCMPushRequest
    {
        [UserFCMIdAppValidationAttribute]
        public int IdApp { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [MinLength(6, ErrorMessage = "deve ter pelo menos {1} digítos")]
        [MaxLength(23, ErrorMessage = "deve ter no máximo {1} digítos")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "apenas números.")]
        public string Re { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [JsonProperty("notification")]
        public NotificationRequest Notification { get; set; }
    }
}