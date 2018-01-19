using Supple.Xml.Exceptions;
using System;

namespace Supple.Xml
{
    class ConvertableDeserializer : IValueDeserializer
    {
        public object Deserialize(Type type, string name, string value)
        {
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (FormatException e)
            {
                throw new ConvertibleFormatException(name, value, type, e);
            }
            
        }

        public bool IsMatch(Type type, string name, string value)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                    return false;
                default:
                    return true;
            }
        }
    }
}
