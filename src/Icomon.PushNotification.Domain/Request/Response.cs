namespace Icomon.PushNotification.Domain.Request
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public Response()
        {
            this.success = false;
            this.message = string.Empty;
            this.data = null;
        }
    }
}
