using System;

namespace Supple.Xml.Exceptions
{
    public class MemberNotFoundException : Exception
    {
        public string MemberName { get; }
        public Type BaseType { get; }

        public MemberNotFoundException(string memberName, Type baseType)
        {
            MemberName = memberName;
            BaseType = baseType;
        }

        public MemberNotFoundException(string memberName, Type baseType, string message) : base(message)
        {
            MemberName = memberName;
            BaseType = baseType;
        }

        public MemberNotFoundException(string memberName, Type baseType, string message, Exception innerException) : base(message, innerException)
        {
            MemberName = memberName;
            BaseType = baseType;
        }

        public MemberNotFoundException(string memberName, Type baseType, Exception innerException) : base("", innerException)
        {
            MemberName = memberName;
            BaseType = baseType;
        }

        public override string Message => $"Cannot find member {MemberName} in type {BaseType.Name} {base.Message}";
    }
}
