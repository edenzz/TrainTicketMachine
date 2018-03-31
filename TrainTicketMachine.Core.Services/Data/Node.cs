using System.Collections.Generic;

namespace TrainTicket.Core.Services.Data
{
    public class Node
    {
        public char Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }

        public Node(char value, int depth, Node parent)
        {
            Value = value;
            Children = new List<Node>();
            Depth = depth;
            Parent = parent;
        }

        public bool IsLeaf() => Children.Count == 0;

        public Node FindChildNode(char c)
        {
            foreach (var child in Children)
            {
                if (child.Value == c)
                {
                    return child;
                }
            }
            return null;
        }        
    }
}