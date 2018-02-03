using System.Collections.Generic;
using System.Xml.Linq;

namespace Supple.Deserialization.Xml
{
    class AttributeNode : Node
    {
        private readonly XAttribute _attribute;

        public AttributeNode(XAttribute attribute)
        {
            _attribute = attribute;
        }

        public override string Name => _attribute.Name.LocalName;

        public override string Value => _attribute.Value;

        public override bool HasChildren => false;

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
            _attribute.Remove();
        }
    }
}
