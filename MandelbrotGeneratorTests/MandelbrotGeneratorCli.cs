using System;
using System.Numerics;
using System.Runtime.Versioning;

[assembly: CLSCompliant(true)]
[assembly: SupportedOSPlatform("windows")]

#nullable enable

namespace MandelbrotGeneratorTests
{
    static class MandelbrotGeneratorCli
    {
        static void Main()
        {
            Complex c = new(2.1, 3.5);
            Console.WriteLine(c);
        }
    }
}
