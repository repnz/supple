using System;

namespace Supple.Deserialization.Exceptions
{
    public class InterfaceTypeNotFoundException : InvalidNodeException
    {
        public Type InterfaceType { get; }

        public InterfaceTypeNotFoundException(Type interfaceType, Node node) : base(node)
        {
            InterfaceType = interfaceType;
        }

        public InterfaceTypeNotFoundException(Type interfaceType, Node node, Exception inner) : base(node, inner)
        {
            InterfaceType = interfaceType; 
        }

        public InterfaceTypeNotFoundException(Type interfaceType, Node node, string msg) : base(node, msg)
        {
            InterfaceType = interfaceType;
        }

        public InterfaceTypeNotFoundException(Type interfaceType, Node node, string msg, Exception inner) : base(node, msg, inner)
        {
            InterfaceType = interfaceType;
        }

        public override string Message => $"Cannot deserialize interface '{InterfaceType.Name}' without specifing 'Type' {base.Message}";
    }
}
