using System;
using System.Reflection;

namespace Supple.Xml.Collection
{
    class CollectionValueDeserializer : IValueDeserializer
    {
        private readonly IValueDeserializer _valueDeserializer;

        public CollectionValueDeserializer(IValueDeserializer valueDeserializer)
        {
            _valueDeserializer = valueDeserializer;
        }

        public object Deserialize(Type type, string name, string value)
        {
            Type listBase = CollectionTools.GetCollectionBase(type);
            Type elementType = CollectionTools.GetCollectionElementType(listBase);
            object instance = Activator.CreateInstance(type);
            MethodInfo method = listBase.GetMethod("Add");

            // ignore open and close brackets 
            value = value.Substring(1, value.Length - 2);

            foreach (string listElementValue in value.Split(','))
            {
                object listItem = _valueDeserializer.Deserialize(
                    elementType,
                    elementType.Name,
                    listElementValue.Trim()
                    );

                method.Invoke(instance, new object[] { listItem });
            }

            return instance;
        }
        public bool IsMatch(Type type, string name, string value)
        {
            return CollectionTools.HasCollectionBase(type) && 
                value.StartsWith("[") && 
                value.EndsWith("]");
        }
    }
}
