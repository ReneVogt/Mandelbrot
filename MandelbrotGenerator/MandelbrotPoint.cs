using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace MandelbrotGenerator
{
    readonly struct MandelbrotPoint
    {
        internal double Real { get; }
        internal double Imaginary { get; }
        internal bool Set { get; }
        internal int Iterations { get; }
        internal double LastMagnitude { get; }
        MandelbrotPoint(double real, double imaginary)
            : this(real, imaginary, 0, 0)
        {
            Set = true;
        }
        MandelbrotPoint(double real, double imaginary, int iterations, double lastMagnitude)
        {
            Real = real;
            Imaginary = imaginary;
            Set = false;
            Iterations = iterations;
            LastMagnitude = lastMagnitude;
        }
        internal static MandelbrotPoint Calculate(double real, double imaginary, int maxIterations, CancellationToken cancellationToken = default)
        {
            double r = real, i = imaginary;
            HashSet<(double, double)> knownPoints = new HashSet<(double, double)>
            {
                (r, i)
            };

            for (int iteration = maxIterations; iteration > 0; iteration--)
            {
                cancellationToken.ThrowIfCancellationRequested();
                double r2 = r * r - i * i + real;
                i = 2 * r * i + imaginary;
                r = r2;

                if (!knownPoints.Add((r, i)))
                    return new MandelbrotPoint(real, imaginary);

                double d = r * r + i * i;
                if (d > 4)
                    return new MandelbrotPoint(real, imaginary, iteration, Math.Sqrt(d));
            }

            return new MandelbrotPoint(real, imaginary);
        }
    }
}
