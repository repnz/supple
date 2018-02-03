using System;

namespace Supple.Deserialization.Context.Handlers.MemberAssign
{
    class MemberNodeHandlerFactory : INodeHandlerFactory
    {
        private readonly IDelegator _delegator;

        public MemberNodeHandlerFactory(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public INodeHandler CreateNodeHandler(object obj)
        {
            return new MemberNodeHandler(obj, _delegator);
        }

        public bool IsMatch(Type type, Node node)
        {
            return true;
        }
    }
}
