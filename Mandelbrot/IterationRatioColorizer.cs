using System;
using System.Drawing;
using MandelbrotGenerator;

#nullable enable

namespace Mandelbrot
{
    sealed class IterationRatioColorizer : MandelbrotColorizer
    {
        public override Color SetColor => Color.Black;
        protected override Color GetImmediateColor(int x, int y, double r, double i, int neededIterations, int maxIterations, double squaredMagnitude)
        {
            if (neededIterations <= 0) return Color.LightBlue;
            double d = Math.Min(1, Math.Log(squaredMagnitude) / Math.Log(2) / 5);
            double iterationIndex = neededIterations - d;
            double v = Math.Sqrt(iterationIndex / maxIterations);
            double hue = 360d * v;
            return ConvertHsvToRgb(hue, 1, v);
        }
    }
}
