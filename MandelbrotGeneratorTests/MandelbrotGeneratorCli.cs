using System;
using System.Drawing.Imaging;
using System.Threading;
using MandelbrotGenerator;

namespace MandelbrotGeneratorTests
{
    static class MandelbrotGeneratorCli
    {
        const int width = 1200;
        const int height = 800;
        static int lastProgress = 0;
        static Timer timer = new Timer(OnTimer, null, 0, 40);
        static MandelbrotImageGenerator generator = new MandelbrotImageGenerator
        {
            MaximumNumberOfIterations = 120
        };
        static void Main()
        {
            using var bitmap = generator.CreateBitmap(width, height);
            bitmap.Save(@"C:\Arbeit\mandelbrot.png", ImageFormat.Png);
            Console.WriteLine("Done");
        }
        static void OnTimer(object state)
        {
            if (generator.Progress != lastProgress)
            {
                lastProgress = generator.Progress;
                Console.WriteLine(lastProgress);
            }
        }
    }
}
