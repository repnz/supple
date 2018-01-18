namespace Supple.Tests.TestObjects
{
    class ConstructorObject
    {
        public string ParamA { get; }
        public string ParamB { get; }

        public ConstructorObject(string paramA, string paramB)
        {
            ParamA = paramA;
            ParamB = paramB;
        }
    }
}
