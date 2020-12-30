using System.Drawing.Imaging;
using MandelbrotGenerator;

namespace MandelbrotGeneratorTests
{
    static class MandelbrotGeneratorCli
    {
        static void Main()
        {
            MandelbrotImageGenerator generator = new MandelbrotImageGenerator {MaximumNumberOfIterations = 100};
            var bmp = generator.CreateBitmap(256, 256, realMin: -1.5, realMax: 0.8);
            bmp.Save(@"c:\arbeit\mandelbrot.png", ImageFormat.Png);
        }
    }
}
