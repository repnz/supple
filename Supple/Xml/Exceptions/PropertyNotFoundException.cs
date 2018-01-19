using System;

namespace Supple.Xml.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public string PropertyName { get; }
        public Type BaseType { get; }

        public PropertyNotFoundException(string propertyName, Type baseType)
        {
            PropertyName = propertyName;
            BaseType = baseType;
        }

        public PropertyNotFoundException(string propertyName, Type baseType, string message) : base(message)
        {
            PropertyName = propertyName;
            BaseType = baseType;
        }

        public PropertyNotFoundException(string propertyName, Type baseType, string message, Exception innerException) : base(message, innerException)
        {
            PropertyName = propertyName;
            BaseType = baseType;
        }

        public PropertyNotFoundException(string propertyName, Type baseType, Exception innerException) : base("", innerException)
        {
            PropertyName = propertyName;
            BaseType = baseType;
        }

        public override string Message => $"Cannot find property {PropertyName} in type {BaseType.Name} {base.Message}";
    }
}
