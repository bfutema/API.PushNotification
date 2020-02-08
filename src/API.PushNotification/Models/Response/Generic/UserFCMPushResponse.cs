using Newtonsoft.Json;

namespace API.PushNotification.Models.Response.Generic
{
    public class UserFCMPushResponse
    {
        [JsonProperty("idApp")]
        public int IdApp { get; set; }

        [JsonProperty("re")]
        public string Re { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}