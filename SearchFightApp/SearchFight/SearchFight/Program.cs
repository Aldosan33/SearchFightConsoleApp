using System;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.Infrastructure;

namespace SearchFight
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("There mush be one argument at least, please try again !");
                    return;
                }

                Console.WriteLine("Processing results ...");
                var searchManager = SearchManagerFactory.CreateSearchManager();
                var result = await searchManager.GetSearchReportAsync(args?.ToList());

                Console.Clear();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing your request: {ex.Message}");
                return;
            }
            Console.ReadKey();
        }
    }
}
