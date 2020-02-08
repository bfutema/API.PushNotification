using Newtonsoft.Json;
using System.Collections.Generic;
using API.PushNotification.Models.Response.Generic;

namespace API.PushNotification.Models.Response
{
    public class UserFCMPushResponseFailed : UserFCMPushResponse
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}