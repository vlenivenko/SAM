using System.Net;
using SAM.Core.CQRS.Errors;

namespace SAM.Core.CQRS.Responses
{
    /// <summary>
    /// Generic handler response
    /// </summary>
    public abstract class HandlerResponse<TSuccessResponse>
        where TSuccessResponse : class
    {
        protected HandlerResponse(HttpStatusCode httpStatusCode, TSuccessResponse successResponse, ErrorResponse errorResponse)
        {
            HttpStatusCode = httpStatusCode;
            SuccessResponse = successResponse;
            ErrorResponse = errorResponse;
        }

        /// <summary>
        /// Status code
        /// </summary>
        public readonly HttpStatusCode HttpStatusCode;

        /// <summary>
        /// Success response
        /// </summary>
        public TSuccessResponse SuccessResponse { get; }

        /// <summary>
        /// Error response
        /// </summary>
        public ErrorResponse ErrorResponse { get; }

        /// <summary>
        /// Represent success response suitable for handers
        /// </summary>
        /// <param name="successResponse"></param>
        /// <returns></returns>
        public static OkHandlerResponse<TSuccessResponse> Ok(TSuccessResponse successResponse) => new OkHandlerResponse<TSuccessResponse>(successResponse);

        /// <summary>
        /// Check whether status code is successful or not
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessStatusCode() => (int)HttpStatusCode == 200 || (int)HttpStatusCode == 201;
    }
}