using System;
using System.Drawing;

namespace MandelbrotGenerator
{
    public abstract class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new DefaultColorizer();
        public abstract bool UsePostCalculationColorization{ get; }
        public abstract Color SetColor { get; }
        public abstract Color ImmediateColorization(int x, int y, double r, double i, int neededIterations, int maxIterations,
                                                    double squaredMagnitude);
        public abstract void InitializePostCalculationColorization(MandelbrotPoint[] rawData, int maxIterations);
        public abstract Color PostCalculationColorization(int x, int y, double r, double i, int neededIterations, int maxIterations,
                                                    double squaredMagnitude);
        protected static Color ConvertHsvToRgb(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
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
