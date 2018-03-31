using System;
using System.IO;
using System.Linq;
using TrainTicket.Core.Services;

namespace TrainTicketMachine.Sample.Console
{
    public class ConsoleFetcherService : IFetcherService
    {
        public ConsoleFetcherService()
        {
        }

        public string[] Fetch()
        {
            var args = Environment.GetCommandLineArgs();
            var contentParam = args[2];
            string content;
            if (contentParam.Trim().ToLower() == "load")
            {
                content = File.ReadAllText(@".\items.txt");
            }
            else
            {
                content = contentParam;
            }
            return content.Split(new[] { ",", ";", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(y => y.Trim()).ToArray();
        }
    }
}