namespace Supple.Tests.TestObjects
{
    class ThreeConstructorTestObject
    {
        public string ParamA { get; set; }
        public string ParamB { get; set; }
        public string ParamC { get; set; }

        public ThreeConstructorTestObject(string paramA, string paramB, string paramC)
        {
            ParamA = paramA;
            ParamB = paramB;
            ParamC = paramC;
        }

        public ThreeConstructorTestObject() { }
    }
}
