using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Common.Extensions;
using SearchFight.Core.Interfaces;
using SearchFight.Domain.Results;
using SearchFight.Services.Interfaces;

namespace SearchFight.Core.Implementations
{
    public class SearchManager : ISearchManager
    {
        private readonly IEnumerable<ISearch> _searchClients;
        private readonly StringBuilder _outputMessage;

        public SearchManager(IEnumerable<ISearch> searchClients)
        {
            _searchClients = searchClients;
            _outputMessage = new StringBuilder();
        }

        public async Task<string> GetSearchReportAsync(List<string> queries)
        {
            var distinctQueries = queries.Distinct();

            var searchResults = await GetSearchResultsAsync(distinctQueries);

            var groupedResults = GetGroupedResults(searchResults);

            foreach (var group in groupedResults)
            {
                var results = group.Select(client => $"{client.SearchClient}: {client.TotalResults}");
                _outputMessage.AppendLine($"{group.Key}: {string.Join(" ", results)}");
            }

            var winners = GetWinnersPerClient(searchResults);

            foreach (var winner in winners)
            {
                _outputMessage.AppendLine($"{winner.ClientName} winner: {winner.Query}");
            }

            var totalWinner = GetTotalWinner(searchResults);
            _outputMessage.AppendLine($"Total winner: {totalWinner}");

            return _outputMessage.ToString();
        }

        public async Task<List<SearchResult>> GetSearchResultsAsync(IEnumerable<string> queries)
        {
            var results = new List<SearchResult>();

            foreach (var query in queries)
            {
                foreach (var client in _searchClients)
                {
                    results.Add(new SearchResult
                    {
                        SearchClient = client.ClientName,
                        Query = query,
                        TotalResults = await client.GetNumberOfResultsAsync(query)
                    });
                }
            }
            return results;
        }

        public IEnumerable<Winner> GetWinnersPerClient(List<SearchResult> searchResults)
        {
            var winners = searchResults.OrderBy(result => result.SearchClient)
                                       .GroupBy(result => result.SearchClient, result => result,
                                        (client, result) => new Winner
                                        {
                                            ClientName = client,
                                            Query = result.MaxValue(r => r.TotalResults).Query
                                        });
            return winners;
        }

        public string GetTotalWinner(List<SearchResult> searchResults)
        {
            var winner = searchResults.OrderBy(result => result.SearchClient)
                                      .GroupBy(result => result.Query, result => result,
                                      (query, result) => new
                                      {
                                          Query = query,
                                          Total = result.Sum(r => r.TotalResults)
                                      })
                                      .MaxValue(r => r.Total).Query;
            return winner;
        }

        public IEnumerable<IGrouping<string, SearchResult>> GetGroupedResults(List<SearchResult> searchResults)
        {
            var results = searchResults.OrderBy(result => result.SearchClient)
                                       .ToLookup(result => result.Query, result => result);
            return results;
        }
    }
}