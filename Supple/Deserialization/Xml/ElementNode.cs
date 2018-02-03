using System.Collections.Generic;
using System.Xml.Linq;

namespace Supple.Deserialization.Xml
{
    class ElementNode : Node
    {
        private readonly XElement _element;

        public ElementNode(XElement element)
        {
            _element = element;
        }

        public override string Name => _element.Name.LocalName;

        public override string Value => _element.Value;

        public override bool HasChildren => _element.HasAttributes || _element.HasElements;

        public override IEnumerable<Node> GetChildren()
        {
            List<Node> nodes = new List<Node>();

            foreach (XAttribute attribute in _element.Attributes())
            {
                nodes.Add(new AttributeNode(attribute));
            }

            foreach (XElement subElement in _element.Elements())
            {
                nodes.Add(new ElementNode(subElement));
            }

            return nodes;
        }

        public override Node GetChildren(string name)
        {
            XName xName = XName.Get(name);
            var attr = _element.Attribute(xName);

            if (attr != null)
            {
                return new AttributeNode(attr);
            }

            var elem = _element.Element(xName);

            if (elem != null)
            {
                return new ElementNode(elem);
            }

            return null;
        }

        public override void Remove()
        {
            _element.Remove();
        }
    }
}
