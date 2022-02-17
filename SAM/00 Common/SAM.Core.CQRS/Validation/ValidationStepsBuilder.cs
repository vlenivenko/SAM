namespace SAM.Core.CQRS.Validation
{
    /// <summary>
    /// Builder responsible for validation rule setup
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationStepsBuilder<TRequest, TResponse>
    {
        // generic validation message
        private Func<TRequest, string> ErrorMessageFunction { get; set; } = _ => "Validation failed";

        // generic validation failed status code
        private int StatusCode { get; set; } = 400;

        // validation function
        private readonly Func<TRequest, Task<bool>> _validationPredicate;

        private TResponse SuccessResponse { get; set; }

        public ValidationStepsBuilder(Func<TRequest, Task<bool>> validationPredicate)
        {
            _validationPredicate = validationPredicate;
        }

        /// <summary>
        /// Set error message in case of failed validation
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public ValidationStepsBuilder<TRequest, TResponse> WithErrorMessage(string errorMessage)
        {
            ErrorMessageFunction = _ => errorMessage;
            return this;
        }

        /// <summary>
        /// Set status code in case of failed validation
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public ValidationStepsBuilder<TRequest, TResponse> WithStatusCode(int statusCode)
        {
            StatusCode = statusCode;
            return this;
        }

        internal ValidationSteps<TRequest, TResponse> Build(TRequest request)
        {
            return new ValidationSteps<TRequest, TResponse>
            {
                StatusCode = StatusCode,
                ValidationPredicate = _validationPredicate,
                ErrorMessage = ErrorMessageFunction(request),
                SuccessResponse = SuccessResponse,
            };
        }
    }
}