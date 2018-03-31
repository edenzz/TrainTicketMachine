# Train Ticket Machine

This repository aims to solve a [Backend Code Challenge](https://github.com/edenzz/TrainTicketMachine/blob/master/TruphoneBackendDevelopment-TrainTicketMachine%20v1.1.pdf)

[![License](https://img.shields.io/badge/licence-MIT-green.svg)](https://github.com/edenzz/TrainTicketMachine/blob/master/LICENSE)

## How to build?
#### Prerequisites

1. [.NET Framework 4.5.2](https://www.microsoft.com/net/download/dotnet-framework-runtime/net452)
2. [Visual Studio 2017](https://www.visualstudio.com/downloads/), minimum installation is enough.

#### Steps
1. Clone the code repo from `https://github.com/edenzz/TrainTicketMachine.git`
2. Open `TrainTicketMachine.sln` from code repo in Visual Studio and build *TrainTicketMachine.sln*.

## Usage

1. Implement the IFetcherService Interface:

```C#
namespace TrainTicket.Core.Services
{
	public interface IFetcherService 
	{
		string[] Fetch();
	}
}
```	

#### For example:

```C#
using TrainTicket.Core.Services;

namespace YourNamespace
{
	public class DummyFetcherService : IFetcherService
	{
		public string[] Fetch()
		{
		   return new string[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" };
		}
	}
}

```		  

2. Invoke Search Method from your application:

```C#
using TrainTicket.Core.Services;

namespace YourNamespace
{
	...
	ISearchService searchService = new TrieSearchService(new DummyFetcherService()); 
	searchService.Search(text) // text query from the user
	...
}
```		   
			   
## Samples

- [Console Sample using Autofac](https://github.com/edenzz/TrainTicketMachine/tree/master/TrainTicketMachine.Sample.Console)
			   
## Used Libraries

- [Autofac](https://autofac.org/) - for the Console Sample
- [Log4net](https://logging.apache.org/log4net/) - for Logging purposes
- [Moq](https://github.com/moq/moq) - for the Integration Tests