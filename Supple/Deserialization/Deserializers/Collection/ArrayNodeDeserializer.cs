using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Supple.Deserialization.Exceptions;
using Supple.TypeResolvers;

namespace Supple.Deserialization.Deserializers.Collection
{
    class ArrayNodeDeserializer : INodeDeserializer
    {
        private readonly IDelegator _delegator;
        private readonly IRuntimeTypeResolver _runtimeTypeResolver;

        public ArrayNodeDeserializer(IDelegator delegator, IRuntimeTypeResolver resolver)
        {
            _delegator = delegator;
            _runtimeTypeResolver = resolver;
        }

        public Type GetUsedType(Node node, string elementName, Type defaultType)
        {
            if (node.Name == elementName)
            {
                return defaultType;
            }

            if (!(defaultType.IsAbstract || defaultType.IsInterface))
            {
                throw new UnexpectedNodeException(elementName, node);
            }

            return _runtimeTypeResolver.GetType(node.Name);
        }

        public object Deserialize(Type type, Node node)
        {
            Type elementType = type.GetElementType();
            string elementName = _delegator.CreateName(elementType);

            IEnumerable<Node> subNodes = node.GetChildren();

            IList arr = (IList)Activator.CreateInstance(type, new object[] { subNodes.Count() });
            
            int curIndex = 0;

            foreach (Node subNode in subNodes)
            {
                Type usedType = GetUsedType(subNode, elementName, elementType);
                arr[curIndex] = _delegator.Deserialize(usedType, subNode);
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
