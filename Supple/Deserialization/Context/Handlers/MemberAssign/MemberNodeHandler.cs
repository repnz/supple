using Supple.Reflection;
using Supple.Deserialization.Exceptions;
using System;

namespace Supple.Deserialization.Context.Handlers.MemberAssign
{
    class MemberNodeHandler : INodeHandler
    {
        private readonly object _instance;
        private readonly IDelegator _delegator;

        public MemberNodeHandler(object obj, IDelegator delegator)
        {
            _instance = obj;
            _delegator = delegator;
        }

        public HandleStatus HandleNode(Node node, bool isOptional)
        {
            Member member = GetMember(
                _instance.GetType(),
                node.Name,
                isOptional
                );

            if (member == null) { return HandleStatus.Continue; }

            object deserializedObject = _delegator.Deserialize(
                member.MemberType,
                node
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
