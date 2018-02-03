using Supple.Deserialization.References;
using System;

namespace Supple.Deserialization.Deserializers
{
    class ReferenceVariableDeserializer : ValueDeserializer
    {
        private readonly IReferenceStore _store;

        public ReferenceVariableDeserializer(IReferenceStore store)
        {
            _store = store;
        }

        protected override bool IsMatch(Type type, string name, string value)
        {
            return value.StartsWith("$");
        }

        protected override object Deserialize(Type type, string name, string value)
        {
            string varName = value.Substring(1);
            return _store.Get(varName);
        }
    }
}
