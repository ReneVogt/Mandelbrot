using System.Collections.Generic;
using System.Threading;

namespace MandelbrotGenerator
{
    public readonly struct MandelbrotPoint
    {
        public double Real { get; }
        public double Imaginary { get; }
        public bool Set { get; }
        public int Iterations { get; }
        public double SquaredMagnitude { get; }
        MandelbrotPoint(double real, double imaginary)
            : this(real, imaginary, 0, 0)
        {
            Set = true;
        }
        MandelbrotPoint(double real, double imaginary, int iterations, double squaredMagnitude)
        {
            Real = real;
            Imaginary = imaginary;
            Set = false;
            Iterations = iterations;
            SquaredMagnitude = squaredMagnitude;
        }
        internal static MandelbrotPoint Calculate(double real, double imaginary, int maxIterations, CancellationToken cancellationToken = default)
        {
            double r = real, i = imaginary;
            HashSet<(double, double)> knownPoints = new HashSet<(double, double)>
            {
                (r, i)
            };

            double oldR2 = r * r;
            double oldI2 = i * i;
            double magnitude = oldR2 + oldI2;
            if (magnitude > 4)
                return new MandelbrotPoint(real, imaginary, 0, magnitude);

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                i = 2 * r * i + imaginary;
                r = oldR2 - oldI2 + real;

                if (!knownPoints.Add((r, i)))
                    return new MandelbrotPoint(real, imaginary);

                oldR2 = r * r;
                oldI2 = i * i;
                magnitude = oldR2 + oldI2;
                if (magnitude > 4)
                    return new MandelbrotPoint(real, imaginary, iteration, magnitude);
            }

            return new MandelbrotPoint(real, imaginary);
        }
    }
}
