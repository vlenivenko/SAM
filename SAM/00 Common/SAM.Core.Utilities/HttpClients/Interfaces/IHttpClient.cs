namespace SAM.Core.Utilities.HttpClients.Interfaces
{
    /// <summary>
    /// Very basic functionality for interacts with resourse using HTTP protocol
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Execute POST request
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<string> PostAsync(object obj);
    }
}
