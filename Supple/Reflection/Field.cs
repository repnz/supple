using System;
using System.Reflection;

namespace Supple.Reflection
{
    public class Field : Member
    {
        public FieldInfo FieldInfo { get; }

        public Field(FieldInfo info)
        {
            FieldInfo = info;
        }

        public override bool IsSettable => !FieldInfo.IsInitOnly && FieldInfo.IsPublic;

        public override Type MemberType => FieldInfo.FieldType;

        public override void SetValue(object instance, object value)
        {
            FieldInfo.SetValue(instance, value);
        }
    }
}
