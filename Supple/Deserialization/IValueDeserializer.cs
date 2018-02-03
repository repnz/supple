using System;

namespace Supple.Deserialization
{
    public interface IValueDeserializer 
    {
        bool IsMatch(Type type, string name, string value);

        object Deserialize(Type type, string name, string value);
    }
}
