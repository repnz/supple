using System;
using System.Xml.Linq;

namespace Supple.Xml
{
    public interface IElementDeserializer
    {
        bool IsMatch(Type type, XElement element);

        object Deserialize(Type type, XElement element);
    }
}
