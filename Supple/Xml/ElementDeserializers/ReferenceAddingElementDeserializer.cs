using System.Xml.Linq;
using Supple.Xml.References;

namespace Supple.Xml.ElementDeserializers
{
    public class ReferenceAddingElementDeserializer : PropertiesElementDeserializer
    {
        private readonly IReferenceStore _referenceStore;

        public ReferenceAddingElementDeserializer(IElementDeserializer elementDeserializer, 
            IValueDeserializer valueDeserializer, IReferenceStore referenceStore) 
            : base(elementDeserializer, valueDeserializer)
        {
            _referenceStore = referenceStore;
        }

        protected override void HandleAttribute(object instance, XAttribute attribute)
        {
            if (attribute.Name.LocalName == "Name")
            {
                _referenceStore.Add(attribute.Value, instance);

                if (instance.GetType().GetProperty("Name") == null) { return; }
            }
            
            base.HandleAttribute(instance, attribute);
            
        }

        protected override void HandleElement(object instance, XElement element)
        {
            if (element.Name.LocalName == "Name" && !element.HasAttributes && !element.HasElements)
            {
                _referenceStore.Add(element.Value, instance);

                if (instance.GetType().GetProperty("Name") == null) { return; }
            }

            base.HandleElement(instance, element);
        }
    }
}
