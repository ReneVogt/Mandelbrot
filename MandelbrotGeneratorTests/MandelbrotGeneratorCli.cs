using System;
using MandelbrotGenerator;

#nullable enable

namespace MandelbrotGeneratorTests
{
    static class MandelbrotGeneratorCli
    {
        static void Main()
        {
            MandelbrotArea area = (-2.12358123d, 3.1712309154853d, -1.234109853d, 4.12345d);
            Console.WriteLine(area);
        }
    }
}
