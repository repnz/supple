using System.Collections.Generic;

namespace Supple.Deserialization
{
    public abstract class Node
    {
        public abstract string Name { get; }
        public abstract string Value { get; }
        public abstract IEnumerable<Node> GetChildren();
        public abstract Node GetChildren(string name);
        public abstract bool HasChildren { get; }
        public abstract void Remove();
    }
}
