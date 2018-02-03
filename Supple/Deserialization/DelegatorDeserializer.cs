using System;
using System.Collections.Generic;

namespace Supple.Deserialization
{
    public class DelegatorDeserializer : IDelegator
    {
        public IList<INodeDeserializer> NodeDeserializers { get; }
        public IList<INodeDeserializer> ValueDeserializers { get; }
        public IList<ITypeNameCreator> TypeNameCreators { get; }

        public DelegatorDeserializer()
        {
            NodeDeserializers = new List<INodeDeserializer>();
            TypeNameCreators = new List<ITypeNameCreator>();
            ValueDeserializers = new List<INodeDeserializer>();
        }

        public string CreateName(Type type)
        {
            foreach (ITypeNameCreator nameCreator in TypeNameCreators)
            {
                if (nameCreator.IsMatch(type))
                {
                    return nameCreator.CreateName(type);
                }
            }

            throw new InvalidOperationException($"Missing ITypeNameCreator to match {type.Name}");
        }

        public object Deserialize(Type type, Node node)
        {
            foreach (var nodeDeserializer in NodeDeserializers)
            {
                if (nodeDeserializer.IsMatch(type, node))
                {
                    return nodeDeserializer.Deserialize(type, node);
                }
            }

            throw new InvalidOperationException($"Missing NodeDeserializer to match {type.Name}, {node.Name}");
        }

        public bool IsMatch(Type type, Node node)
        {
            return true;
        }

        public bool IsMatch(Type type) { return true; }
    }
}
