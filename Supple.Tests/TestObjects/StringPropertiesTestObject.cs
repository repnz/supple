using System.Collections.Generic;

namespace Supple.Tests.TestObjects
{
    class StringPropertiesTestObject
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            StringPropertiesTestObject other = obj as StringPropertiesTestObject;

            if (other == null)
            {
                return false;
            }

            return other.Name == Name && other.Value == Value;
        }

        public override int GetHashCode()
        {
            int hashCode = -244751520;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
