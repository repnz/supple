using Supple.Deserialization.Exceptions;
using System;
using System.Reflection;

namespace Supple.Deserialization.Deserializers
{
    class EnumDeserializer : INodeDeserializer
    {
        public object Deserialize(Type type, Node node)
        {
            try
            {
                return Enum.Parse(type, node.Value);
            }
            catch (Exception e)
            {
                throw new EnumConvertException(node, type, e);
            }
        }

        public bool IsMatch(Type type, Node node)
        {
            return type.IsEnum;
        }
    }
}
