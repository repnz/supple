using Supple.Deserialization.Exceptions;
using System;

namespace Supple.Deserialization.Deserializers
{
    class ConvertableDeserializer : ValueDeserializer
    {
        protected override object Deserialize(Type type, string name, string value)
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

        protected override bool IsMatch(Type type, string name, string value)
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
