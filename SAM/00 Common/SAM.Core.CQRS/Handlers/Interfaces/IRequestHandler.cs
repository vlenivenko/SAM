using SAM.Core.CQRS.Responses;

namespace SAM.Core.CQRS.Handlers.Interfaces
{
    /// <summary>
    /// Handler
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HandlerResponse<TResponse>> HandleAsync(TRequest request);
    }
}
