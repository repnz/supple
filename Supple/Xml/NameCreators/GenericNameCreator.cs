using System;
using System.Linq;

namespace Supple.Xml.NameCreators
{
    class GenericNameCreator : ITypeNameCreator
    {
        private readonly ITypeNameCreator _nameCreator;

        public GenericNameCreator(ITypeNameCreator typeNameCreator)
        {
            _nameCreator = typeNameCreator;
        }

        public GenericNameCreator()
        {
            _nameCreator = this;
        }

        public bool IsMatch(Type type)
        {
            return true;
        }

        private string GetNameWithoutGenericArity(Type t)
        {
            string name = t.Name;
            
            if (t.IsInterface && name.StartsWith("I"))
            {
                name = name.Substring(1);
            }

            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        public string CreateName(Type type)
        {
            string typeName = GetNameWithoutGenericArity(type);

            if (type.IsGenericType)
            {
                typeName += "Of";

                Type[] genericArguments = type.GetGenericArguments();

                foreach (Type genericArgument in genericArguments.Take(genericArguments.Length - 1))
                {
                    typeName += _nameCreator.CreateName(genericArgument);
                    typeName += "And";
                }

                // Add Last Generic Argument 
                typeName += _nameCreator.CreateName(genericArguments.Last());
            }
            
            return typeName;
        }
    }
}
