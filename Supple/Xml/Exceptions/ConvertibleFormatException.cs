using System;
using System.Runtime.Serialization;

namespace Supple.Xml.Exceptions
{
    public class ConvertibleFormatException : FormatException
    {
        public string Name { get; }
        public string Value { get; }
        public Type ExpectedType { get; }

        public ConvertibleFormatException(string name, string value, Type expectedType)
        {
            Name = name;
            Value = value;
            ExpectedType = expectedType;
        }

        public ConvertibleFormatException(string name, string value, Type expectedType, string message) : base(message)
        {
            Name = name;
            Value = value;
            ExpectedType = expectedType;
        }

        public ConvertibleFormatException(string name, string value, Type expectedType, Exception innerException) : base("", innerException)
        {
            Name = name;
            Value = value;
            ExpectedType = expectedType;
        }

        public ConvertibleFormatException(string name, string value, Type expectedType, string message, Exception innerException) : base(message, innerException)
        {
            Name = name;
            Value = value;
            ExpectedType = expectedType;
        }

        public override string Message => $"Cannot convert {Name} with value {Value} to {ExpectedType.Name}";
    }
}
