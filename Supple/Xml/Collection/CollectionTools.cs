using System;
using System.Collections.Generic;
using System.Linq;

namespace Supple.Xml.Collection
{
    class CollectionTools
    {
        public static bool HasCollectionBase(Type type)
        {
            return GetCollectionBase(type) != null;
        }

        public static Type GetCollectionBase(Type type)
        {
            return type.GetInterfaces().FirstOrDefault(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(ICollection<>)
            );
        }

        public static Type GetCollectionElementType(Type collectionBase)
        {
            return collectionBase.GenericTypeArguments.First();
        }
    }
}
