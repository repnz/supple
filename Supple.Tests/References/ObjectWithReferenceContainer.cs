using Supple.Collections;
using System;
using System.Collections.Generic;

namespace Supple.Tests.References
{
    class ObjectWithReferenceContainer : IEquatable<ObjectWithReferenceContainer>
    {
        public List<NamedElement> NamedElements { get; set; }
        public ReferenceHolder Holder { get; set; }

        public bool Equals(ObjectWithReferenceContainer other)
        {
            return EnumerableExtentions.IsItemsEqual(NamedElements, other.NamedElements) &&
                            Holder.Equals(other.Holder);
                            
        }
    }
}
