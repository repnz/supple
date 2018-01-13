using System;

namespace Supple.Xml.List
{
    class ListValueDeserializer : IValueDeserializer
    {
        private readonly IValueDeserializer _valueDeserializer;

        public ListValueDeserializer(IValueDeserializer valueDeserializer)
        {
            _valueDeserializer = valueDeserializer;
        }

        public object Deserialize(Type type, string name, string value)
        {
            Type listBase = ListTools.GetListBase(type);
            Type elementType = ListTools.GetListElementType(listBase);
            System.Collections.IList list = 
                (System.Collections.IList)Activator.CreateInstance(type);

            foreach (string listElementValue in value.Split(','))
            {
                object listItem = _valueDeserializer.Deserialize(
                    elementType,
                    elementType.Name,
                    listElementValue
                    );

                list.Add(listItem);
            }

            return list;
        }
        public bool IsMatch(Type type, string name, string value)
        {
            return ListTools.HasListBase(type);
        }
    }
}
