using Newtonsoft.Json;
using API.PushNotification.Models.Response.Generic;

namespace API.PushNotification.Models.Response
{
    public class UserFCMPushResponseSuccess : UserFCMPushResponse
    {
        [JsonProperty("notification")]
        public NotificationResponse Notification { get; set; }
    }
}