using Supple.Xml.Exceptions;
using System;
using System.Xml.Linq;

namespace Supple.Xml
{
    public class InterfaceDeserializer : IElementDeserializer
    {
        private readonly IRuntimeTypeResolver _typeResolver;
        private readonly IElementDeserializer _elementDeserializer;

        public InterfaceDeserializer(IRuntimeTypeResolver typeResolver, 
            IElementDeserializer elementDeserializer)
        {
            _elementDeserializer = elementDeserializer;
            _typeResolver = typeResolver;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return type.IsInterface || type.IsAbstract;
        }

        public object Deserialize(Type type, XElement element)
        {
            XAttribute typeNameAttr = element.Attribute(XName.Get("Type"));

            if (typeNameAttr == null)
            {
                throw new InterfaceTypeNotFoundException(type, element);
            }

            string typeName = typeNameAttr.Value;
            typeNameAttr.Remove();

            try
            {
                type = _typeResolver.GetType(typeName);
            }
            catch (Exception e)
            {
                throw new RuntimeTypeException(typeName, e);
            }
            
            return _elementDeserializer.Deserialize(type, element);
        }
    }
}
