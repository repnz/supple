using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    class ConstructorNotFoundException : Exception
    {
        public XElement Element { get; }
        public Type Type { get; }

        public ConstructorNotFoundException(XElement element, Type type)
        {
            Element = element;
            Type = type;
        }

        public ConstructorNotFoundException(XElement element, Type type, string message) : base(message)
        {
            Element = element;
            Type = type;
        }

        public ConstructorNotFoundException(XElement element, Type type, string message, Exception innerException) : base(message, innerException)
        {
            Element = element;
            Type = type;
        }

        public ConstructorNotFoundException(XElement element, Type type, Exception innerException) : base("", innerException)
        {
            Element = element;
            Type = type;
        }

        public override string Message => $"Constructor not found for element {Element.Name} of type {Type.Name} {base.Message}";
    }
}
