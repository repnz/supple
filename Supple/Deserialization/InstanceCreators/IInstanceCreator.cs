using System;

namespace Supple.Deserialization.InstanceCreators
{
    public interface IInstanceCreator
    {
        object CreateInstance(Type type, Node node);
    }
}
