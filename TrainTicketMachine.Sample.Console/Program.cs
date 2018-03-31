using Autofac;
using System.Diagnostics;
using TrainTicket.Core.Services;

namespace TrainTicketMachine.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.Init();
            using (var scope = container.BeginLifetimeScope())
            {
                var searchService = scope.Resolve<ISearchService>();
                var stopWatch = Stopwatch.StartNew();

                System.Console.WriteLine("---------Search Started---------");
                System.Console.WriteLine($"Search Input: {args[0]}");
                var result = searchService.Search(args[0]);

                System.Console.WriteLine();
                System.Console.WriteLine("---------Search Finished--------");
                System.Console.WriteLine();
                System.Console.WriteLine($"---------Results Found---------");
                System.Console.WriteLine($"Elaped: {stopWatch.Elapsed}");
                System.Console.WriteLine($"Possible Choices: {string.Join(", ", result.Words)}");
                System.Console.WriteLine($"Possible Next Characters: {string.Join(", ", result.NextCharacters)}");
                System.Console.ReadKey();
            }
        }
    }
}
