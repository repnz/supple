using Supple.Deserialization.References;
using System;

namespace Supple.Deserialization.Context.Handlers.References
{
    public class ReferenceAddingHandlerFactory : INodeHandlerFactory
    {
        private readonly IReferenceStore _store;

        public ReferenceAddingHandlerFactory(IReferenceStore store)
        {
            _store = store;
        }

        public INodeHandler CreateNodeHandler(object obj)
        {
            return new ReferenceAddingHandler(obj, _store);
        }

        public bool IsMatch(Type type, Node node)
        {
            return true;
        }
    }
}
