using System.IO;

namespace Supple.Deserialization
{
    public interface ISuppleDeserializer
    {
        T Deserialize<T>(Stream stream);
    }
}
