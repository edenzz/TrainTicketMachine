using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace TrainTicket.Core.Services.IntegrationTests
{
    [TestClass]
    public class TrieTests
    {
        private Mock<IFetcherService> _fetcherService;
        private ISearchService _searchService;

        [TestInitialize]
        public void TestInitialize()
        {
            _fetcherService = new Mock<IFetcherService>();
        }

        [DataRow("DART", new string[] { "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY" }, new string[] { "DARTFORD", "DARTMOUTH" }, new char[] { 'F', 'M' })]
        [DataRow("LIVERPOOL", new string[] { "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON" }, new string[] { "LIVERPOOL", "LIVERPOOL LIME STREET" }, new char[] { ' ' })]
        [DataRow("KINGS CROSS", new string[] { "EUSTON", "LONDON BRIDGE", "VICTORIA" }, new string[] { }, new char[] { })]
        [DataRow(null, new string[] { "BANSTEAD", "BARKING", "BARNES" }, new string[] { "BANSTEAD", "BARKING", "BARNES" }, new char[] { 'B' })]
        [DataRow("", new string[] { "ST PANCRAS", "FENCHURCH STREET", "WHYTELEAFE" }, new string[] { "ST PANCRAS", "FENCHURCH STREET", "WHYTELEAFE" }, new char[] { 'S', 'F', 'W' })]
        [DataTestMethod]
        public void TestSearch(string input, string[] stations, string[] resultStations, char[] resultChars)
        {
            _fetcherService.Setup(x => x.Fetch()).Returns(stations);
            _searchService = new TrieSearchService(_fetcherService.Object);

            var searchResult = _searchService.Search(input);
            Assert.IsTrue(resultStations.ToList().SequenceEqual(searchResult.Words) &&  
                          resultChars.ToList().SequenceEqual(searchResult.NextCharacters));
        }
    }
}
