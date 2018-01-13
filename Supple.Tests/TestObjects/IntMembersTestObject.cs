using System;

namespace Supple.Tests.TestObjects
{
    class IntMembersTestObject : IEquatable<IntMembersTestObject>
    {
        public int IntMember { get; set; }
        public uint UIntMember { get; set; }
        public float FloatMember { get; set; }
        public double DoubleMember { get; set; }
        public byte ByteMember { get; set; }


        public override bool Equals(object obj)
        {
            return Equals(obj as IntMembersTestObject);
        }

        public bool Equals(IntMembersTestObject other)
        {
            return other != null &&
                   IntMember == other.IntMember &&
                   UIntMember == other.UIntMember &&
                   FloatMember == other.FloatMember &&
                   DoubleMember == other.DoubleMember &&
                   ByteMember == other.ByteMember;
        }

        public override int GetHashCode()
        {
            var hashCode = 570438729;
            hashCode = hashCode * -1521134295 + IntMember.GetHashCode();
            hashCode = hashCode * -1521134295 + UIntMember.GetHashCode();
            hashCode = hashCode * -1521134295 + FloatMember.GetHashCode();
            hashCode = hashCode * -1521134295 + DoubleMember.GetHashCode();
            hashCode = hashCode * -1521134295 + ByteMember.GetHashCode();
            return hashCode;
        }
    }
}
