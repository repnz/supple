using System;
using System.Xml.Linq;

namespace Supple.Xml.Exceptions
{
    public class UnexpectedElementException : InvalidElementException
    {
        public string ExpectedName { get; }

        public UnexpectedElementException(string expectedName, XElement element) : base(element)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedElementException(string expectedName, XElement element, Exception inner) : base(element, inner)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedElementException(string expectedName, XElement element, string msg) : base(element, msg)
        {
            ExpectedName = expectedName;
        }

        public UnexpectedElementException(string expectedName, XElement element, string msg, Exception inner) : base(element, msg, inner)
        {
            ExpectedName = expectedName;
        }

        public override string Message => $"Expected '{ExpectedName}' but got '{Element.Name}' {base.Message}";
    }
}
