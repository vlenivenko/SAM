using AutoMapper;
using Microsoft.Extensions.Logging;
using SAM.Search.SearchEngines.Requests;

namespace SAM.Search.SearchEngines.Clients
{
    public class SearchEnginesHttpClient2 : SearchEngineClient
    {
        public SearchEnginesHttpClient2(
            IMapper mapper,
            ILogger logger) : base(mapper, logger)
        {
        }

        public override string Url { get => "https://ptsv2.com/t/j713l-1645087166/post"; }

        public override Type RequestType => typeof(SearchEnginesRequest2);
    }
}