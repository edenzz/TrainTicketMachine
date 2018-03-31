using Core.Services;
using log4net;
using TrainTicket.Core.Services.Data;
using TrainTicket.Core.Services.Extensions;

namespace TrainTicket.Core.Services
{
    public interface ITrieBuilderService
    {
        Node Build(params string[] items);
    }

    public sealed class TrieBuilderService : ITrieBuilderService
    {
        private readonly Node _root;
        private readonly ILog _logger;

        public TrieBuilderService()
        {
            _logger = LogManager.GetLogger(typeof(TrieBuilderService));
            _root = new Node(Constants.TRIE_ROOT_CHAR, 0, null);
        }
        
        public Node Build(params string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                InsertItem(items[i]);
                _logger.Debug($"Item {items[i]} inserted");
            }
            return _root;

            void InsertItem(string s)
            {
                var commonPrefix = _root.Prefix(s);
                var current = commonPrefix;

                for (var i = current.Depth; i < s.Length; i++)
                {
                    var newNode = new Node(s[i], current.Depth + 1, current);
                    current.Children.Add(newNode);
                    current = newNode;
                }
                current.Children.Add(new Node(Constants.TRIE_LEAF_CHAR, current.Depth + 1, current));
            }
        }
    }
}