using System;
using System.Net.Http;
using System.Web.Http.ModelBinding;

namespace API.PushNotification.Exceptions
{
    public class ConflictModelException : Exception
    {
        public ModelStateDictionary State { get; set; }

        public ConflictModelException(string message, ModelStateDictionary state) : base (message)
        {
            this.State = state;
        }
    }
}