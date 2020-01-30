using System;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http.Filters;
using System.Collections.Generic;

using API.PushNotification.Util;
using API.PushNotification.Exceptions;

namespace API.PushNotification.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is BadRequestException)
            {
                var errors = new List<string>();

                string message = JsonConvert.SerializeObject(new
                {
                    message = actionExecutedContext.Exception.Message,
                    errors = errors,
                });

                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message),
                };

                throw new HttpResponseException(resp);
            }

            if (actionExecutedContext.Exception is NotFoundException)
            {
                string message = JsonConvert.SerializeObject(new
                {
                    message = actionExecutedContext.Exception.Message,
                });

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(message),
                };

                throw new HttpResponseException(resp);
            }
            
            if (actionExecutedContext.Exception is ConflictModelException)
            {
                var errors = Functions.FilterErrors<ConflictModelException>(actionExecutedContext);

                string message = JsonConvert.SerializeObject(new
                {
                    message = actionExecutedContext.Exception.Message,
                    errors = errors,
                });

                var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(message),
                };

                throw new HttpResponseException(resp);
            }

            if (actionExecutedContext.Exception is InternalServerErrorException)
            {
                string message = JsonConvert.SerializeObject(new
                {
                    message = actionExecutedContext.Exception.Message,
                });

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(message),
                };

                throw new HttpResponseException(resp);
            }

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                string message = JsonConvert.SerializeObject(new
                {
                    message = actionExecutedContext.Exception.Message,
                });

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(message),
                };

                throw new HttpResponseException(resp);
            }
        }
    }
}