using System;
using System.Linq;
using System.Collections.Generic;
using Supple.Deserialization.InstanceCreators;

namespace Supple.Deserialization.Context
{
    public class ContextNodeDeserializer : INodeDeserializer
    {
        private readonly IEnumerable<INodeHandlerFactory> _handlerFactory;
        private readonly ConstructorCaller _caller;

        public ContextNodeDeserializer(IEnumerable<INodeHandlerFactory> handlerFactories, IDelegator delegator)
        {
            _handlerFactory = handlerFactories;
            _caller = new ConstructorCaller(delegator);
        }

        public object Deserialize(Type type, Node node)
        {
            object obj = _caller.CreateInstance(type, node);

            INodeHandler[] handlers =
                _handlerFactory
                .Where(f => f.IsMatch(type, node))
                .Select(factory => factory.CreateNodeHandler(obj))
                .ToArray();

            if (handlers.Length == 0)
            {
                throw new InvalidOperationException();
            }

            foreach (Node subNode in node.GetChildren())
            {
                HandleStatus status = HandleStatus.Continue;

                foreach (INodeHandler handler in handlers)
                {
                    bool optional = (status == HandleStatus.Optional);

                    switch (handler.HandleNode(subNode, optional))
                    {
                        case HandleStatus.Optional:
                            status = HandleStatus.Optional;
                            break;
                        case HandleStatus.End:
                            status = HandleStatus.End;
                            break;
                        default:
                            break;
                    }

                    if (status == HandleStatus.End)
                    {
                        break;
                    }
                }

                if (status == HandleStatus.Continue)
                {
                    throw new InvalidOperationException();
                }
            }

            return obj;
        }

        public bool IsMatch(Type type, Node node)
        {
            return true;
        }
    }
}
