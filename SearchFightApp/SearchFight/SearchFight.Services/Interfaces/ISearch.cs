using System.Threading.Tasks;

namespace SearchFight.Services.Interfaces
{
    public interface ISearch
    {
        string ClientName { get; }
        Task<long> GetNumberOfResultsAsync(string query);
    }
}