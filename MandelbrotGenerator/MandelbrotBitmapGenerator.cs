using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MandelbrotGenerator.Colorizer;

namespace MandelbrotGenerator
{
    /// <summary>
    /// Creates a <see cref="Bitmap"/> with the given resolution of
    /// the given section of the Mandelbrot set in the complex plane.
    /// </summary>
    public sealed class MandelbrotBitmapGenerator : IDisposable
    {
        #region Constants
        static readonly ArgumentNullException colorizerNullException = new ArgumentNullException("colorizer");
        static readonly ArgumentNullException scopeNullException = new ArgumentNullException("scope");
        static readonly ArgumentException invalidResolutionException =
            new ArgumentException("The resolution must have positive width and height!", "resolution");
        static readonly ArgumentException implementationNotPreciseEnoughException = new ArgumentException("The desired scope and resolution cannot be calculated precisely enough by the current implementation.");
        #endregion

        readonly int maxDegreeOfParallelism, maximumNumberOfIterations;
        readonly MandelbrotColorizer colorizer;
        readonly ComplexScope scope;
        readonly Size resolution;
        readonly (Point pixel, int index, Complex c)[] pixelSource;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        int started, disposed;
        int iteratingProgress, colorizingProgress;

        int PixelCount => pixelSource.Length;

        CancellationToken CancellationToken => cancellationTokenSource.Token;

        /// <summary>
        /// Gets a value between 0 and 100 indicating the calculation progress in percent.
        /// </summary>
        public int Progress
        {
            get
            {
                int c = PixelCount, ip = iteratingProgress, cp = colorizingProgress;
                if (c <= 0) return 0;
                if (!colorizer.UsePostCalculationColorization) return ip * 100 / c;
                if (cp <= 0)
                    return ip * 80 / c;
                return 80 + cp * 20 / c;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the calculation has been cancelled.
        /// </summary>
        public bool IsCancelled => cancellationTokenSource.IsCancellationRequested;

        /// <summary>
        /// Creates a new instance of the <see cref="MandelbrotBitmapGenerator"/>.
        /// </summary>
        /// <param name="colorizer">The <see cref="MandelbrotColorizer"/> to use to colorize the calculation results for the bitmap.</param>
        /// <param name="resolution">The resolution of the raster and final bitmap.</param>
        /// <param name="scope">The scope in the complex plane to analyze.</param>
        /// <param name="maximumNumberOfIterations">The maximum number of iterations after which an sequence can be considered convergent.</param>
        /// <param name="maxDegreeOfParallelism">This value is used for the TPL via the <see cref="ParallelOptions.MaxDegreeOfParallelism"/> property to control how the calculation is parallelized.</param>
        /// <exception cref="ArgumentNullException"><paramref name="colorizer"/> or <paramref name="scope"/> are <c>null</c>!</exception>
        /// <exception cref="ArgumentException"><paramref name="resolution"/> has zero or negative values, or the <paramref name="scope"/> is too small to be analyzed by this implementation.</exception>
        public MandelbrotBitmapGenerator(MandelbrotColorizer colorizer, Size resolution, ComplexScope scope, int maximumNumberOfIterations, int maxDegreeOfParallelism = -1)
        {
            this.colorizer = colorizer ?? throw colorizerNullException;
            this.scope = scope ?? throw scopeNullException;

            if (resolution.Width <= 0 || resolution.Height <= 0)
                throw invalidResolutionException;

            var reals = Enumerable.Range(0, resolution.Width)
                                  .Select(x => scope.LowerLeft.Real + x * scope.Real / resolution.Width)
                                  .ToArray();
            var checkSet = new HashSet<double>();
            if (!reals.All(checkSet.Add))
                throw implementationNotPreciseEnoughException;
            
            var imaginaries = Enumerable.Range(0, resolution.Height)
                                  .Select(y => scope.UpperRight.Imaginary - y * scope.Imaginary / resolution.Height)
                                  .ToArray();
            checkSet.Clear();
            if (!imaginaries.All(checkSet.Add))
                throw implementationNotPreciseEnoughException;

            pixelSource = (from y in Enumerable.Range(0, resolution.Height)
                               from x in Enumerable.Range(0, resolution.Width)
                               select (
                                          pixel: new Point(x, y), 
                                          index: y * resolution.Width + x, 
                                          c: new Complex(reals[x], imaginaries[y])
                                          )
                              ).ToArray();

            this.resolution = resolution;
            this.scope = scope;
            this.maximumNumberOfIterations = maximumNumberOfIterations;
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
        }
        /// <inheritdoc />
        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) != 0) return;
            cancellationTokenSource.Dispose();
        }

