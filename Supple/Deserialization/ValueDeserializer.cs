using System;

namespace Supple.Deserialization
{
    public abstract class ValueDeserializer : INodeDeserializer
    {
        protected abstract object Deserialize(Type type, string name, string value);
        protected abstract bool IsMatch(Type type, string name, string value);

        public object Deserialize(Type type, Node node)
        {
            return Deserialize(type, node.Name, node.Value);
        }

        public bool IsMatch(Type type, Node node)
        {
            return !node.HasChildren && IsMatch(type, node.Name, node.Value);
        }
    }
}
