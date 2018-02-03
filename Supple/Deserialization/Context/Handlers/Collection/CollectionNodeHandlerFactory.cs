using Supple.Deserialization.Deserializers.Collection;
using System;

namespace Supple.Deserialization.Context.Handlers.Collection
{
    class CollectionNodeHandlerFactory : INodeHandlerFactory
    {
        private readonly IDelegator _delegator;

        public CollectionNodeHandlerFactory(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public INodeHandler CreateNodeHandler(object obj)
        {
            return new CollectionNodeHandler(obj, _delegator);       
        }

        public bool IsMatch(Type type, Node node)
        {
            return CollectionTools.HasCollectionBase(type);
        }
    }
}
