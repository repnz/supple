using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    public class UnexpectedElementException : InvalidElementException
    {
        public string ExpectedName { get; }

        public UnexpectedElementException(string expectedName, XElement element) : base(element)
        {
        }

        public UnexpectedElementException(string expectedName, XElement element, Exception inner) : base(element, inner)
        {
        }

        public UnexpectedElementException(string expectedName, XElement element, string msg) : base(element, msg)
        {
        }

        public UnexpectedElementException(string expectedName, XElement element, string msg, Exception inner) : base(element, msg, inner)
        {
        }

        public override string Message => $"Expected {ExpectedName} but got {Element.Name} {base.Message}";
    }
}
