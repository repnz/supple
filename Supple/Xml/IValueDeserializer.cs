using System;

namespace Supple.Xml
{
    public interface IValueDeserializer 
    {
        bool IsMatch(Type type, string name, string value);

        object Deserialize(Type type, string name, string value);
    }
}
