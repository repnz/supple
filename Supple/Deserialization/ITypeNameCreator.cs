using System;

namespace Supple.Deserialization
{
    public interface ITypeNameCreator
    {
        string CreateName(Type type);
        bool IsMatch(Type type);
    }
}
