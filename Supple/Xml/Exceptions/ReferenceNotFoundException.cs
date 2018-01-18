using System;
using System.Collections.Generic;

namespace Supple.Xml.Exceptions
{
    public class ReferenceNotFoundException : KeyNotFoundException
    {
        public string Reference { get; }

        public ReferenceNotFoundException(string reference)
        {
            Reference = reference;
        }

        public ReferenceNotFoundException(string reference, string message) : base(message)
        {
            Reference = reference;
        }

        public ReferenceNotFoundException(string reference, string message, Exception innerException) : base(message, innerException)
        {
            Reference = reference;
        }

        public ReferenceNotFoundException(string reference, Exception innerException) : base("", innerException)
        {
            Reference = reference;
        }

        public override string Message => $"Reference '{Reference}' is not found in the reference store. {base.Message}";
    }
}
