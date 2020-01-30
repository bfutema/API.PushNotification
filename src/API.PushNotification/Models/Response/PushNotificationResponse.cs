using System.Collections.Generic;

namespace API.PushNotification.Models.Response
{
    public class PushNotificationResponse
    {
        public long multicast_id { get; set; }
        public long success { get; set; }
        public long failure { get; set; }
        public long canonical_ids { get; set; }
        public int MyProperty { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string message_id { get; set; }
    }
}