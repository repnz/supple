using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    public interface IElementHandlerFactory
    {
        IElementHandler CreateElementHandler(object obj);

        bool IsMatch(Type type, XElement element);
    }
}
