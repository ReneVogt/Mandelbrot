using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MandelbrotGenerator;

#nullable enable

namespace Mandelbrot
{
    public class AnalyticColorizer : MandelbrotColorizer
    {
        readonly List<int> iterations = new List<int>();
        readonly List<double> magnitudes = new List<double>();

        public void Reset()
        {
            lock(iterations) iterations.Clear();
            lock(magnitudes) magnitudes.Clear();
        }
        public void DumpAnalytics()
        {
            lock (iterations)
                Console.WriteLine($"ITERATIONS: {iterations.Min()} {iterations.Average()} {iterations.Max()}");
            lock (magnitudes)
                Console.WriteLine($"MAGNITUDES: {magnitudes.Min()} {magnitudes.Average()} {magnitudes.Max()}");
        }

        public override Color GetOutsideColor(int neededIterations, int maximumIterations, double squaredMagnitude)
        {
            lock(iterations) iterations.Add(neededIterations);
            lock(magnitudes) magnitudes.Add(squaredMagnitude);
            return base.GetOutsideColor(neededIterations, maximumIterations, squaredMagnitude);
        }
    }
}
