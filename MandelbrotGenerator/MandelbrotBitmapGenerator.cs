using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MandelbrotGenerator.Colorizer;

namespace MandelbrotGenerator
{
    public sealed class MandelbrotBitmapGenerator
    {
        readonly int maxDegreeOfParallelism, maximumNumberOfIterations;
        readonly MandelbrotColorizer colorizer;
        readonly MandelbrotArea area;
        readonly Size resolution;
        readonly int pixels;
        readonly TaskCompletionSource<Bitmap> taskCompletionSource = new TaskCompletionSource<Bitmap>();
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        int started;
        int iteratingProgress, colorizingProgress;

        CancellationToken CancellationToken => cancellationTokenSource.Token;

        public int Progress
        {
            get
            {
                int c = pixels, ip = iteratingProgress, cp = colorizingProgress;
                if (c <= 0) return 0;
                if (!colorizer.UsePostCalculationColorization) return ip * 100 / c;
                if (cp <= 0)
                    return ip * 80 / c;
                return 80 + cp * 20 / c;
            }
        }
        public bool IsCancelled => cancellationTokenSource.IsCancellationRequested;

        public MandelbrotBitmapGenerator(MandelbrotColorizer colorizer, Size resolution, MandelbrotArea area, int maximumNumberOfIterations, int maxDegreeOfParallelism = -1)
        {
            this.colorizer = colorizer ?? throw new ArgumentNullException(nameof(colorizer));
            if (resolution.Width <= 0 || resolution.Height <= 0)
                throw new ArgumentException("The resolution must have positive width and height!", nameof(resolution));

            var (realMin, imaginaryMin, realMax, imaginaryMax) = area;
            if (realMin >= realMax || imaginaryMin >= imaginaryMax)
                throw new ArgumentException("The requested area is invalid!.", nameof(area));

            if (realMax - realMin < resolution.Width * double.Epsilon ||
                imaginaryMax - imaginaryMin <= resolution.Height * double.Epsilon)
                throw new ArgumentException("The desired area and resolution cannot be calculated precisely enough by the current implementation.");

            this.resolution = resolution;
            pixels = resolution.Height * resolution.Width;
            this.area = area;
            this.maximumNumberOfIterations = maximumNumberOfIterations;
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        public Task<Bitmap> CreateBitmapParallel()
        {
            ThrowIfAlreadyUsed();
            return Task.Run(CreateBitmapInternal, CancellationToken);
        }
        public Bitmap CreateBitmap()
        {
            ThrowIfAlreadyUsed();
            return CreateBitmapInternal();
        }
        public void Cancel() => cancellationTokenSource.Cancel();
        void ThrowIfAlreadyUsed()
        {
            if (Interlocked.CompareExchange(ref started, 1, 0) != 0)
                throw new InvalidOperationException($"This {nameof(MandelbrotBitmapGenerator)} has already generated a bitmap! It cannot be used twice.");
        }
        Bitmap CreateBitmapInternal()
        {
            var (realMin, imaginaryMin, realMax, imaginaryMax) = area;
            int height = resolution.Height, width = resolution.Width;
            double dx = realMax - realMin;
            double dy = imaginaryMax - imaginaryMin;

            var pixelSource =(from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select new Point(x, y)).ToArray();

            var options = new ParallelOptions
            {
                CancellationToken = CancellationToken,
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            var maxIterations = maximumNumberOfIterations;
            iteratingProgress = colorizingProgress = 0;

            object? userState = colorizer.Initialize(resolution, area, maxIterations);

            Color[] colors = new Color[pixels];
            if (colorizer.UsePostCalculationColorization)
            {
                MandelbrotPoint[] iteratedPoints = new MandelbrotPoint[pixels];

                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    iteratedPoints[pixel.Y * width + pixel.X] = MandelbrotPoint.Calculate(realMin + pixel.X * dx / width,
                                                                                          imaginaryMax - pixel.Y * dy / height, maxIterations,
                                                                                          CancellationToken);
                    Interlocked.Increment(ref iteratingProgress);
                });
                userState = colorizer.Initialize(iteratedPoints, userState);
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    int index = pixel.Y * width + pixel.X;
                    var iteratedPoint = iteratedPoints[index];
                    colors[index] = iteratedPoint.Set
                                        ? colorizer.SetColor
                                        : colorizer.GetColor(pixel, iteratedPoint, userState);
                    Interlocked.Increment(ref colorizingProgress);
                });
            }
            else
            {
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    var m = MandelbrotPoint.Calculate(realMin + pixel.X * dx / width,
                                                                                          imaginaryMax - pixel.Y * dy / height, maxIterations,
                                                                                          CancellationToken);
                    colors[pixel.Y * width + pixel.X] = m.Set ? colorizer.SetColor :
                        colorizer.GetColor(pixel, m, userState);
                    Interlocked.Increment(ref iteratingProgress);
                });
            }

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var lockBits = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int i = 0; i < colors.Length; i++)
                Marshal.WriteInt32(lockBits.Scan0, i * 4, colors[i].ToArgb());
            bitmap.UnlockBits(lockBits);

            return bitmap;
        }
    }
}
