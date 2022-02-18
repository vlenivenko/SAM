using System.Text;
using Newtonsoft.Json;
using SAM.Core.Utilities.HttpClients.Interfaces;

namespace SAM.Core.Utilities.HttpClients
{
    /// <inheritdoc/>
    public abstract class BaseHttpClient : IHttpClient
    {
        /// <summary>
        /// Destination url
        /// </summary>
        public abstract string Url { get; }

        /// <inheritdoc/>
        public async Task<string> PostAsync(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(Url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
