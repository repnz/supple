using System;

namespace Supple.Xml
{
    class ConvertableDeserializer : IValueDeserializer
    {
        public object Deserialize(Type type, string name, string value)
        {
            return Convert.ChangeType(value, type);
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
