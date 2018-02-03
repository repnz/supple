using Supple.Deserialization;
using Supple.Deserialization.NameCreators;
using System;
using System.Collections.Generic;

namespace Supple.TypeResolvers
{
    public class StaticTypeResolver : IRuntimeTypeResolver
    {
        private readonly IDictionary<string, Type> _types;
        private readonly ITypeNameCreator _nameCreator;

        public StaticTypeResolver(IDictionary<string, Type> types)
        {
            _types = types;
            _nameCreator = new GenericNameCreator();
        }

        public StaticTypeResolver() : this(new Dictionary<string, Type>()) { }

        public void AddType<T>()
        {
            _types.Add(_nameCreator.CreateName(typeof(T)), typeof(T));
        }

        public void AddType<T>(string typeName)
        {
            _types.Add(typeName, typeof(T));
        }

        public Type GetType(string typeName)
        {
            return _types[typeName];
        }
    }
}
