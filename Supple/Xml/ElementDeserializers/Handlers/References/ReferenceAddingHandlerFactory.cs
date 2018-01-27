using Supple.Xml.References;
using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.References
{
    public class ReferenceAddingHandlerFactory : IElementHandlerFactory
    {
        private readonly IReferenceStore _store;

        public ReferenceAddingHandlerFactory(IReferenceStore store)
        {
            _store = store;
        }

        public IElementHandler CreateElementHandler(object obj)
        {
            return new ReferenceAddingHandler(obj, _store);
        }

        public bool IsMatch(Type type, XElement element)
        {
            return true;
        }
    }
}
