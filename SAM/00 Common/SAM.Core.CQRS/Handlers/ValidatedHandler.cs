using System.Net;
using SAM.Core.CQRS.Errors;
using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;

namespace SAM.Core.CQRS.Handlers
{
    /// <summary>
    /// Represents handler with response to be validated
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class ValidatedHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TResponse : class
        where TRequest : class
    {
        private readonly IRequestValidator<TRequest, TResponse> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatedHandler<TRequest, TResponse>"/> class.
        /// </summary>
        /// <param name="validator">Specific validator</param>
        protected ValidatedHandler(IRequestValidator<TRequest, TResponse> validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Handler </returns>
        public async Task<HandlerResponse<TResponse>> HandleAsync(TRequest request)
        {
            try
            {
                var validationResponse = await _validator.ValidateAsync(request);
                if (!validationResponse.PassedValidation)
                {
                    return validationResponse.Response;
                }

                return await HandleValidatedRequestAsync(request);
            }
            catch (Exception ex)
            {
                // in future we can use logger here

                var errorResponse = new ErrorResponse(ex.Message);

                return new FailedHandlerResponse<TResponse>(HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Handles validation request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task<HandlerResponse<TResponse>> HandleValidatedRequestAsync(TRequest request);
    }
}