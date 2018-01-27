using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Supple.Xml
{
    public class DelegatorDeserializer : IDelegator
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

            throw new InvalidOperationException($"Missing ITypeNameCreator to match {type.Name}");
        }

        public object Deserialize(Type type, XElement element)
        {
            foreach (IElementDeserializer elementDeserializer in ElementDeserializers)
            {
                if (elementDeserializer.IsMatch(type, element))
                {
                    return elementDeserializer.Deserialize(type, element);
                }
            }

            throw new InvalidOperationException($"Missing ElementDeserializer to match {type.Name}, {element.Name}");
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

            throw new InvalidOperationException($"Missing ValueDeserializer to match {type.Name}, {name}");
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
