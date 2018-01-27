using Supple.Xml.Collection;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.Collection
{
    class CollectionElementHandler : IElementHandler
    {
        private readonly object _obj;
        private readonly Type _elementType;
        private readonly MethodInfo _addMethod;
        private readonly string _elementTypeName;
        private readonly IDelegator _delegator;

        public CollectionElementHandler(object obj, IDelegator delegator)
        {
            this._obj = obj;
            _delegator = delegator;

            Type collectionBase = CollectionTools.GetCollectionBase(obj.GetType());
            _elementType = CollectionTools.GetCollectionElementType(collectionBase);
            _addMethod = collectionBase.GetMethod("Add");
            _elementTypeName = delegator.CreateName(_elementType);
        }

        public HandleStatus HandleAttribute(XAttribute attribute, bool isOptional)
        {
            return HandleStatus.Continue;
        }

        public HandleStatus HandleElement(XElement element, bool isOptional)
        {
            if (element.Name.LocalName != _elementTypeName)
            {
                return HandleStatus.Continue;
            }

            object listItem = _delegator.Deserialize(_elementType, element);
            _addMethod.Invoke(_obj, new object[] { listItem });
            return HandleStatus.End;
        }
    }
}
