using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using TerritoryLocator.Validation;

namespace TerritoryLocator.Base.Attributes
{
            /// <summary>
    /// Exception filter to catch all the API controller exceptions in a single place
    /// </summary>
    public class ApiControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// This filters exceptions in Web API controllers so that they return
        /// more appropriate HTTP Response error codes for some exception types.
        /// Also removes stack trace information from the HTTP response.
        /// For example: returning BadRequest for validation errors instead of 
        /// just InternalServer Error.
        /// InternalServer Errors are also logged to our exception logging
        /// infrastructure.
        /// </summary>
        /// <param name="context">Context of the exception</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            // at a later point in time. Currently, just checking if HttpContext.Current is available.
            if (HttpContext.Current == null) return;
            // For handling the IIS interference in Server Exceptions.
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;

            // Replace 500 errors with BadRequest for ValidationException
            if (context.Exception is ValidationException)
            {
                // Validation exception - create a BadRequest status code and include validation errors in response.
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, ((ValidationException)context.Exception).Errors);
            }
            else
            {
                // For all other exceptions, check whether it's a Forbidden response...
                var sc = HttpStatusCode.InternalServerError;
                var responseException = (HttpResponseException)context.Exception.InnerException;
                if (responseException != null)
                {
                    sc = responseException.Response.StatusCode;
                }

                context.Response = sc == HttpStatusCode.Forbidden ? context.Request.CreateResponse(HttpStatusCode.Forbidden, new List<string> { "Forbidden" }) : context.Request.CreateResponse(HttpStatusCode.InternalServerError, new List<string> { "InternalServerError - " + context.Exception.Message });
            }
        }
    }

}