using Autofac;
using log4net.Config;
using TrainTicket.Core.Services;

namespace TrainTicketMachine.Sample.Console
{
    public class Startup
    {
        public static IContainer Init()
        {
            XmlConfigurator.Configure();
            var builder = new ContainerBuilder();
            LoadIoC(builder);
            var container = builder.Build();
            return container;
        }

        private static void LoadIoC(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleFetcherService>().As<IFetcherService>();
            builder.RegisterType<TrieSearchService>().As<ISearchService>().SingleInstance();
        }
    }
}
