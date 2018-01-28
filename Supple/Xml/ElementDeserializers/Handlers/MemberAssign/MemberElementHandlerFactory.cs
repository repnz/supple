using System;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.MemberAssign
{
    class MemberElementHandlerFactory : IElementHandlerFactory
    {
        private readonly IDelegator _delegator;

        public MemberElementHandlerFactory(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public IElementHandler CreateElementHandler(object obj)
        {
            return new MemberElementHandler(obj, _delegator);
        }

        public bool IsMatch(Type type, XElement element)
        {
            return true;
        }
    }
}
