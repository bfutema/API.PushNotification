using Newtonsoft.Json;

namespace API.PushNotification.Models.Request
{
    public class PushNotificationRequest
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("data")]
        public NotificationRequest Notification { get; set; }
    }
}