using System;

namespace Supple.Deserialization.Exceptions
{
    public class EnumConvertException : InvalidNodeException
    {
        public Type EnumType { get; }

        public EnumConvertException(Node node, Type enumType) : base(node)
        {
            EnumType = enumType;
        }

        public EnumConvertException(Node node, Type enumType, 
            Exception inner) : base(node, inner)
        {
            EnumType = enumType;
        }

        public EnumConvertException(Node node, Type enumType, string msg) : base(node, msg)
        {
            EnumType = enumType;
        }

        public EnumConvertException(Node node, Type enumType, string msg, 
            Exception inner) : base(node, msg, inner)
        {
            EnumType = enumType;
        }

        public override string Message =>
            $"Cannot convert value {Node.Value} to enum type {EnumType.Name}";

    }
}
