using System;
using System.Collections.Generic;

namespace Supple.Deserialization
{
    class ValueNode : Node
    {
        public override string Name { get; }

        public override string Value { get; }

        public override bool HasChildren => false;

        public ValueNode(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override IEnumerable<Node> GetChildren()
        {
            return new Node[0];    
        }

        public override Node GetChildren(string name)
        {
            return null;
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
