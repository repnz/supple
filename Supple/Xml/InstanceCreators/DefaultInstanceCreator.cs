using System;
using System.Xml.Linq;

namespace Supple.Xml.InstanceCreators
{
    class DefaultInstanceCreator : IInstanceCreator
    {
        public object CreateInstance(Type type, XElement element)
        {
            return Activator.CreateInstance(type);
        }
    }
}
