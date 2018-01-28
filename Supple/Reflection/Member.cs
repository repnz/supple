using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Supple.Reflection
{
    public abstract class Member
    {
        public abstract bool IsSettable { get; }

        public abstract Type MemberType { get; }

        public abstract void SetValue(object instance, object value);

        public static Member GetMember(Type type, string name)
        {
            IEnumerable<MemberInfo> members = type.GetMember(name,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
                );

            foreach (MemberInfo member in members)
            {
                switch(member.MemberType)
                {
                    case MemberTypes.Property:
                        return new Property(member as PropertyInfo);
                    case MemberTypes.Field:
                        return new Field(member as FieldInfo);
                    default:
                        continue;
                }
            }

            return null;
        }
    }
}
