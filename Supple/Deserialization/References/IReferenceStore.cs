namespace Supple.Deserialization.References
{
    public interface IReferenceStore
    {
        void Add(string varName, object obj);
        object Get(string varName);
        void Reset();
    }
}
