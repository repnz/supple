using Supple.Xml.References;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.References
{
    public class ReferenceAddingHandler : IElementHandler
    {
        private readonly IReferenceStore _store;
        private readonly object _instance;

        public ReferenceAddingHandler(object instance, IReferenceStore referenceStore)
        {
            _instance = instance;
            _store = referenceStore;
        }

        public HandleStatus HandleAttribute(XAttribute attribute, bool isOptional)
        {
            if (attribute.Name.LocalName == "Name")
            {
                _store.Add(attribute.Value, _instance);
                return HandleStatus.Optional;
            }

            return HandleStatus.Continue;
        }

        public HandleStatus HandleElement(XElement element, bool isOptional)
        {
            if (element.Name.LocalName == "Name" && !element.HasAttributes && !element.HasElements)
            {
                _store.Add(element.Value, _instance);
                return HandleStatus.Optional;
            }

            return HandleStatus.Continue;
        }
    }
}
