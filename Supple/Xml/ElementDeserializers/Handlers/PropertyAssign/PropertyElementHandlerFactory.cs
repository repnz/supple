using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.PropertyAssign
{
    class PropertyElementHandlerFactory : IElementHandlerFactory
    {
        private readonly IDelegator _delegator;

        public PropertyElementHandlerFactory(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public IElementHandler CreateElementHandler(object obj)
        {
            return new PropertyElementHandler(obj, _delegator);
        }

        public bool IsMatch(Type type, XElement element)
        {
            return true;
        }
    }
}
