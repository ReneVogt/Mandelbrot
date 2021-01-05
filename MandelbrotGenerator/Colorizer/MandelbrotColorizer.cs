using System;
using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public abstract class MandelbrotColorizer
    {
        public static MandelbrotColorizer BlackAndWhite { get; } = new BlackAndWhiteColorizer();
        public static MandelbrotColorizer IterationRatioColorizer { get; } = new IterationRatioColorizer();
        public static MandelbrotColorizer IterationModuloColorizer { get; } = new IterationModuloColorizer();
        internal bool UsePostCalculationColorization{ get; }
        
        public abstract Color SetColor { get; }

        protected MandelbrotColorizer(bool usePostCalculationColorization) => UsePostCalculationColorization = usePostCalculationColorization;

        public virtual object? Initialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations)
        {
            return null;
        }
        public virtual object? Initialize(MandelbrotPoint[] iteratedPoints, object? userState)
        {
            return null;
        }
        public abstract Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, object? userState);
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
