using Supple.Collections;
using System.Collections.Generic;

namespace Supple.Tests.TestObjects
{
    class ObjectWithListProperty
    {
        public List<string> Elements { get; set; }

        public override bool Equals(object obj)
        {
            var property = obj as ObjectWithListProperty;
            return property != null && (Elements == property.Elements ||
                EnumerableExtentions.IsItemsEqual(Elements, property.Elements));
        }

        public override int GetHashCode()
        {
            return 1573927372 + EqualityComparer<List<string>>.Default.GetHashCode(Elements);
        }
    }
}
