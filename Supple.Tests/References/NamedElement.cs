using System;

namespace Supple.Tests.References
{
    class NamedElement : IEquatable<NamedElement>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(NamedElement other)
        {
            return Name == other.Name && Value == other.Value;
        }
    }
}
