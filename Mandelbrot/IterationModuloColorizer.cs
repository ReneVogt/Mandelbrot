using System;
using System.Drawing;
using MandelbrotGenerator;

#nullable enable

namespace Mandelbrot
{
    sealed class IterationModuloColorizer : MandelbrotColorizer
    {
        public override Color SetColor => Color.Black;
        protected override Color GetImmediateColor(int x, int y, double r, double i, int neededIterations, int maxIterations, double squaredMagnitude)
        {
            if (neededIterations <= 0) return SetColor;
            double magnitudeImpact = Math.Min(1, Math.Log(squaredMagnitude) / Math.Log(2) / 5);
            double hue = neededIterations - magnitudeImpact;
            while (hue < 0) hue += 360;
            if (hue > 360)
                hue -= Math.Floor(hue / 360) * 360;
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
