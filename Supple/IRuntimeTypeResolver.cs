using System;

namespace Supple
{
    public interface IRuntimeTypeResolver
    {
        Type GetType(string typeName);
    }
}
