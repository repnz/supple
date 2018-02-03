using System;
using System.Collections.Generic;

namespace Supple.Deserialization.NameCreators
{
    public class KeyValuePairNameCreator : ITypeNameCreator
    {
        public string CreateName(Type type)
        {
            return "Pair";
        }

        public bool IsMatch(Type type)
        {
            return type.IsGenericType && 
                type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }
    }
}
