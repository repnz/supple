using System;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    public class PropertiesElementDeserializer : BaseElementDeserializer
    {

        public PropertiesElementDeserializer(IElementDeserializer elementDeserializer,
         IValueDeserializer valueDeserializer) : base(elementDeserializer, valueDeserializer)
        {
        }

        private PropertyInfo GetProperty(Type type, string propertyName)
        {
            return type.GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.Instance
                );
        }

        protected override void HandleAttribute(object instance, XAttribute attribute)
        {
            PropertyInfo property = GetProperty(instance.GetType(), attribute.Name.LocalName);

            object deserializedObject = ValueDeserializer.Deserialize(
                property.PropertyType,
                attribute.Name.LocalName,
                attribute.Value
                );

            property.SetValue(instance, deserializedObject);
        }

        protected override void HandleElement(object instance, XElement element)
        {
            PropertyInfo property = GetProperty(instance.GetType(), element.Name.LocalName);

            object deserializedObject = ElementDeserializer.Deserialize(
                property.PropertyType,
                element
                );

            property.SetValue(instance, deserializedObject);
        }
    }
}
