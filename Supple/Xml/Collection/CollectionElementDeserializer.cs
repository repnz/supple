using Supple.Xml.Exceptions;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.Collection
{
    public class CollectionElementDeserializer : IElementDeserializer
    {
        private readonly IElementDeserializer _elementDeserializer;
        private readonly ITypeNameCreator _nameCreator;

        public CollectionElementDeserializer(IElementDeserializer elementDeserializer,
            ITypeNameCreator nameCreator)
        {
            _elementDeserializer = elementDeserializer;
            _nameCreator = nameCreator;
        }

        public object Deserialize(Type type, XElement element)
        {
            Type collectionBase = CollectionTools.GetCollectionBase(type);
            Type elementType = CollectionTools.GetCollectionElementType(collectionBase);

            object list = Activator.CreateInstance(type);
            MethodInfo method = collectionBase.GetMethod("Add");
            string elementTypeName = _nameCreator.CreateName(elementType);

            foreach (XElement subElement in element.Elements())
            {
                // validate subElement Name
                if (subElement.Name.LocalName != elementTypeName)
                {
                    throw new UnexpectedElementException(elementTypeName, subElement);
                }

                object obj = _elementDeserializer.Deserialize(elementType, subElement);
                method.Invoke(list, new object[] { obj });
            }
             
            return list;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return CollectionTools.HasCollectionBase(type);
        }
    }
}
