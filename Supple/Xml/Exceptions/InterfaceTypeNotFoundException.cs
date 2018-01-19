using System;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    public class InterfaceTypeNotFoundException : InvalidElementException
    {
        public Type InterfaceType { get; }

        public InterfaceTypeNotFoundException(Type interfaceType, XElement element) : base(element)
        {
            InterfaceType = interfaceType;
        }

        public InterfaceTypeNotFoundException(Type interfaceType, XElement element, Exception inner) : base(element, inner)
        {
            InterfaceType = interfaceType; 
        }

        public InterfaceTypeNotFoundException(Type interfaceType, XElement element, string msg) : base(element, msg)
        {
            InterfaceType = interfaceType;
        }

        public InterfaceTypeNotFoundException(Type interfaceType, XElement element, string msg, Exception inner) : base(element, msg, inner)
        {
            InterfaceType = interfaceType;
        }

        public override string Message => $"Cannot deserialize interface '{InterfaceType.Name}' without specifing 'Type' {base.Message}";
    }
}
