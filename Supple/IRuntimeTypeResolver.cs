using System;

namespace Supple
{
    public interface IRuntimeTypeResolver
    {
        void AddType<T>();

        Type GetType(string typeName);
    }
}
