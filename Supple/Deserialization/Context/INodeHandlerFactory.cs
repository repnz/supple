using System;

namespace Supple.Deserialization.Context
{
    public interface INodeHandlerFactory
    {
        INodeHandler CreateNodeHandler(object obj);

        bool IsMatch(Type type, Node node);
    }
}
