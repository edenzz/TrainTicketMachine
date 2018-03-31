using TrainTicket.Core.Services.Data;

namespace TrainTicket.Core.Services.Extensions
{
    public static class NodeExtensions
    {
        public static Node Prefix(this Node Root, string input)
        {
            var currentNode = Root; 
            var result = currentNode;

            if(!string.IsNullOrEmpty(input))
            {
                foreach (var character in input)
                {
                    currentNode = currentNode.FindChildNode(character);
                    if (currentNode == null)
                    {
                        break;
                    }
                    result = currentNode;
                }
            }
            return result;
        }
    }
}