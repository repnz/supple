using System.Collections.Generic;

namespace Supple.Tests.TestObjects
{
    public class SubProperties
    {
        public string SubPropertyA { get; set; }
        public string SubPropertyB { get; set; }

        public override bool Equals(object obj)
        {
            var properties = obj as SubProperties;
            return properties != null &&
                   SubPropertyA == properties.SubPropertyA &&
                   SubPropertyB == properties.SubPropertyB;
        }

        public override int GetHashCode()
        {
            var hashCode = -489567231;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SubPropertyA);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SubPropertyB);
            return hashCode;
        }
    }
}
