using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
