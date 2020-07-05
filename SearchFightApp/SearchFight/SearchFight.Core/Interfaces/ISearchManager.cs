using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.Domain.Results;

namespace SearchFight.Core.Interfaces
{
    public interface ISearchManager
    {
        Task<string> GetSearchReportAsync(List<string> queries);
        Task<List<SearchResult>> GetSearchResultsAsync(IEnumerable<string> queries);
        IEnumerable<Winner> GetWinnersPerClient(List<SearchResult> searchResults);
        string GetTotalWinner(List<SearchResult> searchResults);
        IEnumerable<IGrouping<string, SearchResult>> GetGroupedResults(List<SearchResult> searchResults);
    }
}