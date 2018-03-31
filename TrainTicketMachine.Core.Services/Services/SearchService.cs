using Core.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainTicket.Core.Services.Data;
using TrainTicket.Core.Services.Extensions;
using TrainTicket.Core.Services.Models;

namespace TrainTicket.Core.Services
{
    public interface ISearchService
    {
        SearchResult Search(string searchInput);
    }

    public sealed class TrieSearchService : ISearchService
    {
        private readonly ILog _logger;
        private readonly ITrieBuilderService _treeBuilderService;
        private readonly Node _rootNode;

        public TrieSearchService(IFetcherService fetcherService)
        {
            _logger = LogManager.GetLogger(typeof(TrieSearchService));
            _treeBuilderService = new TrieBuilderService();
            _rootNode = Build();

            Node Build()
            {
                var items = fetcherService.Fetch();
                if (!items.Any())
                {
                    throw new InvalidOperationException("No items were fetched");
                }
                return _treeBuilderService.Build(items);
            }
        }

        public SearchResult Search(string searchInput)
        {
            var node = _rootNode.Prefix(searchInput);
            List<string> possibleChoices = new List<string>();
            GetSuffixes(_rootNode, searchInput, node, possibleChoices);
            _logger.Info($"Search finished successfully for {searchInput}");
           
            return new SearchResult()
            {
                Words = possibleChoices.Any() ? possibleChoices.ToArray() : new string[] { },
                NextCharacters = possibleChoices.Any() && !node.IsLeaf() ? node.Children.Where(x => x.Value != Constants.TRIE_LEAF_CHAR).Select(x => x.Value).ToArray() : new char[] { }
            };

            void GetSuffixes(Node root, string prefixStr, Node prefixNode, List<string> suffixes)
            {
                _logger.Debug("GetSuffixes");
                if (!string.IsNullOrEmpty(prefixStr) && (prefixNode == root || prefixNode.IsLeaf()))
                {
                    return;
                }

                foreach (var child in prefixNode.Children)
                {
                    _logger.Debug($"Prefix: {prefixStr}");
                    if (child.IsLeaf())
                    {
                        _logger.Debug($"Reach Leaf: {prefixStr}");
                        suffixes.Add(prefixStr);
                    }
                    _logger.Debug($"Entering Recursion: {prefixStr + child.Value}");
                    GetSuffixes(root, $"{prefixStr + child.Value}", child, suffixes);
                }
            }
        }
    }
}