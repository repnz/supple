using System;
using System.Xml.Linq;

namespace Supple.Xml.List
{
    public class ListElementDeserializer : IElementDeserializer
    {
        private readonly IElementDeserializer _elementDeserializer;
        private readonly ITypeNameCreator _nameCreator;

        public ListElementDeserializer(IElementDeserializer elementDeserializer,
            ITypeNameCreator nameCreator)
        {
            _elementDeserializer = elementDeserializer;
            _nameCreator = nameCreator;
        }

        public object Deserialize(Type type, XElement element)
        {
            Type collectionBase = ListTools.GetListBase(type);
            Type elementType = ListTools.GetListElementType(collectionBase);
            System.Collections.IList list = 
                (System.Collections.IList)Activator.CreateInstance(type);

            string elementTypeName = _nameCreator.CreateName(elementType);

            foreach (XElement subElement in element.Elements())
            {
                // validate subElement Name
                if (subElement.Name.LocalName != elementTypeName)
                {
                    throw new InvalidOperationException();
                }

                object obj = _elementDeserializer.Deserialize(elementType, subElement);

                list.Add(obj);
            }

            return list;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return ListTools.HasListBase(type);
        }
    }
}
