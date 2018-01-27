using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Supple.Xml.ElementDeserializers
{
    public class HandlersCallerElementDeserializer : IElementDeserializer
    {
        private readonly IEnumerable<IElementHandlerFactory> _handlerFactory;
        private readonly ConstructorCaller _caller;

        public HandlersCallerElementDeserializer(IEnumerable<IElementHandlerFactory> handlerFactories, IDelegator delegator)
        {
            _handlerFactory = handlerFactories;
            _caller = new ConstructorCaller(delegator);
        }

        public object Deserialize(Type type, XElement element)
        {
            object obj = _caller.CreateInstance(type, element);

            IElementHandler[] handlers =
                _handlerFactory
                .Where(f => f.IsMatch(type, element))
                .Select(factory => factory.CreateElementHandler(obj))
                .ToArray();

            if (handlers.Length == 0)
            {
                throw new InvalidOperationException();
            }

            foreach (XElement subElement in element.Elements())
            {
                HandleStatus status = HandleStatus.Continue;

                foreach (IElementHandler handler in handlers)
                {
                    bool optional = (status == HandleStatus.Optional);

                    switch (handler.HandleElement(subElement, optional))
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
            }


            foreach (XAttribute attribute in element.Attributes())
            {
                HandleStatus status = HandleStatus.Continue;

                foreach (IElementHandler elementHandler in handlers)
                {
                    bool optional = (status == HandleStatus.Optional);

                    switch (elementHandler.HandleAttribute(attribute, optional))
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
            }

            return obj;
        }

        public bool IsMatch(Type type, XElement element)
        {
            return true;
        }
    }
}
