using SAM.Search.Enums;
using SAM.Search.SearchEngines.Clients;

namespace SAM.Search.SearchEngines
{
    public interface ISearchEngineFactory
    {
        ISearchEngineClient GetClient(MatchEngineType matchEngineType);
    }
}
