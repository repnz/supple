using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supple.Xml.NameCreators
{
    class KeyValuePairNameCreator : ITypeNameCreator
    {
        public string CreateName(Type type)
        {
            return "Pair";
        }

        public bool IsMatch(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }
    }
}
