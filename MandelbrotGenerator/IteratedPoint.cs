using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace MandelbrotGenerator
{
    /// <summary>
    /// Represents the element in the sequence <c>z[n+1] = z[n]*z[n] + c</c>
    /// of a given <see cref="Complex"/> number <c>c</c> and <c>z[0] = 0</c>
    /// after <see cref="Iterations"/> iterations.
    /// </summary>
    public sealed class IteratedPoint
    {
        /// <summary>
        /// The seed of the sequence. This is the point in the complex plane for
        /// which the sequence's behaviour determines wether it belongs to the
        /// Mandelbrot set or not.
        /// </summary>
        public Complex C { get; }
        /// <summary>
        /// The result of the iterations. This is the <see cref="Iterations"/>th element in
        /// the sequence. If its <see cref="Complex.Magnitude"/> is greater than 2, the sequence diverges and <see cref="C"/>
        /// does not belong to the Mandelbrot set. If it is less than or equal to 2, the sequence did not escape after <see cref="Iterations"/> iterations.
        /// </summary>
        public Complex Z { get; }
        /// <summary>
        /// Gets a value that indicates wether <see cref="C"/> belongs to the Mandelbrot set.
        /// If the <see cref="Complex.Magnitude"/> of <see cref="Z"/> is greater than 2, <see cref="C"/>
        /// does not belong to the set and <c>false</c> is returned. If it is less than or equal to 2, <see cref="C"/>
        /// is assumed to belong to the set and <c>true</c> is returned.
        /// </summary>
        public bool Set => Z.Magnitude <= 2;
        /// <summary>
        /// The number of iterations calculated to this point.
        /// </summary>
        public int Iterations { get; }

        IteratedPoint(Complex c) : this(c, c, -1){}
        IteratedPoint(Complex c, Complex z, int iterations)
        {
            C = c;
            Z = z;
            Iterations = iterations;
        }
        internal static IteratedPoint Iterate(Complex c, int maxIterations, CancellationToken cancellationToken = default)
        {
            if (c.Magnitude > 2)
                return new IteratedPoint(c);

            var knownPoints = new HashSet<Complex>
            {
                c
            };

            Complex squareCache = new Complex(c.Real * c.Real, c.Imaginary * c.Imaginary);
            var z = c;
            for (int iteration = 1; iteration <= maxIterations; iteration++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                z = new Complex(squareCache.Real - squareCache.Imaginary, 2 * z.Real * z.Imaginary) + c;

                if (!knownPoints.Add(z))
                    return new IteratedPoint(z);

                squareCache = new Complex(z.Real * z.Real, z.Imaginary * z.Imaginary);
                if (squareCache.Real + squareCache.Imaginary > 4)
                    return new IteratedPoint(c, z, iteration);
            }

            return new IteratedPoint(c, z, maxIterations);
        }
    }
}
