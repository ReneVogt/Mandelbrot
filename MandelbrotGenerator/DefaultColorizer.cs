using System;
using System.Drawing;

namespace MandelbrotGenerator
{
    sealed class DefaultColorizer : MandelbrotColorizer
    {
        public override bool UsePostCalculationColorization => false;
        public override Color SetColor => Color.Black;
        public override Color ImmediateColorization(int x, int y, double r, double i, int neededIterations, int maxIterations, double squaredMagnitude)
        {
            if (neededIterations <= 0) return Color.Black;
            double d = Math.Min(1, Math.Log(squaredMagnitude) / Math.Log(2) / 6);
            double iterationIndex = neededIterations - d;
            double v = Math.Sqrt(iterationIndex / maxIterations);
            double hue = 360d * v;
            return ConvertHsvToRgb(hue, 1, v);
        }
        public override void InitializePostCalculationColorization(MandelbrotPoint[] rawData, int maxIterations)
        {
        }
        public override Color PostCalculationColorization(int x, int y, double r, double i, int neededIterations, int maxIterations,
                                                          double squaredMagnitude) => Color.Black;
    }
}
