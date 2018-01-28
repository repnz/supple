using System;
using System.Reflection;

namespace Supple.Reflection
{
    public class Property : Member
    {
        public PropertyInfo PropertyInfo { get; }

        public Property(PropertyInfo info) : base()
        {
            PropertyInfo = info;
        }

        public override bool IsSettable
        {
            get
            {
                if (PropertyInfo.SetMethod == null)
                {
                    return false;
                }

                if (!PropertyInfo.SetMethod.IsPublic)
                {
                    return false;
                }

                return true;
            }
        }

        public override Type MemberType => PropertyInfo.PropertyType;

        public override void SetValue(object instance, object value)
        {
            PropertyInfo.SetValue(instance, value);
        }

    }
}
