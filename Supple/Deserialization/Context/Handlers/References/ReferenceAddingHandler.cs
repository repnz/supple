using Supple.Deserialization.References;

namespace Supple.Deserialization.Context.Handlers.References
{
    public class ReferenceAddingHandler : INodeHandler
    {
        private readonly IReferenceStore _store;
        private readonly object _instance;

        public ReferenceAddingHandler(object instance, IReferenceStore referenceStore)
        {
            _instance = instance;
            _store = referenceStore;
        }

        public HandleStatus HandleNode(Node node, bool isOptional)
        {
            if (!node.HasChildren && node.Name == "Name")
            {
                _store.Add(node.Value, _instance);
                return HandleStatus.Optional;
            }

            return HandleStatus.Continue;
        }
    }
}
