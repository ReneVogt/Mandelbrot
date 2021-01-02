using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MandelbrotGenerator.Exceptions;

namespace MandelbrotGenerator
{
    public class MandelbrotImageGenerator
    {
        int iteratingProgress, colorizingProgress, pixels;
        public int Progress
        {
            get
            {
                int c = pixels, ip = iteratingProgress, cp = colorizingProgress;
                if (c <= 0) return 0;
                if (!Colorizer.UsePostCalculationColorization) return ip * 100 / c;
                if (cp <= 0)
                    return ip * 80 / c;
                return 80 + cp * 20 / c;
            }
        }

        public int MaxDegreeOfParallelism { get; set; } = -1;
        public MandelbrotColorizer Colorizer { get; }
        public int MaximumNumberOfIterations { get; set; } = 500;

        public MandelbrotImageGenerator()
            : this(MandelbrotColorizer.Default)
        { }
        public MandelbrotImageGenerator(MandelbrotColorizer colorizer)
        {
            Colorizer = colorizer ?? throw new ArgumentNullException(nameof(colorizer));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Color[] CreateImage(int width, int height, MandelbrotArea area, CancellationToken cancellationToken = default)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(width), message: "Parameter 'width' must be greater than or equal to zero.",
                                                      actualValue: width);
            if (height < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(height), message: "Parameter 'height' must be greater than or equal to zero.",
                                                      actualValue: height);

            var (realMin, realMax, imaginaryMin, imaginaryMax) = area;
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

            var pixelSource =(from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select (x, y)).ToArray();

            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };
            var maxIterations = MaximumNumberOfIterations;
            pixels = height * width;
            iteratingProgress = colorizingProgress = 0;

            if (colorizer.UsePostCalculationColorization)
            {
                MandelbrotPoint[] iteratedPoints = new MandelbrotPoint[pixels];
                Color[] colors = new Color[pixels];

                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    iteratedPoints[pixel.y * width + pixel.x] = MandelbrotPoint.Calculate(realMin + pixel.x * dx / width,
                                                                                          imaginaryMax - pixel.y * dy / height, maxIterations,
                                                                                          cancellationToken);
                    Interlocked.Increment(ref iteratingProgress);
                });
                colorizer.InitializePostCalculationColorization(iteratedPoints, maxIterations);
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    int index = pixel.y * width + pixel.x;
                    var iteratedPoint = iteratedPoints[index];
                    colors[index] = iteratedPoint.Set
                                        ? colorizer.SetColor
                                        : colorizer.PostCalculationColorization(pixel.x, pixel.y, iteratedPoint.Real, iteratedPoint.Imaginary,
                                                                                iteratedPoint.Iterations, maxIterations,
                                                                                iteratedPoint.SquaredMagnitude);
                    Interlocked.Increment(ref colorizingProgress);
                });
                return colors;
            }
            else
            {
                Color[] colors = new Color[pixels];
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    var m = MandelbrotPoint.Calculate(realMin + pixel.x * dx / width,
                                                                                          imaginaryMax - pixel.y * dy / height, maxIterations,
                                                                                          cancellationToken);
                    colors[pixel.y * width + pixel.x] =
                        colorizer.ImmediateColorization(pixel.x, pixel.y, m.Real, m.Imaginary, m.Iterations, maxIterations, m.SquaredMagnitude);
                    Interlocked.Increment(ref iteratingProgress);
                });
                return colors;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Bitmap CreateBitmap(int width, int height, MandelbrotArea area, CancellationToken cancellationToken = default)
        {
            var colors = CreateImage(width, height, area, cancellationToken);
            
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var lockBits = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int i = 0; i < colors.Length; i++)
                Marshal.WriteInt32(lockBits.Scan0, i*4, colors[i].ToArgb());
            bitmap.UnlockBits(lockBits);

            return bitmap;
        }
    }
}
