using System.Net;
using SAM.Core.CQRS.Errors;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;

namespace SAM.Core.CQRS.Validation
{
    /// <inheritdoc>
    public abstract class BaseValidator<TRequest, TResponse> : IRequestValidator<TRequest, TResponse> where TResponse : class where TRequest : class
    {
        private readonly List<ValidationStepsBuilder<TRequest, TResponse>> _validationSteps = new List<ValidationStepsBuilder<TRequest, TResponse>>();

        /// <summary>
        /// Validates the request based on provided rules in specific validator
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ValidationResponse<TResponse>> ValidateAsync(TRequest request)
        {
            foreach (var validationStep in _validationSteps.Select(a => a.Build(request)))
            {
                var success = await validationStep.ValidationPredicate(request);

                if (!success)
                {
                    if (validationStep.SuccessResponse != null)
                    {
                        return new ValidationResponse<TResponse>(false, new OkHandlerResponse<TResponse>((HttpStatusCode)validationStep.StatusCode, validationStep.SuccessResponse));
                    }

                    return new ValidationResponse<TResponse>(false, new FailedHandlerResponse<TResponse>((HttpStatusCode)validationStep.StatusCode, new ErrorResponse(validationStep.ErrorMessage)));
                }
            }
            return new ValidationResponse<TResponse>(true, null);
        }

        /// <summary>
        /// Validation rule method
        /// </summary>
        /// <param name="predicate">Validation function</param>
        /// <returns></returns>
        public ValidationStepsBuilder<TRequest, TResponse> Must(Func<TRequest, Task<bool>> predicate)
        {
            var builder = new ValidationStepsBuilder<TRequest, TResponse>(predicate);
            _validationSteps.Add(builder);
            return builder;
        }

        /// <summary>
        /// Validation rule method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ValidationStepsBuilder<TRequest, TResponse> Must(Func<TRequest, bool> predicate)
        {
            return Must(t => Task.FromResult(predicate(t)));
        }
    }
}