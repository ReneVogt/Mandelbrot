using System;
using System.Drawing;
using MandelbrotGenerator;

#nullable enable

namespace Mandelbrot
{
    sealed class IterationRoundTripColorizer : MandelbrotColorizer
    {
        public override Color SetColor => Color.Black;
        protected override Color GetImmediateColor(int x, int y, double r, double i, int neededIterations, int maxIterations, double squaredMagnitude)
        {
            if (neededIterations <= 0) return Color.DarkSlateGray;
            double hue = neededIterations - Math.Log(Math.Log(squaredMagnitude) / Math.Log(4)) / Math.Log(2);
            while (hue < 0) hue += 360;
            if (hue > 360)
                hue -= Math.Floor(hue / 360) * 360;
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
