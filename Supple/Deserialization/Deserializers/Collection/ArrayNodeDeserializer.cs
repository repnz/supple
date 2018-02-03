using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Supple.Deserialization.Deserializers.Collection
{
    class ArrayNodeDeserializer : INodeDeserializer
    {
        private readonly IDelegator _delegator;

        public ArrayNodeDeserializer(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public object Deserialize(Type type, Node node)
        {
            Type elementType = type.GetElementType();
            string elementName = _delegator.CreateName(elementType);

            IEnumerable<Node> subNodes = node.GetChildren();

            int arrayLength = subNodes.Count((n) => n.Name == elementName);

            IList arr = (IList)Activator.CreateInstance(type, new object[] { arrayLength });
            
            int curIndex = 0;

            foreach (Node subNode in subNodes)
            {
                arr[curIndex] = _delegator.Deserialize(elementType, subNode);
                curIndex++;
            }

            return arr;
        }

        public bool IsMatch(Type type, Node node)
        {
            return node.HasChildren && type.IsArray;
        }
    }
}
