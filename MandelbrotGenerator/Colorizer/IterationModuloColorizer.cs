using System;
using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public sealed class IterationModuloColorizer : MandelbrotColorizer
    {
        /// <inheritdoc />
        public override Color SetColor => Color.Black;

        public IterationModuloColorizer() : base(false){}

        /// <inheritdoc />
        public override Color GetColor(Point pixel, IteratedPoint iteratedPoint, object? userState)
        {
            if (iteratedPoint.Iterations <= 0) return SetColor;
            double magnitudeImpact = Math.Min(1, Math.Log(iteratedPoint.Z.Magnitude) / Math.Log(2) / 3);
            double hue = iteratedPoint.Iterations - magnitudeImpact;
            while (hue < 0) hue += 360;
            if (hue > 360)
                hue -= Math.Floor(hue / 360) * 360;
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
