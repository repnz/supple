using System.Collections.Generic;
using System.Reflection;

namespace Supple.Deserialization.InstanceCreators
{
    public class ConstructorMap
    {
        private readonly Dictionary<string, Node> _m;
        public ConstructorInfo Constructor { get; }

        public IReadOnlyDictionary<string, Node> Parameters => _m;

        public ConstructorMap(ConstructorInfo ctor)
        {
            _m = new Dictionary<string, Node>();
            Constructor = ctor;
        }

        public void AddMatchParameter(ParameterInfo param, Node node)
        {
            _m.Add(param.Name, node);
        }

        public int GetMatchCount()
        {
            return _m.Count;
        }

        public bool IsFine()
        {
            return _m.Count == Constructor.GetParameters().Length;
        }

        
    }
}
