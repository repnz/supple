using System;
using System.Reflection;

namespace Supple.Xml.Collection
{
    class CollectionValueDeserializer : IValueDeserializer
    {
        private readonly IValueDeserializer _valueDeserializer;
        private readonly ITypeNameCreator _nameCreator;

        public CollectionValueDeserializer(IValueDeserializer valueDeserializer, 
            ITypeNameCreator nameCreator)
        {
            _valueDeserializer = valueDeserializer;
            _nameCreator = nameCreator;
        }

        public object Deserialize(Type type, string name, string value)
        {
            Type listBase = CollectionTools.GetCollectionBase(type);
            Type elementType = CollectionTools.GetCollectionElementType(listBase);
            object instance = Activator.CreateInstance(type);
            MethodInfo method = listBase.GetMethod("Add");
            string elementTypeName = _nameCreator.CreateName(elementType);

            // ignore open and close brackets 
            value = value.Substring(1, value.Length - 2);

            foreach (string listElementValue in value.Split(','))
            {
                object listItem = _valueDeserializer.Deserialize(
                    elementType,
                    elementTypeName,
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
