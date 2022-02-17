namespace SAM.Core.CQRS.Validation.Interfaces
{
    /// <summary>
    /// Request validator
    /// </summary>
    /// <typeparam name="TRequest">Request</typeparam>
    /// <typeparam name="TResponse">Response</typeparam>
    public interface IRequestValidator<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        /// <summary>
        /// Validates the request
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <returns>Validator response</returns>
        Task<ValidationResponse<TResponse>> ValidateAsync(TRequest request);
    }
}
