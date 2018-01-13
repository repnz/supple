using System.Collections.Generic;

namespace Supple.Tests.TestObjects
{
    interface ITestInterface
    {
        string Do();
    }

    class TestInterfaceImpl1 : ITestInterface
    {
        public string OneProperty { get; set; }

        public string Do() { return OneProperty; }

        public override bool Equals(object obj)
        {
            var impl = obj as TestInterfaceImpl1;
            return impl != null &&
                   OneProperty == impl.OneProperty;
        }

        public override int GetHashCode()
        {
            return -1600431250 + EqualityComparer<string>.Default.GetHashCode(OneProperty);
        }
    }

    class TestInterfaceImpl2 : ITestInterface
    {
        public string TwoProperty { get; set; }

        public string Do() { return TwoProperty; }

        public override bool Equals(object obj)
        {
            var impl = obj as TestInterfaceImpl2;
            return impl != null &&
                   TwoProperty == impl.TwoProperty;
        }

        public override int GetHashCode()
        {
            return -669734620 + EqualityComparer<string>.Default.GetHashCode(TwoProperty);
        }
    }

    class ObjectWithInterfaceProperty
    {
        public ITestInterface InterfaceProperty { get; set; }

        public override bool Equals(object obj)
        {
            var property = obj as ObjectWithInterfaceProperty;
            return property != null &&
                   EqualityComparer<ITestInterface>.Default.Equals(InterfaceProperty, property.InterfaceProperty);
        }

        public override int GetHashCode()
        {
            return 1823273665 + EqualityComparer<ITestInterface>.Default.GetHashCode(InterfaceProperty);
        }
    }
}
