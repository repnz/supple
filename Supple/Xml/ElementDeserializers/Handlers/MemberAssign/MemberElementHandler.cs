using Supple.Reflection;
using Supple.Xml.Exceptions;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace Supple.Xml.ElementDeserializers.Handlers.MemberAssign
{
    class MemberElementHandler : IElementHandler
    {
        private readonly object _instance;
        private readonly IDelegator _delegator;

        public MemberElementHandler(object obj, IDelegator delegator)
        {
            _instance = obj;
            _delegator = delegator;
        }

        public HandleStatus HandleAttribute(XAttribute attribute, bool isOptional)
        {
            Member member = GetMember(
                _instance.GetType(),
                attribute.Name.LocalName,
                isOptional
                );

            if (member == null) { return HandleStatus.Continue; }

            object deserializedObject = _delegator.Deserialize(
                member.MemberType,
                attribute.Name.LocalName,
                attribute.Value
                );

            member.SetValue(_instance, deserializedObject);
            return HandleStatus.Optional;
        }

        public HandleStatus HandleElement(XElement element, bool isOptional)
        {
            Member member = GetMember(_instance.GetType(), element.Name.LocalName, isOptional);

            if (member == null) { return HandleStatus.Continue; }

            object deserializedObject = _delegator.Deserialize(
                member.MemberType,
                element
                );

            member.SetValue(_instance, deserializedObject);
            return HandleStatus.Optional;
        }

        private Member GetMember(Type type, string memberName, 
            bool optional)
        {
            Member member = Member.GetMember(type, memberName);
            
            if (member == null)
            {
                if (optional)
                {
                    return null;
                }

                throw new MemberNotFoundException(memberName, type);
            }
            if (!member.IsSettable && !optional)
            {
                throw new MemberNotFoundException(memberName, type,
                    "Member Is Not Settable");
            }

            return member;
        }
    }
}
