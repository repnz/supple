using Supple.Xml.Exceptions;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.PropertyAssign
{
    class PropertyElementHandler : IElementHandler
    {
        private readonly object _obj;
        private readonly IDelegator _delegator;

        public PropertyElementHandler(object obj, IDelegator delegator)
        {
            _obj = obj;
            _delegator = delegator;
        }

        public HandleStatus HandleAttribute(XAttribute attribute, bool isOptional)
        {
            PropertyInfo property = GetProperty(
                _obj.GetType(),
                attribute.Name.LocalName,
                isOptional
                );

            if (property == null) { return HandleStatus.Continue; }

            object deserializedObject = _delegator.Deserialize(
                property.PropertyType,
                attribute.Name.LocalName,
                attribute.Value
                );

            property.SetValue(_obj, deserializedObject);
            return HandleStatus.Optional;
        }

        public HandleStatus HandleElement(XElement element, bool isOptional)
        {
            PropertyInfo property = GetProperty(_obj.GetType(), element.Name.LocalName, isOptional);

            if (property == null) { return HandleStatus.Continue; }

            object deserializedObject = _delegator.Deserialize(
                property.PropertyType,
                element
                );

            property.SetValue(_obj, deserializedObject);
            return HandleStatus.Optional;
        }

        private PropertyInfo GetProperty(Type type, string propertyName, 
            bool optional)
        {
            PropertyInfo prop = type.GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.Instance
                );

            if (prop == null)
            {
                if (optional)
                {
                    return null;
                }

                throw new PropertyNotFoundException(propertyName, type);
            }
            if (prop.SetMethod == null && !optional)
            {
                throw new PropertyNotFoundException(propertyName, type,
                    "Property Does Not Contain Public Setter");
            }

            return prop;
        }
    }
}
