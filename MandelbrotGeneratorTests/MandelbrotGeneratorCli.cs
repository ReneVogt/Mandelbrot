using System.Drawing.Imaging;
using MandelbrotGenerator;

namespace MandelbrotGeneratorTests
{
    static class MandelbrotGeneratorCli
    {
        const int width = 300;
        const int height = 200;
        static void Main()
        {
            using var bitmap = MandelbrotImageGenerator.CreateBitmap(width, height);
            bitmap.Save(@"C:\Arbeit\mandelbrot.png", ImageFormat.Png);

        }
    }
}
