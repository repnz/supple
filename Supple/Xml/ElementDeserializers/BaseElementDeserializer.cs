using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using Supple.Xml.InstanceCreators;

namespace Supple.Xml.ElementDeserializers
{
    public abstract class BaseElementDeserializer : IElementDeserializer
    {
        protected abstract void HandleAttribute(object instance, XAttribute attribute);
        protected abstract void HandleElement(object instance, XElement element);
        
        protected IElementDeserializer ElementDeserializer { get; }
        protected IValueDeserializer ValueDeserializer { get; }
        private readonly ConstructorCaller _constructorCaller;

        public BaseElementDeserializer(IElementDeserializer elementDeserializer,
         IValueDeserializer valueDeserializer)
        {
            ElementDeserializer = elementDeserializer;
            ValueDeserializer = valueDeserializer;
            _constructorCaller = new ConstructorCaller(elementDeserializer, valueDeserializer);
        }

        public object Deserialize(Type type, XElement element)
        {
            object instance = CreateInstance(type, element);

            foreach (XAttribute attribute in element.Attributes())
            {
                HandleAttribute(instance, attribute);
            }

            foreach (XElement subElement in element.Elements())
            {
                HandleElement(instance, subElement);
            }

            return instance;
        }

        public virtual bool IsMatch(Type type, XElement element)
        {
            return true;
        }

        protected virtual object CreateInstance(Type type, XElement element)
        {
            return _constructorCaller.CreateInstance(type, element);
        }        
    }
}
