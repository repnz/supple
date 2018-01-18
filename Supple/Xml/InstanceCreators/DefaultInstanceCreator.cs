using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
