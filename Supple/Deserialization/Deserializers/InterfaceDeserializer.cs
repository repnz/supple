using System;
using Supple.Deserialization.Exceptions;
using Supple.TypeResolvers;

namespace Supple.Deserialization.Deserializers
{
    public class InterfaceDeserializer : INodeDeserializer
    {
        private readonly IRuntimeTypeResolver _typeResolver;
        private readonly INodeDeserializer _deserializer;

        public InterfaceDeserializer(IRuntimeTypeResolver typeResolver,
            INodeDeserializer deserializer)
        {
            _deserializer = deserializer;
            _typeResolver = typeResolver;
        }

        public bool IsMatch(Type type, Node node)
        {
            return type.IsInterface || type.IsAbstract;
        }

        public object Deserialize(Type type, Node node)
        {
            Node typeNode = node.GetChildren("Type");

            if (typeNode == null)
            {
                throw new InterfaceTypeNotFoundException(type, node);
            }

            string typeName = typeNode.Value;

            typeNode.Remove();

            try
            {
                type = _typeResolver.GetType(typeName);
            }
            catch (Exception e)
            {
                throw new RuntimeTypeException(typeName, type, e);
            }
            
            return _deserializer.Deserialize(type, node);
        }
    }
}
