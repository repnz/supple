using System.IO;

namespace Supple
{
    public interface ISuppleDeserializer
    {
        T Deserialize<T>(Stream stream);
    }
}
