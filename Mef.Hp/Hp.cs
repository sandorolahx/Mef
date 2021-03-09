using Mef.Abstraction;
using System;
using System.Composition;

namespace Mef.Hp
{
    [Export(typeof(IPrinter))]
    public class Hp : IPrinter
    {
        public string Print()
        {
            return "Hp";
        }
    }
}
