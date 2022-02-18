using AutoMapper;
using Microsoft.Extensions.Logging;
using SAM.Search.SearchEngines.Requests;

namespace SAM.Search.SearchEngines.Clients
{
    public class SearchEnginesHttpClient1 : SearchEngineClient
    {
        public SearchEnginesHttpClient1(
            IMapper mapper,
            ILogger logger) : base(mapper, logger)
        {
        }

        public override string Url { get => "https://ptsv2.com/t/n6e6r-1645087132/post"; }

        public override Type RequestType => typeof(SearchEnginesRequest1);
    }
}