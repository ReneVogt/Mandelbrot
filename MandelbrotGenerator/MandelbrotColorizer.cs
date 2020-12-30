using System;
using System.Drawing;

namespace MandelbrotGenerator
{
    public class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new MandelbrotColorizer();
        public virtual Color SetColor => Color.Black;
        public virtual Color Colorize(int neededIterations, double squaredMagnitude, MandelbrotStatistics statistics)
        {
            double lowerLimit = Math.Max(1, statistics.AverageNeededIterations - Math.Sqrt(statistics.IterationVariance) / 2);
            double iterationIndex = neededIterations - Math.Min(1, squaredMagnitude / 30d);
            if (iterationIndex < lowerLimit) return Color.Black;

            double v = (iterationIndex - lowerLimit) / (statistics.MaximumNumberOfIterations - lowerLimit);
            double hue = 360d * iterationIndex;// - 60;
            if (hue >= 360) hue -= 360;
            if (hue < 0) hue += 360;
            return ColorFromHSV(hue, 1, v);
        }
        static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
            return hi switch
            {
                0 => Color.FromArgb(255, v, t, p),
                1 => Color.FromArgb(255, q, v, p),
                2 => Color.FromArgb(255, p, v, t),
                3 => Color.FromArgb(255, p, q, v),
                4 => Color.FromArgb(255, t, p, v),
                _ => Color.FromArgb(255, v, p, q)
            };
        }
    }
}
