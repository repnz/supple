namespace Supple.Tests.TestObjects
{
    class TwoConstructorTestObject
    {
        public string ParamA { get; }
        public string ParamB { get; }

        public TwoConstructorTestObject() { }

        public TwoConstructorTestObject(string paramA)
        {
            ParamA = paramA;
        }

        public TwoConstructorTestObject(string paramA, string paramB)
        {
            ParamA = paramA;
            ParamB = paramB;
        }

        
    }
}
