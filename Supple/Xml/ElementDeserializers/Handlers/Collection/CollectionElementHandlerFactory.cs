using Supple.Xml.Collection;
using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.Collection
{
    class CollectionElementHandlerFactory : IElementHandlerFactory
    {
        private readonly IDelegator _delegator;

        public CollectionElementHandlerFactory(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public IElementHandler CreateElementHandler(object obj)
        {
            return new CollectionElementHandler(obj, _delegator);       
        }

        public bool IsMatch(Type type, XElement element)
        {
            return CollectionTools.HasCollectionBase(type);
        }
    }
}
