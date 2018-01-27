using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    public interface IElementHandler
    {
        HandleStatus HandleElement(XElement element, bool isOptional);

        HandleStatus HandleAttribute(XAttribute attribute, bool isOptional);
    }
}