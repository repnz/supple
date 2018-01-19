using System;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    public class InterfaceTypeNotFoundException : InvalidElementException
    {
        public InterfaceTypeNotFoundException(XElement element) : base(element)
        {
        }

        public InterfaceTypeNotFoundException(XElement element, Exception inner) : base(element, inner)
        {
        }

        public InterfaceTypeNotFoundException(XElement element, string msg) : base(element, msg)
        {
        }

        public InterfaceTypeNotFoundException(XElement element, string msg, Exception inner) : base(element, msg, inner)
        {
        }

        public override string Message => $"Cannot deserialize interface without specifing 'Type' {base.Message}";
    }
}
