using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers
{
    public abstract class BaseElementDeserializer : IElementDeserializer
    {
        protected abstract void HandleAttribute(object instance, XAttribute attribute);
        protected abstract void HandleElement(object instance, XElement element);

        public object Deserialize(Type type, XElement element)
        {
            object instance = Activator.CreateInstance(type);

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

        
    }
}
