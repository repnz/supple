using System;
using System.Collections.Generic;
using System.Linq;

namespace Supple.Xml.List
{
    class ListTools
    {
        public static bool HasListBase(Type type)
        {
            return ListTools.GetListBase(type) != null;
        }

        public static Type GetListBase(Type type)
        {
            return type.GetInterfaces().FirstOrDefault(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)
            );
        }

        public static Type GetListElementType(Type collectionBase)
        {
            return collectionBase.GenericTypeArguments.First();
        }
    }
}
