using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.Core.Interfaces;
using SearchFight.Domain.Results;
using SearchFight.Infrastructure;
using Xunit;

namespace SearchFight.Tests.Core
{
    public class SearchManagerTests
    {
        private readonly ISearchManager _searchManager;
        private readonly List<string> _queries;
        private readonly List<SearchResult> _searchResults;
        public SearchManagerTests()
        {
            _searchManager = SearchManagerFactory.CreateSearchManager();
            _queries = new List<string> { ".net", "java" };
            _searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "Google",
                    TotalResults = 4450000000
                },
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "MSN Search",
                    TotalResults = 1235442
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "Google",
                    TotalResults = 96600000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "MSN Search",
                    TotalResults = 9438148
                },
            };
        }

        #region "TotalWinner"

        [Fact]
        public void GivenTotalWinner_WhenHealthSearchResults_ReturnsString()
        {
            var totalWinner = _searchManager.GetTotalWinner(_searchResults);

            Assert.IsType<string>(totalWinner);
        }

        [Fact]
        public async Task GivenTotalWinner_WhenNullSearchResults_ThrowsArgumentNullException()
        {
            List<SearchResult> searchResults = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return Task.Run(() =>
                {
                    return _searchManager.GetTotalWinner(searchResults);
                });
            });
        }

        [Fact]
        public void GivenTotalWinner_WhenNetAsTotalWinner_ReturnsNet()
        {
            var totalWinner = _searchManager.GetTotalWinner(_searchResults);

            Assert.True(totalWinner == ".net");
        }

        #endregion "TotalWinner"

        #region "WinnersPerClient"

        [Fact]
        public void GivenWinnersPerClient_WhenHealthSearchResults_ReturnsListOfWinners()
        {
            var winners = _searchManager.GetWinnersPerClient(_searchResults);

            Assert.All(winners, item => Assert.True(!string.IsNullOrEmpty(item.ClientName)));
            Assert.All(winners, item => Assert.True(!string.IsNullOrEmpty(item.Query)));
        }

        [Fact]
        public async Task GivenWinnersPerClient_WhenNullSearchResults_ThrowsArgumentNullException()
        {
            List<SearchResult> searchResults = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return Task.Run(() =>
                {
                    return _searchManager.GetWinnersPerClient(searchResults);
                });
            });
        }

        #endregion "WinnersPerClient"

        #region "SearchResults"

        [Fact]
        public async Task GivenSearchResults_WhenHealthQueries_ReturnsListOfResults()
        {
            var searchResults = await _searchManager.GetSearchResultsAsync(_queries);

            Assert.IsType<List<SearchResult>>(searchResults);
        }

        #endregion "SearchResults"

        #region "GroupedResults"

        [Fact]
        public void GivenGroupedResults_WhenHealthSearchResults_ReturnsLookUp()
        {
            var groupedResults = _searchManager.GetGroupedResults(_searchResults);

            Assert.IsType<Lookup<string, SearchResult>>(groupedResults);
        }

        [Fact]
        public async Task GivenGroupedResults_WhenNullSearchResults_ThrowsArgumentNullException()
        {
            List<SearchResult> searchResults = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return Task.Run(() =>
                {
                    return _searchManager.GetGroupedResults(searchResults);
                });
            });
        }

        #endregion "GroupedResults"

        #region "SearchReport"

        [Fact]
        public async Task GivenSearchReport_WhenNullQueries_ThrowsArgumentNullException()
        {
            List<string> queries = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return Task.Run(() =>
                {
                    return _searchManager.GetSearchReportAsync(queries);
                });
            });
        }

        [Fact]
        public async Task GivenSearchReport_WhenHealthQueries_ReturnsString()
        {
            var result = await _searchManager.GetSearchReportAsync(_queries);

            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task GivenSearchReport_WhenHealthQueries_ReturnsNotEmptyString()
        {
            var result = await _searchManager.GetSearchReportAsync(_queries);

            Assert.True(!string.IsNullOrEmpty(result));
        }

        #endregion "SearchReport"
    }
}