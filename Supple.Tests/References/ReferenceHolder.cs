using System;

namespace Supple.Tests.References
{
    class ReferenceHolder : IEquatable<ReferenceHolder>
    {
        public NamedElement A { get; set; }
        public NamedElement B { get; set; }

        public bool Equals(ReferenceHolder holder)
        {
            return A.Equals(holder.A) && B.Equals(holder.B);
        }
    }
}
