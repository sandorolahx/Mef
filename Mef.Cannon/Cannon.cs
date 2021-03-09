using Mef.Abstraction;
using System;
using System.Composition;

namespace Mef.Cannon
{
    [Export(typeof(IPrinter))]
    public class Hp : IPrinter
    {
        public string Print()
        {
            return "Cannon";
        }
    }
}
