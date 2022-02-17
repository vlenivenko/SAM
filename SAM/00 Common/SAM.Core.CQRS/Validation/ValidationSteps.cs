namespace SAM.Core.CQRS.Validation
{
    /// <summary>
    /// Specific validation step
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    internal class ValidationSteps<TRequest, TResponse>
    {
        /// <summary>
        /// Validation function
        /// </summary>
        public Func<TRequest, Task<bool>> ValidationPredicate { get; set; }

        /// <summary>
        /// Returned status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Success response
        /// </summary>
        public TResponse SuccessResponse { get; set; }
    }
}
