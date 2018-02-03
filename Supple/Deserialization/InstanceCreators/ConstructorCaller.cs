using Supple.Deserialization.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Supple.Deserialization.InstanceCreators
{
    class ConstructorCaller : IInstanceCreator
    {
        private readonly IDelegator _delegator;

        public ConstructorCaller(IDelegator delegator)
        {
            _delegator = delegator;
        }

        public object CreateInstance(Type type, Node node)
        {
            var map = GetMatchConstructorMap(type, node);
            return CallConstructor(map);
        }

        private ConstructorMap GetMatchConstructorMap(Type type, Node node)
        {
            // Get Constructors ordered by number of parameters
            IEnumerable<ConstructorInfo> ctors = type
                .GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);

            IEnumerable<Node> subNodes = node.GetChildren();

            var values = new Dictionary<string, Node>();

            foreach (var n in subNodes)
            {
                values[n.Name.ToLower()] = n;
            }

            ConstructorMap bestMatch = null;

            foreach (ConstructorInfo ctor in ctors)
            {
                ConstructorMap map = CreateConstructorMap(ctor, values);

                if (map.IsFine())
                {
                    return map;
                }
                else if (bestMatch == null || map.GetMatchCount() > bestMatch.GetMatchCount())
                {
                    bestMatch = map;
                }
            }

            throw new ConstructorNotFoundException(node, type);
        }

        private ConstructorMap CreateConstructorMap(ConstructorInfo info,
            IReadOnlyDictionary<string, Node> values)
        {
            ConstructorMap map = new ConstructorMap(info);

            foreach (ParameterInfo parameter in info.GetParameters())
            {
                string s = parameter.Name.ToLower();

                if (values.TryGetValue(s, out Node value))
                {
                    map.AddMatchParameter(parameter, value);
                }
            }

            return map;
        }

        private object CallConstructor(ConstructorMap map)
        {
            object[] parameters = new object[map.GetMatchCount()];
            int index = 0;
            
            foreach (ParameterInfo paramInfo in map.Constructor.GetParameters())
            {
                Node paramNode = map.Parameters[paramInfo.Name];

                parameters[index] = _delegator.Deserialize(
                    paramInfo.ParameterType,
                    paramNode
                    );

                paramNode.Remove();
                ++index;
            }

            return map.Constructor.Invoke(parameters);
        }
    }
}
