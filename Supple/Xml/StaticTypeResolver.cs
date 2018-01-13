using System;
using System.Collections.Generic;

namespace Supple.Xml
{
    public class StaticTypeResolver : IRuntimeTypeResolver
    {
        private readonly IDictionary<string, Type> _types;

        public StaticTypeResolver(IDictionary<string, Type> types)
        {
            _types = types;
        }

        public StaticTypeResolver() : this(new Dictionary<string, Type>()) { }

        public void AddType<T>()
        {
            _types.Add(typeof(T).Name, typeof(T));
        }

        public Type GetType(string typeName)
        {
            return _types[typeName];
        }
    }
}
