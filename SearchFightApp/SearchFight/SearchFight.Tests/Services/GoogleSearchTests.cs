using System;
using System.Threading.Tasks;
using SearchFight.Services.Implementations;
using SearchFight.Services.Interfaces;
using Xunit;

namespace SearchFight.Tests.Services
{
    public class GoogleSearchTests
    {
        private readonly ISearch _search;

        public GoogleSearchTests()
        {
            _search = new GoogleSearch();
        }

        [Fact]
        public async Task GivenGoogleSearch_WhenOkStatusCode_ReturnsNumberOfResults()
        {
            var query = ".net";

            var result = await _search.GetNumberOfResultsAsync(query);

            Assert.IsType<long>(result);
        }

        [Fact]
        public async Task GivenGoogleSearch_WhenNullQuery_ThrowsArgumentException()
        {
            string query = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _search.GetNumberOfResultsAsync(query));
        }
    }
}