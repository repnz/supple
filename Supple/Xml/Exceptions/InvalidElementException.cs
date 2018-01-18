using System;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    public class InvalidElementException : Exception
    {
        public XElement Element { get; }

        public InvalidElementException(XElement element)
        {
        }

        public InvalidElementException(XElement element, Exception inner) : base("", inner)
        {
            Element = element;
        }

        public InvalidElementException(XElement element, string msg) : base(msg)
        {
            Element = element;
        }

        public InvalidElementException(XElement element, string msg, Exception inner) : base(msg, inner)
        {
            Element = element;
        }


        public override string Message
        {
            get
            {
                return $"Invalid Element {Element.Name} {base.Message}";
            }
        }
    }
}
