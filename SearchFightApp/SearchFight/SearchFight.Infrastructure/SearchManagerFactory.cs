using System;
using System.Linq;
using SearchFight.Core.Implementations;
using SearchFight.Core.Interfaces;
using SearchFight.Services.Interfaces;

namespace SearchFight.Infrastructure
{
    public class SearchManagerFactory
    {
        public static ISearchManager CreateSearchManager()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                ?.Where(assembly => assembly.FullName.StartsWith("SearchFight"));

            var searchClients = loadedAssemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterface(typeof(ISearch).ToString()) != null)
                .Select(type => Activator.CreateInstance(type) as ISearch);

            return new SearchManager(searchClients);
        }
    }
}