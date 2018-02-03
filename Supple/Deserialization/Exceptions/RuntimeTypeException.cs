using System;

namespace Supple.Deserialization.Exceptions
{
    public class RuntimeTypeException : Exception
    {
        public string TypeName { get; }
        public Type InterfaceType { get; }

        public RuntimeTypeException(string typeName, Type interfaceType)
        {
            TypeName = typeName;
            InterfaceType = interfaceType;
        }

        public RuntimeTypeException(string typeName, Type interfaceType, Exception innerException) : base("", innerException)
        {
            TypeName = typeName;
            InterfaceType = interfaceType;
        }

        public RuntimeTypeException(string typeName, Type interfaceType, string msg) : base(msg)
        {
            TypeName = typeName;
            InterfaceType = interfaceType;
        }

        public RuntimeTypeException(string typeName, Type interfaceType, string msg, Exception innerException) : base(msg, innerException)
        {
            TypeName = typeName;
            InterfaceType = interfaceType;
        }

        public override string Message => $"Cannot fetch runtime type '{TypeName}' of interface '{InterfaceType.Name}' {base.Message}";
    }
}
