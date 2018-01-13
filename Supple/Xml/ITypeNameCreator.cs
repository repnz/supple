using System;

namespace Supple.Xml
{
    public interface ITypeNameCreator
    {
        string CreateName(Type type);
        bool IsMatch(Type type);
    }
}
