using System;

namespace Supple.TypeResolvers
{
    public interface IRuntimeTypeResolver
    {
        Type GetType(string typeName);
    }
}
