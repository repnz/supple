using Supple.Deserialization.Deserializers.Collection;
using System;
using System.Reflection;

namespace Supple.Deserialization.Context.Handlers.Collection
{
    class CollectionNodeHandler : INodeHandler
    {
        private readonly object _obj;
        private readonly Type _elementType;
        private readonly MethodInfo _addMethod;
        private readonly string _elementTypeName;
        private readonly IDelegator _delegator;

        public CollectionNodeHandler(object obj, IDelegator delegator)
        {
            this._obj = obj;
            _delegator = delegator;

            Type collectionBase = CollectionTools.GetCollectionBase(obj.GetType());
            _elementType = CollectionTools.GetCollectionElementType(collectionBase);
            _addMethod = collectionBase.GetMethod("Add");
            _elementTypeName = delegator.CreateName(_elementType);
        }

        public HandleStatus HandleNode(Node subNode, bool isOptional)
        {
            if (subNode.Name != _elementTypeName)
            {
                return HandleStatus.Continue;
            }

            object listItem = _delegator.Deserialize(_elementType, subNode);
            _addMethod.Invoke(_obj, new object[] { listItem });
            return HandleStatus.End;
        }
    }
}
