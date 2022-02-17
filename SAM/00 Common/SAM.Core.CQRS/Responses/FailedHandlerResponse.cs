using System.Net;
using SAM.Core.CQRS.Errors;

namespace SAM.Core.CQRS.Responses
{
    /// <summary>
    /// Failed handler response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class FailedHandlerResponse<TResponse> : HandlerResponse<TResponse> where TResponse : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedHandlerResponse<TResponse>"/> class.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="errorResponse"></param>
        public FailedHandlerResponse(HttpStatusCode httpStatusCode, ErrorResponse errorResponse)
            : base(httpStatusCode, null, errorResponse)
        {
        }
    }
}
