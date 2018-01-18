using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supple.Xml.Exceptions
{
    class PropertyNotFoundException : Exception
    {
        public string Name { get; }
        public Type Type { get; }

        public PropertyNotFoundException(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public PropertyNotFoundException(string name, Type type, string message) : base(message)
        {
            Name = name;
            Type = type;
        }

        public PropertyNotFoundException(string name, Type type, string message, Exception innerException) : base(message, innerException)
        {
            Name = name;
            Type = type;
        }

        public PropertyNotFoundException(string name, Type type, Exception innerException) : base("", innerException)
        {
            Name = name;
            Type = type;
        }

        public override string Message => $"Cannot find property {Name} in type {Type} {base.Message}";
    }
}
