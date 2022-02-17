using System.Net;

namespace SAM.Core.CQRS.Responses
{
    /// <summary>
    /// Success handler response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class OkHandlerResponse<TResponse> : HandlerResponse<TResponse> where TResponse : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkHandlerResponse<TResponse>"/> class.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="response"></param>
        public OkHandlerResponse(HttpStatusCode httpStatusCode, TResponse response)
            : base(httpStatusCode, response, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OkHandlerResponse<TResponse>"/> class.
        /// </summary>
        /// <param name="response"></param>
        public OkHandlerResponse(TResponse response)
            : base(HttpStatusCode.OK, response, null)
        { }
    }
}
