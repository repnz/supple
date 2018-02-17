using System;

namespace Supple.Deserialization.Exceptions
{
    public class InvalidNodeException : Exception
    {
        public Node Node { get; }

        public InvalidNodeException(Node node)
        {
            Node = node;
        }

        public InvalidNodeException(Node node, Exception inner) : base("", inner)
        {
            Node = node;
        }

        public InvalidNodeException(Node node, string msg) : base(msg)
        {
            Node = node;
        }

        public InvalidNodeException(Node node, string msg, Exception inner) : base(msg, inner)
        {
            Node = node;
        }


        public override string Message
        {
            get
            {
                return $"Invalid Node {Node.Name} {base.Message}";
            }
        }
    }
}
