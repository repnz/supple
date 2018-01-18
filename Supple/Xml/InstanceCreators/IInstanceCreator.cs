using System;
using System.Xml.Linq;

namespace Supple.Xml.InstanceCreators
{
    public interface IInstanceCreator
    {
        object CreateInstance(Type type, XElement element);
    }
}
