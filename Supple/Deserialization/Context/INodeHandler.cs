namespace Supple.Deserialization.Context
{
    public interface INodeHandler
    {
        HandleStatus HandleNode(Node node, bool isOptional);
    }
}