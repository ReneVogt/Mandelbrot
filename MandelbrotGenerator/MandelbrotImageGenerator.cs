using System;
using System.Diagnostics;
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
                Debug.WriteLine($"PROGRESS: {ip} {cp} {c}");
                if (c <= 0) return 0;
                if (cp <= 0)
                    return ip * 80 / c;
                return 80 + cp * 20 / c;
            }
        }

        public int MaxDegreeOfParallelism { get; set; } = -1;
        public MandelbrotColorizer? Colorizer { get; set; }
        public int MaximumNumberOfIterations { get; set; } = 500;
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

            var pixelSource = from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select (x, y);

            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };
            var maxIterations = MaximumNumberOfIterations;
            pixels = height * width;
            iteratingProgress = colorizingProgress = 0;
            MandelbrotPoint[] iteratedPoints = new MandelbrotPoint[pixels];
            Parallel.ForEach(pixelSource, options, pixel =>
            {
                iteratedPoints[pixel.y * width + pixel.x] = MandelbrotPoint.Calculate(realMin + pixel.x * dx / width,
                                                                                      imaginaryMax - pixel.y * dy / height, maxIterations,
                                                                                      cancellationToken);
                Interlocked.Increment(ref iteratingProgress);
            });
            Color[] colors = new Color[pixels];
            var statistics = new MandelbrotStatistics(maxIterations, iteratedPoints);
            Parallel.ForEach(iteratedPoints.Select((m, i) => (m, i)), options, m =>
            {
                colors[m.i] = m.m.Set ? colorizer.SetColor : colorizer.Colorize(m.m.Iterations, m.m.SquaredMagnitude, statistics);
                Interlocked.Increment(ref colorizingProgress);
            });
            iteratingProgress = colorizingProgress = 0;
            return colors;
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
