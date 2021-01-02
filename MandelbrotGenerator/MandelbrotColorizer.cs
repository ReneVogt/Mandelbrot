using System;
using System.Collections.Generic;
using System.Drawing;

namespace MandelbrotGenerator
{
    public class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new MandelbrotColorizer();
        public bool UsePostCalculationColorization{ get; }
        
        public virtual Color SetColor { get; } = Color.Black;

        public MandelbrotColorizer(bool usePostCalculationColorization = false) => UsePostCalculationColorization = usePostCalculationColorization;

        public Color GetColor(int x, int y, double r, double i, int neededIterations, int maxIterations,
                              double squaredMagnitude) => UsePostCalculationColorization
                                                              ? throw new InvalidOperationException(
                                                                    $"This {GetType().Name} does not support immediate colorization!")
                                                              : GetImmediateColor(x, y, r, i, neededIterations, maxIterations, squaredMagnitude);
        public void PostCalculation_Initialize(IReadOnlyList<MandelbrotPoint> rawData, int maxIterations)
        {
            if (!UsePostCalculationColorization) throw new InvalidOperationException($"This {GetType().Name} does not support post calculation colorization!");
            InitializePostCalculationColorization(rawData, maxIterations);
        }
        public Color PostCalculation_GetColor(int x, int y, double r, double i, int neededIterations,
                                              double squaredMagnitude) => UsePostCalculationColorization
                                                                              ? GetPostCalculationColor(
                                                                                  x, y, r, i, neededIterations, squaredMagnitude)
                                                                              : throw new InvalidOperationException(
                                                                                    $"This {GetType().Name} does not support post calculation colorization!");

        protected virtual Color GetImmediateColor(int x, int y, double r, double i, int neededIterations, int maxIterations,
                                                  double squaredMagnitude) => UsePostCalculationColorization
                                                                                  ? throw new InvalidOperationException(
                                                                                        $"This {GetType().Name} does not support immediate colorization!")
                                                                                  : neededIterations == 0
                                                                                      ? Color.Black
                                                                                      : Color.White;
        protected virtual void InitializePostCalculationColorization(IReadOnlyList<MandelbrotPoint> rawData, int maxIterations)
        {
            if (!UsePostCalculationColorization) throw new InvalidOperationException($"This {GetType().Name} does not support post calculation colorization!");
        }
        protected virtual Color GetPostCalculationColor(int x, int y, double r, double i, int neededIterations,
                                                        double squaredMagnitude) => UsePostCalculationColorization
                                                                                        ? neededIterations == 0 ? Color.Black : Color.White
                                                                                        : throw new InvalidOperationException(
                                                                                              $"This {GetType().Name} does not support post calculation colorization!");
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
