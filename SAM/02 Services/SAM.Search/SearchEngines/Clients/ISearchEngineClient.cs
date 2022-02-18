namespace SAM.Search.SearchEngines.Clients
{
    /// <summary>
    /// Describes search client functionality
    /// </summary>
    public interface ISearchEngineClient
    {
        /// <summary>
        /// Search using provided object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<string> SearchAsync(object request);
    }
}
