using System;

namespace Supple.Deserialization.Exceptions
{
    public class UnexpectedNodeException : InvalidNodeException
    {
        public string ExpectedName { get; }

        public UnexpectedNodeException(string expectedName, Node node) : base(node)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedNodeException(string expectedName, Node node, Exception inner) : base(node, inner)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedNodeException(string expectedName, Node node, string msg) : base(node, msg)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedNodeException(string expectedName, Node node, string msg, Exception inner) : base(node, msg, inner)
        {
            ExpectedName = expectedName;
        }

        public override string Message => $"Expected '{ExpectedName}' but got '{Node.Name}' {base.Message}";
    }
}
