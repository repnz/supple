using System;
using System.Collections;
using System.Linq;

namespace Supple.Deserialization.Deserializers.Collection
{
    class ArrayValueDeserializer : ValueDeserializer
    {
        private readonly IDelegator _delegator;

        public ArrayValueDeserializer(IDelegator delegator)
        {
            _delegator = delegator;
        }

        protected override object Deserialize(Type type, string name, string value)
        {
            value = value.Substring(1, value.Length- 2).Trim();
             Type elementType = type.GetElementType();
            int arrayLength = value.Count(c => c == ',') + 1;
            IList arr = (IList)Activator.CreateInstance(type, new object[] { arrayLength });
            int curIndex = 0;
            string elementName = _delegator.CreateName(elementType);

            foreach (string elementValue in value.Split(','))
            {
                arr[curIndex] = _delegator.Deserialize(elementType, new ValueNode(elementName, elementValue));
                curIndex++;
            }

            return arr;
        }

        protected override bool IsMatch(Type type, string name, string value)
        {
            return type.IsArray && value.StartsWith("[") && value.EndsWith("]");
        }
    }
}
