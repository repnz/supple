using System;
using System.Collections.Generic;

namespace Supple.Xml.References
{
    class ReferenceStore : IReferenceStore
    {
        private readonly Dictionary<string, object> _references;

        public ReferenceStore()
        {
            _references = new Dictionary<string, object>();
        }

        public void Add(string varName, object obj)
        {
            _references.Add(varName, obj);    
        }

        public object Get(string varName)
        {
            if (_references.TryGetValue(varName, out object returnValue))
            {
                return returnValue;
            }

            throw new InvalidOperationException();
        }

        public void Reset()
        {
            _references.Clear();
        }
    }
}
