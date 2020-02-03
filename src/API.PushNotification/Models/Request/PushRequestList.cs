using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.PushNotification.Models.Request
{
    public class PushRequestList
    {
        //[Required(ErrorMessage = "é obrigatório.")]
        //[JsonProperty("authFCM")]
        //public string AuthorizationFCM { get; set; }

        [Required(ErrorMessage = "é obrigatório.")]
        [JsonProperty("users")]
        public List<UserFCMPushRequest> Users { get; set; }
    }
}