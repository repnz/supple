using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    class ElementValueDeserializer : IElementDeserializer
    {
        private readonly IValueDeserializer _valueDeserializer;

        public ElementValueDeserializer(IValueDeserializer valueDeserializer)
        {
            _valueDeserializer = valueDeserializer;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return !element.HasElements && !element.HasAttributes;
        }

        public object Deserialize(Type type, XElement element)
        { 
            return _valueDeserializer.Deserialize(type, element.Name.LocalName, element.Value);
        }
    }
}
