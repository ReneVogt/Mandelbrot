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
        readonly Size resolution;
        readonly int pixels;
        readonly TaskCompletionSource<Bitmap> taskCompletionSource = new TaskCompletionSource<Bitmap>();
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        int started;

        int iteratingProgress, colorizingProgress;
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
        public int MaxDegreeOfParallelism { get; }
        public MandelbrotColorizer Colorizer { get; }
        public int MaximumNumberOfIterations { get; }
        public Size Resolution => new Size(resolution.Width, resolution.Height);
        public MandelbrotArea Area { get; }
        public Task<Bitmap> GeneratorTask => taskCompletionSource.Task;
        public bool IsCancelled => cancellationTokenSource.IsCancellationRequested;

        public MandelbrotBitmapGenerator(MandelbrotColorizer colorizer, Size resolution, MandelbrotArea area, int maximumNumberOfiterations, int maxDegreeOfParallelism = -1)
        {
            Colorizer = colorizer ?? throw new ArgumentNullException(nameof(colorizer));
            if (resolution.Width <= 0 || resolution.Height <= 0)
                throw new ArgumentException("The resolution must have positive width and height!", nameof(resolution));

            var (realMin, realMax, imaginaryMin, imaginaryMax) = area;
            if (realMin >= realMax || imaginaryMin >= imaginaryMax)
                throw new ArgumentException("The requested area is invalid!.", nameof(area));

            if (realMax - realMin < resolution.Width * double.Epsilon ||
                imaginaryMax - imaginaryMin <= resolution.Height * double.Epsilon)
                throw new ArgumentException("The desired area and resolution cannot be calculated precisely enough by the current implementation.");

            this.resolution = resolution;
            pixels = resolution.Height * resolution.Width;
            Area = area;
            MaximumNumberOfIterations = maximumNumberOfiterations;
            MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        public async Task<Bitmap> GenerateAsync()
        {
            if (Interlocked.CompareExchange(ref started, 1, 0) != 0)
                return await GeneratorTask.ConfigureAwait(false);
            try
            {
                var bmp = await Task.Run(() => CreateBitmap(cancellationTokenSource.Token), cancellationTokenSource.Token).ConfigureAwait(false);
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                taskCompletionSource.SetResult(bmp);
                return bmp;
            }
            catch (Exception e)
            {
                if (cancellationTokenSource.IsCancellationRequested) taskCompletionSource.SetCanceled();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                taskCompletionSource.SetException(e);
                throw;
            }
        }
        public void Cancel() => cancellationTokenSource.Cancel();
        Bitmap CreateBitmap(CancellationToken cancellationToken = default)
        {
            var (realMin, realMax, imaginaryMin, imaginaryMax) = Area;
            int height = Resolution.Height, width = Resolution.Width;
            double dx = realMax - realMin;
            double dy = imaginaryMax - imaginaryMin;

            var pixelSource =(from y in Enumerable.Range(0, height)
                               from x in Enumerable.Range(0, width)
                               select new Point(x, y)).ToArray();

            var options = new ParallelOptions
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };
            var maxIterations = MaximumNumberOfIterations;
            iteratingProgress = colorizingProgress = 0;

            object? userState = Colorizer.Initialize(resolution, Area, maxIterations);

            Color[] colors = new Color[pixels];
            if (Colorizer.UsePostCalculationColorization)
            {
                MandelbrotPoint[] iteratedPoints = new MandelbrotPoint[pixels];

                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    iteratedPoints[pixel.Y * width + pixel.X] = MandelbrotPoint.Calculate(realMin + pixel.X * dx / width,
                                                                                          imaginaryMax - pixel.Y * dy / height, maxIterations,
                                                                                          cancellationToken);
                    Interlocked.Increment(ref iteratingProgress);
                });
                userState = Colorizer.Initialize(iteratedPoints, userState);
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    int index = pixel.Y * width + pixel.X;
                    var iteratedPoint = iteratedPoints[index];
                    colors[index] = iteratedPoint.Set
                                        ? Colorizer.SetColor
                                        : Colorizer.GetColor(pixel, iteratedPoint, userState);
                    Interlocked.Increment(ref colorizingProgress);
                });
            }
            else
            {
                Parallel.ForEach(pixelSource, options, pixel =>
                {
                    var m = MandelbrotPoint.Calculate(realMin + pixel.X * dx / width,
                                                                                          imaginaryMax - pixel.Y * dy / height, maxIterations,
                                                                                          cancellationToken);
                    colors[pixel.Y * width + pixel.X] = m.Set ? Colorizer.SetColor :
                        Colorizer.GetColor(pixel, m, userState);
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
