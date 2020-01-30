using System;
using System.Web;
using System.Linq;
using System.Web.Http.Filters;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace API.PushNotification.Util
{
    public static class Functions
    {
        internal static List<Dictionary<string, string[]>> FilterErrors<TEntity>(HttpActionExecutedContext actionExecutedContext)
        {
            List<Dictionary<string, string[]>> errors = new List<Dictionary<string, string[]>>();

            var exp = Convert.ChangeType(actionExecutedContext.Exception, typeof(TEntity));
            var state = (ModelStateDictionary)exp.GetType().GetProperty("State").GetValue(exp, null);

            if (state != null)
            {
                for (int i = 0; i < state.Keys.Count; i++)
                {
                    var error = new Dictionary<string, string[]>();
                    List<string> errorsMessages = new List<string>();

                    var arrayKeys = (string)state.Keys.ToArray()[i];

                    if (arrayKeys.Contains("."))
                    {
                        //var className = arrayKeys.Contains(".") ? arrayKeys.Split('.')[0] : arrayKeys;

                        var attribute = arrayKeys.Contains(".") ? arrayKeys.Split('.')[1] : arrayKeys;
                        var modelStateCast = (ModelState)state.Values.ToArray()[i];

                        foreach (var errorException in modelStateCast.Errors)
                        {
                            errorsMessages.Add(errorException.ErrorMessage);
                        }

                        error.Add(attribute, errorsMessages.ToArray());
                        errors.Add(error);
                    }
                }
            }

            return errors;
        }
    }
}