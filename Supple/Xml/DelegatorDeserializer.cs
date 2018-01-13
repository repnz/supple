using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Supple.Xml
{
    public class DelegatorDeserializer : 
        IElementDeserializer, 
        IValueDeserializer, 
        ITypeNameCreator
    {
        public IList<IValueDeserializer> ValueDeserializers { get; }
        public IList<ITypeNameCreator> TypeNameCreators { get; }
        public IList<IElementDeserializer> ElementDeserializers { get; }

        public DelegatorDeserializer()
        {
            ValueDeserializers = new List<IValueDeserializer>();
            TypeNameCreators = new List<ITypeNameCreator>();
            ElementDeserializers = new List<IElementDeserializer>();
        }

        public string CreateName(Type type)
        {
            foreach (ITypeNameCreator nameCreator in TypeNameCreators)
            {
                if (nameCreator.IsMatch(type))
                {
                    return nameCreator.CreateName(type);
                }
            }

            throw new InvalidOperationException();
        }

        public object Deserialize(Type type, XElement element)
        {
            foreach (IElementDeserializer valueDeserializer in ElementDeserializers)
            {
                if (valueDeserializer.IsMatch(type, element))
                {
                    return valueDeserializer.Deserialize(type, element);
                }
            }

            throw new InvalidOperationException();
        }

        public object Deserialize(Type type, string name, string value)
        {
            foreach (IValueDeserializer valueDeserializer in ValueDeserializers)
            {
                if (valueDeserializer.IsMatch(type, name, value))
                {
                    return valueDeserializer.Deserialize(type, name, value);
                }
            }

            throw new InvalidOperationException();
        }

        public bool IsMatch(Type type, XElement element)
        {
            return true;
        }

        public bool IsMatch(Type type, string name, string value)
        {
            return true;
        }

        public bool IsMatch(Type type)
        {
            return true;
        }
    }
}
