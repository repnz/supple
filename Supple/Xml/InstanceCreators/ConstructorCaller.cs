using Supple.Xml.Exceptions;
using Supple.Xml.InstanceCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    class ConstructorCaller : IInstanceCreator
    {
        protected IElementDeserializer ElementDeserializer { get; }
        protected IValueDeserializer ValueDeserializer { get; }

        public ConstructorCaller(IElementDeserializer elementDeserializer, 
            IValueDeserializer valueDeserializer)
        {
            ElementDeserializer = elementDeserializer;
            ValueDeserializer = valueDeserializer;
        }

        public object CreateInstance(Type type, XElement element)
        {
            ConstructorInfo[] ctors = type.GetConstructors().Where(c => c.GetParameters().Length != 0).ToArray();

            if (ctors.Length == 0) { return Activator.CreateInstance(type); }

            ISet<string> names = GetNamesSet(element);

            foreach (ConstructorInfo ctor in ctors.OrderByDescending(c => c.GetParameters().Length))
            {
                if (MatchConstructor(type, ctor, element, names))
                {
                    return CallConstructor(type, ctor, element);
                }
            }

            ConstructorInfo defaultCtor = type.GetConstructor(new Type[] { });

            if (defaultCtor == null)
            {
                throw new ConstructorNotFoundException(element, type);
            }
            else
            {
                return defaultCtor.Invoke(new object[] { });
            }
            
        }

        private ISet<string> GetNamesSet(XElement element)
        {
            ISet<string> names = new HashSet<string>();

            foreach (var subElement in element.Elements())
            {
                names.Add(subElement.Name.LocalName.ToLower());
            }

            foreach (var attribute in element.Attributes())
            {
                names.Add(attribute.Name.LocalName.ToLower());
            }

            return names;
        }

        private bool MatchConstructor(Type type, ConstructorInfo info, XElement element, ISet<string> names)
        {
            foreach (ParameterInfo parameter in info.GetParameters())
            {
                string s = parameter.Name.ToLower();

                if (!names.Contains(s)) { return false; }
            }

            return true;
        }

        private bool TryGetElementIgnoreCase(XElement element, string name, out XElement outElement)
        {
            foreach (XElement subElement in element.Elements())
            {
                if (subElement.Name.LocalName.ToLower() == name)
                {
                    outElement = subElement;
                    return true;
                }
            }

            outElement = null;
            return false;
        }

        private object CallConstructor(Type type, ConstructorInfo info, XElement element)
        {
            List<object> parameters = new List<object>(info.GetParameters().Length);

            foreach (ParameterInfo parameter in info.GetParameters())
            {
                string paramName = parameter.Name.ToLower();

                if (TryGetElementIgnoreCase(element, paramName, out XElement subElement))
                {
                    object paramDeserialized = ElementDeserializer.Deserialize(
                        parameter.ParameterType,
                        subElement
                        );

                    parameters.Add(paramDeserialized);
                    subElement.Remove();
                    continue;
                }
                
                if (TryGetAttributeIgnoreCase(element, paramName, out XAttribute subAttribute))
                {
                    object paramDeserialized = ValueDeserializer.Deserialize(
                            parameter.ParameterType,
                            subAttribute.Name.LocalName,
                            subAttribute.Value
                            );

                    parameters.Add(paramDeserialized);
                    subAttribute.Remove();
                    continue;
                }

                throw new InvalidOperationException();
            }

            return info.Invoke(parameters.ToArray());
        }

        private bool TryGetAttributeIgnoreCase(XElement element, string paramName, out XAttribute outAttribute)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.ToLower() == paramName)
                {
                    outAttribute = attribute;
                    return true;
                }
            }

            outAttribute = null;
            return false;
        }
    }
}
