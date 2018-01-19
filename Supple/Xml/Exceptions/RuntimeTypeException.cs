using System;

namespace Supple.Xml.Exceptions
{
    public class RuntimeTypeException : Exception
    {
        public string TypeName { get; }

        public RuntimeTypeException(string typeName)
        {
            TypeName = typeName;
        }

        public RuntimeTypeException(string typeName, Exception innerException) : base("", innerException)
        {
            TypeName = typeName;
        }

        public RuntimeTypeException(string typeName, string msg) : base(msg)
        {
            TypeName = typeName;
        }

        public RuntimeTypeException(string typeName, string msg, Exception innerException) : base(msg, innerException)
        {
            TypeName = typeName;
        }

        public override string Message => $"Cannot fetch runtime type {TypeName} {base.Message}";
    }
}
