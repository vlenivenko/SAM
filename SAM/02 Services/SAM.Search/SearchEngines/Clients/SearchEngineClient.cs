using AutoMapper;
using Microsoft.Extensions.Logging;
using SAM.Core.Utilities.HttpClients;

namespace SAM.Search.SearchEngines.Clients
{
    /// <inheritdoc/>
    public abstract class SearchEngineClient : BaseHttpClient, ISearchEngineClient
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SearchEngineClient(
            IMapper mapper,
            ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Type of supported request
        /// </summary>
        public abstract Type RequestType { get; }

        /// <inheritdoc/>
        public async Task<string> SearchAsync(object request)
        {
            _logger.LogInformation($"Map request to specific engine request type. Request type - {RequestType.Name}.");
            var obj = _mapper.Map(request, typeof(object), RequestType);

            _logger.LogInformation($"Search is about to be started. Url - {Url}.");
            var result = await PostAsync(obj);
            _logger.LogInformation($"Search completed. Url - {Url}. Request type - {RequestType.Name}.");

            return result;
        }
    }
}
