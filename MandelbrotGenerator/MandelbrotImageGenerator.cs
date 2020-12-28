using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MandelbrotGenerator.Exceptions;

namespace MandelbrotGenerator
{
    public static class MandelbrotImageGenerator
    {
        public static Color[] CreateImage(int width, int height,
                                          double realMin = -2,
                                          double realMax = 1,
                                          double imaginaryMin = -1,
                                          double imaginaryMax = 1,
                                          int maxIterations = 100,
                                          MandelbrotColorizer colorizer = null!,
                                          int degreeOfParallelism = -1, 
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
            if (maxIterations <= 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(maxIterations), message: "Parameter 'maxIterations' must be greater than zero.",
                                                      actualValue: maxIterations);

            if (realMax - realMin < width * double.Epsilon ||
                imaginaryMax - imaginaryMin <= height * double.Epsilon)
                throw new MandelbrotPrecisionException();

            colorizer ??= DefaultColorizer;
            double dx = realMax - realMin;
            double dy = imaginaryMax - imaginaryMin;

            var pixelSource = from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select (x, y);

            Color[] result = new Color[width * height];
            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = degreeOfParallelism
            };
            Parallel.ForEach(pixelSource, options, (pixel) =>
            {
                var m = MandelbrotPoint.Calculate(realMin + pixel.x * dx / width, imaginaryMax - pixel.y * dy / height, maxIterations,
                                                  cancellationToken);
                result[pixel.y * width + pixel.x] = colorizer(m.Iterations, maxIterations, m.LastMagnitude);
            });

            return result;
        }
        public static Bitmap CreateBitmap(int width, int height,
                                          double realMin = -2,
                                          double realMax = 1,
                                          double imaginaryMin = -1,
                                          double imaginaryMax = 1,
                                          int maxIterations = 100,
                                          MandelbrotColorizer colorizer = null!,
                                          int degreeOfParallelism = -1,
                                          CancellationToken cancellationToken = default)
        {
            var colors = CreateImage(width: width, height: height, 
                                     realMin: realMin, realMax: realMax,
                imaginaryMin: imaginaryMin, imaginaryMax: imaginaryMax,
                maxIterations: maxIterations,
                colorizer: colorizer,
                degreeOfParallelism: degreeOfParallelism,
                cancellationToken: cancellationToken);
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                bitmap.SetPixel(x, y, colors[y * width + x]);
            return bitmap;

        }
        static Color DefaultColorizer(int remainingIterations, int maxIterations, double magnitude)
        {
            if (remainingIterations == 0) return Color.Black;

            int r = 256 * (maxIterations - remainingIterations) / maxIterations;
            return Color.FromArgb(r, r, r);
        }
    }
}
