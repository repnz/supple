using System;

namespace Supple.Xml.References
{
    class ReferenceVariableDeserializer : IValueDeserializer
    {
        private readonly IReferenceStore _store;

        public ReferenceVariableDeserializer(IReferenceStore store)
        {
            _store = store;
        }

        public bool IsMatch(Type type, string name, string value)
        {
            return value.StartsWith("$");
        }

        public object Deserialize(Type type, string name, string value)
        {
            string varName = value.Substring(1);
            return _store.Get(varName);
        }
    }
}
