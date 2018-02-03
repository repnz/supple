using System;

namespace Supple.Deserialization.Exceptions
{
    public class ConstructorNotFoundException : InvalidNodeException
    {
        public Type NodeType { get; }

        public ConstructorNotFoundException(Node node, Type type) : base(node)
        {
            NodeType = type;
        }

        public ConstructorNotFoundException(Node node, Type type, string message) : base(node, message)
        {
            NodeType = type;
        }

        public ConstructorNotFoundException(Node node, Type type, string message, Exception innerException) : base(node, message, innerException)
        {
            NodeType = type;
        }

        public ConstructorNotFoundException(Node node, Type type, Exception innerException) : base(node, innerException)
        {
            NodeType = type;
        }

        public override string Message => $"Constructor not found for node {Node.Name} of type {NodeType.Name} {base.Message}";
    }
}
