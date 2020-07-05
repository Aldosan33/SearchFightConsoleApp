using System;
using System.Net.Http;
using System.Threading.Tasks;
using SearchFight.Common.Configuration;
using SearchFight.Common.Extensions;
using SearchFight.Domain.Bing;
using SearchFight.Services.Interfaces;

namespace SearchFight.Services.Implementations
{
    public class BingSearch : ISearch
    {
        public string ClientName => "MSN Search";
        private static readonly HttpClient _httpClient;

        static BingSearch()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.BingUrl),
                DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", ConfigurationManager.BingAPIKey } }
            };
        }

        public async Task<long> GetNumberOfResultsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            using (var response = await _httpClient.GetAsync($"?q={query}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentException("There was an error processing your request");
                }

                var result = await response.Content.ReadAsStringAsync();
                var bingResponse = result.DeserializeJson<BingResponse>();
                return long.Parse(bingResponse.WebPages.TotalEstimatedMatches);
            }
        }
    }
}