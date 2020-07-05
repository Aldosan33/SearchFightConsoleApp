using System;
using System.Threading.Tasks;
using SearchFight.Services.Implementations;
using SearchFight.Services.Interfaces;
using Xunit;

namespace SearchFight.Tests.Services
{
    public class BingSearchTests
    {
        private readonly ISearch _search;

        public BingSearchTests()
        {
            _search = new BingSearch();
        }

        [Fact]
        public async Task GivenBingSearch_WhenOkStatusCode_ReturnsNumberOfResults()
        {
            var query = ".net";

            var result = await _search.GetNumberOfResultsAsync(query);

            Assert.IsType<long>(result);
        }

        [Fact]
        public async Task GivenBingSearch_WhenNullQuery_ThrowsArgumentException()
        {
            string query = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _search.GetNumberOfResultsAsync(query));
        }
    }
}