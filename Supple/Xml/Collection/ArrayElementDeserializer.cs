using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections;

namespace Supple.Xml.Collection
{
    class ArrayElementDeserializer : IElementDeserializer
    {
        private IElementDeserializer _elementDeserializer;

        public ArrayElementDeserializer(IElementDeserializer deserializer)
        {
            _elementDeserializer = deserializer;
        }

        public object Deserialize(Type type, XElement element)
        {
            Type elementType = type.GetElementType();
            int arrayLength = element.Elements().Count();
            IList arr = (IList)Activator.CreateInstance(type, new object[] { arrayLength });
            
            int curIndex = 0;

            foreach (XElement subElement in element.Elements())
            {
                arr[curIndex] = _elementDeserializer.Deserialize(elementType, subElement);
                curIndex++;
            }

            return arr;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return type.IsArray;
        }
    }
}
