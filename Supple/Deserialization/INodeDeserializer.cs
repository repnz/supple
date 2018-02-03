using System;

namespace Supple.Deserialization
{
    public interface INodeDeserializer
    {
        bool IsMatch(Type type, Node node);

        object Deserialize(Type type, Node node);
    }
}
