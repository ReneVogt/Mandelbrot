using System;
using System.Linq;

namespace MandelbrotGenerator
{
    public readonly struct MandelbrotStatistics
    {
        public int MaximumNumberOfIterations { get; }
        public MandelbrotPoint[] RawData { get; }
        public double AverageNeededIterations { get; }
        public double IterationVariance { get; }

        internal MandelbrotStatistics(int maximumNumberOfIterations, MandelbrotPoint[] points)
        {
            MaximumNumberOfIterations = maximumNumberOfIterations;
            RawData = points;

            var pois = RawData.Where(p => !p.Set && p.Iterations > 0).ToArray();
            var avgit = AverageNeededIterations = pois.Average(p => p.Iterations);

            IterationVariance = pois.Average(p => (p.Iterations - avgit) * (p.Iterations - avgit));
        }
    }
}
