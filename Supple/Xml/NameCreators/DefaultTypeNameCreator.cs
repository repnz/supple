using System;

namespace Supple.Xml.NameCreators
{
    public class DefaultTypeNameCreator : ITypeNameCreator
    {
        public string CreateName(Type type)
        {
            return type.Name;
        }

        public bool IsMatch(Type type)
        {
            return true;
        }
    }
}
