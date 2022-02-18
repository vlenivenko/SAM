using AutoMapper;
using Microsoft.Extensions.Logging;
using SAM.Search.Enums;
using SAM.Search.SearchEngines.Clients;

namespace SAM.Search.SearchEngines
{
    public class SearchEngineFactory : ISearchEngineFactory
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SearchEngineFactory(
            IMapper mapper,
            ILogger<ISearchEngineFactory> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public ISearchEngineClient GetClient(MatchEngineType matchEngineType)
        {
            switch (matchEngineType)
            {
                case MatchEngineType.MatchEngine1:
                    return new SearchEnginesHttpClient1(_mapper, _logger);
                case MatchEngineType.MatchEngine2:
                    return new SearchEnginesHttpClient2(_mapper, _logger);
                default:
                    throw new NotImplementedException($"Engine client for engine type id {(int)matchEngineType} is not available.");
            }
        }
    }
}
