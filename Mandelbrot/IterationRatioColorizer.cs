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
            if (neededIterations <= 0) return SetColor;
            double magnitudeImpact = Math.Min(1, Math.Log(squaredMagnitude) / Math.Log(2) / 5);
            double iterationIndex = neededIterations - magnitudeImpact;
            double hue = 360d * Math.Pow(iterationIndex / maxIterations, Math.Pow(0.5, Math.Log10(maxIterations)));
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
