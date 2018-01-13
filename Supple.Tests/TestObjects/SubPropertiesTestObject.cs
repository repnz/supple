using System.Collections.Generic;

namespace Supple.Tests.TestObjects
{
    public class SubPropertiesTestObject
    {
        public SubProperties Sub { get; set; }

        public override bool Equals(object other)
        {
            SubPropertiesTestObject otherObject = other as SubPropertiesTestObject;
            return otherObject != null &&
                   EqualityComparer<SubProperties>.Default.Equals(Sub, otherObject.Sub);
        }

        public override int GetHashCode()
        {
            return 52332029 + EqualityComparer<SubProperties>.Default.GetHashCode(Sub);
        }
    }
}
