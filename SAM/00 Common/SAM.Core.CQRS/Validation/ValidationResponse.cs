using SAM.Core.CQRS.Responses;

namespace SAM.Core.CQRS.Validation
{
    /// <summary>
    /// Reponses represents validation information
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationResponse<TResponse> where TResponse : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResponse<TResponse>"/> class.
        /// </summary>
        /// <param name="passedValidation"></param>
        /// <param name="response"></param>
        public ValidationResponse(bool passedValidation, HandlerResponse<TResponse> response)
        {
            PassedValidation = passedValidation;
            Response = response;
        }

        /// <summary>
        /// Flag indicates whether was passed or not
        /// </summary>
        public bool PassedValidation { get; set; }

        /// <summary>
        /// Validated response
        /// </summary>
        public HandlerResponse<TResponse> Response { get; set; }
    }
}
