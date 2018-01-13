using System;

namespace Supple.Xml.NameCreators
{
    class InterfaceNameCreator : ITypeNameCreator
    {
        public string CreateName(Type type)
        {
            return type.Name.Substring(1);
        }

        public bool IsMatch(Type type)
        {
            return type.IsInterface && type.Name.StartsWith("I");
        }
    }
}
