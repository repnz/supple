using System;

namespace Supple.Deserialization.InstanceCreators
{
    class DefaultInstanceCreator : IInstanceCreator
    {
        public object CreateInstance(Type type, Node element)
        {
            return Activator.CreateInstance(type);
        }
    }
}
