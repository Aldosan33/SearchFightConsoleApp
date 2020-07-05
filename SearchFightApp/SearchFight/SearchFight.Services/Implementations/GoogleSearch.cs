using System;
using System.Net.Http;
using System.Threading.Tasks;
using SearchFight.Common.Configuration;
using SearchFight.Common.Extensions;
using SearchFight.Domain.Google;
using SearchFight.Services.Interfaces;

namespace SearchFight.Services.Implementations
{
    public class GoogleSearch : ISearch
    {
        public string ClientName => "Google";
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _googleUrl;

        public GoogleSearch()
        {
            _googleUrl = ConfigurationManager.GoogleUrl.Replace("{0}", ConfigurationManager.GoogleKey)
                                                       .Replace("{1}", ConfigurationManager.GoogleCSEKey);
        }

        public async Task<long> GetNumberOfResultsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            using (var response = await _httpClient.GetAsync(_googleUrl.Replace("{2}", query)))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentException("There was an error processing your request");
                }

                var result = await response.Content.ReadAsStringAsync();
                var googleResponse = result.DeserializeJson<GoogleResponse>();
                return long.Parse(googleResponse.SearchInformation.TotalResults);
            }
        }
    }
}