        /// <summary>
        /// Starts the calculation as a background task and returns the <see cref="Task{Bitmap}"/> that
        /// can be used to <c>await</c> the resulting <see cref="Bitmap"/>:
        /// </summary>
        /// <returns>A <see cref="Task{Bitmap}"/> that can be used to wait for or <c>await</c> the resulting <see cref="Bitmap"/>.</returns>
        /// <exception cref="ObjectDisposedException">This instance has already been disposed of.</exception>
        /// <exception cref="InvalidOperationException">This instance has already been used to generate a bitmap. An instance of <see cref="MandelbrotBitmapGenerator"/> can only be used once.</exception>
        public Task<Bitmap> CreateBitmapParallel()
        {
            ThrowIfDisposed();
            ThrowIfAlreadyUsed();
            return Task.Run(CreateBitmapInternal, CancellationToken);
        }
        /// <summary>
        /// Performs the calculation and returns the generated <see cref="Bitmap"/>.
        /// </summary>
        /// <returns>The generated <see cref="Bitmap"/> representing an approximation of the Mandelbrot set in the given scope and using the specified <see cref="MandelbrotColorizer"/>.</returns>
        /// <exception cref="ObjectDisposedException">This instance has already been disposed of.</exception>
        /// <exception cref="InvalidOperationException">This instance has already been used to generate a bitmap. An instance of <see cref="MandelbrotBitmapGenerator"/> can only be used once.</exception>
        public Bitmap CreateBitmap()
        {
            ThrowIfDisposed();
            ThrowIfAlreadyUsed();
            return CreateBitmapInternal();
        }
        /// <summary>
        /// Cancels a running calculation.
        /// </summary>
        public void Cancel() => cancellationTokenSource.Cancel();
        
        void ThrowIfAlreadyUsed()
        {
            if (Interlocked.CompareExchange(ref started, 1, 0) != 0)
                throw new InvalidOperationException($"This {nameof(MandelbrotBitmapGenerator)} has already generated a bitmap! It cannot be used twice.");
        }
        void ThrowIfDisposed()
        {
            if (disposed > 0)
                throw new ObjectDisposedException(nameof(MandelbrotBitmapGenerator));
        }
        Bitmap CreateBitmapInternal()
        {
            var options = new ParallelOptions
            {
                CancellationToken = CancellationToken,
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            var maxIterations = maximumNumberOfIterations;
            iteratingProgress = colorizingProgress = 0;

            object? userState = colorizer.Initialize(resolution, scope, maxIterations);

            Color[] colors = new Color[PixelCount];
            if (colorizer.UsePostCalculationColorization)
            {
                IteratedPoint[] iteratedPoints = new IteratedPoint[PixelCount];

                Parallel.ForEach(pixelSource, options, p =>
                {
                    iteratedPoints[p.index] = IteratedPoint.Iterate(p.c, maxIterations, CancellationToken);
                    Interlocked.Increment(ref iteratingProgress);
                });
                userState = colorizer.Initialize(iteratedPoints, userState);
                Parallel.ForEach(pixelSource, options, p =>
                {
                    var iteratedPoint = iteratedPoints[p.index];
                    colors[p.index] = iteratedPoint.Set
                                        ? colorizer.SetColor
                                        : colorizer.GetColor(p.pixel, iteratedPoint, userState);
                    Interlocked.Increment(ref colorizingProgress);
                });
            }
            else
            {
                Parallel.ForEach(pixelSource, options, p =>
                {
                    var iteratedPoint = IteratedPoint.Iterate(p.c, maxIterations, CancellationToken);
                    colors[p.index] = iteratedPoint.Set ? colorizer.SetColor :
                        colorizer.GetColor(p.pixel, iteratedPoint, userState);
                    Interlocked.Increment(ref iteratingProgress);
                });
            }

            var bitmap = new Bitmap(resolution.Width, resolution.Height, PixelFormat.Format32bppArgb);
            var lockBits = bitmap.LockBits(new Rectangle(0, 0, resolution.Width, resolution.Height), ImageLockMode.WriteOnly,
                                           PixelFormat.Format32bppArgb);
            for (int i = 0; i < colors.Length; i++)
                Marshal.WriteInt32(lockBits.Scan0, i * 4, colors[i].ToArgb());
            bitmap.UnlockBits(lockBits);

            return bitmap;
        }
    }
}
