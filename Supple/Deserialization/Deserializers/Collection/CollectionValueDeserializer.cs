using System;
using System.Reflection;
using System.Linq;

namespace Supple.Deserialization.Deserializers.Collection
{
    class CollectionValueDeserializer : INodeDeserializer
    {
        private readonly IDelegator _delegator;

        public CollectionValueDeserializer(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public object Deserialize(Type type, Node node)
        {
            Type listBase = CollectionTools.GetCollectionBase(type);
            Type elementType = CollectionTools.GetCollectionElementType(listBase);
            object instance = Activator.CreateInstance(type);
            MethodInfo method = listBase.GetMethod("Add");
            string elementTypeName = _delegator.CreateName(elementType);

            // ignore open and close brackets 
            string value = node.Value.Substring(1, node.Value.Length - 2);

            foreach (string listElementValue in value.Split(',').Select(x => x.Trim()))
            {
                object listItem = _delegator.Deserialize(
                    elementType,
                    new ValueNode(elementTypeName, listElementValue)
                    );

                method.Invoke(instance, new object[] { listItem });
            }

            return instance;
        }

        public bool IsMatch(Type type, Node node)
        {
            return CollectionTools.HasCollectionBase(type) && 
                node.Value.StartsWith("[") && 
                node.Value.EndsWith("]");
        }
    }
}
