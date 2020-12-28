using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MandelbrotGenerator.Exceptions;

namespace MandelbrotGenerator
{
    public class MandelbrotImageGenerator
    {
        int progress, pixels;
        public int MaxDegreeOfParallelism { get; set; } = -1;
        public MandelbrotColorizer? Colorizer { get; set; }
        public int MaximumNumberOfIterations { get; set; } = 100;
        public int Progress
        {
            get
            {
                var p = pixels;
                if (p == 0) return 0;
                return progress * 100 / p;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Color[] CreateImage(int width, int height,
                                          double realMin = -2,
                                          double realMax = 1,
                                          double imaginaryMin = -1,
                                          double imaginaryMax = 1,
                                          CancellationToken cancellationToken = default)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(width), message: "Parameter 'width' must be greater than or equal to zero.",
                                                      actualValue: width);
            if (height < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(height), message: "Parameter 'height' must be greater than or equal to zero.",
                                                      actualValue: height);
            if (realMin >= realMax)
                throw new ArgumentException("The real minimum must be less than the real maximum.");
            if (imaginaryMin >= imaginaryMax)
                throw new ArgumentException("The imaginary minimum must be less than the imaginary maximum.");

            if (realMax - realMin < width * double.Epsilon ||
                imaginaryMax - imaginaryMin <= height * double.Epsilon)
                throw new MandelbrotPrecisionException();

            var colorizer = Colorizer ?? MandelbrotColorizer.Default;
            double dx = realMax - realMin;
            double dy = imaginaryMax - imaginaryMin;

            var pixelSource = from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select (x, y);

            Color[] result = new Color[width * height];
            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };
            var maxIterations = MaximumNumberOfIterations;
            pixels = height * width;
            progress = 0;
            Parallel.ForEach(pixelSource, options, (pixel) =>
            {
                var m = MandelbrotPoint.Calculate(realMin + pixel.x * dx / width, imaginaryMax - pixel.y * dy / height, maxIterations,
                                                  cancellationToken);
                result[pixel.y * width + pixel.x] = m.Set ? colorizer.GetInsideColor() : colorizer.GetOutsideColor(m.Iterations, maxIterations, m.SquaredMagnitude);
                Interlocked.Increment(ref progress);
            });
            progress = 0;
            return result;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Bitmap CreateBitmap(int width, int height,
                                   double realMin = -2,
                                   double realMax = 1,
                                   double imaginaryMin = -1,
                                   double imaginaryMax = 1,
                                   CancellationToken cancellationToken = default)
        {
            var colors = CreateImage(width: width, height: height, 
                                     realMin: realMin, realMax: realMax,
                imaginaryMin: imaginaryMin, imaginaryMax: imaginaryMax,
                cancellationToken: cancellationToken);
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                bitmap.SetPixel(x, y, colors[y * width + x]);
            return bitmap;
        }
    }
}
