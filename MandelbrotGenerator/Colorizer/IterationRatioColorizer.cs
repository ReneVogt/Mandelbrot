using System;
using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public sealed class IterationRatioColorizer : GenericValueColorizer<int>
    {
        /// <inheritdoc />
        public override Color SetColor => Color.Black;

        public IterationRatioColorizer() : base(false) { }

        /// <inheritdoc />
        public override int OnInitialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations)
        {
            base.OnInitialize(resolution, area, maximumNumberOfIterations);
            return maximumNumberOfIterations;
        }

        /// <inheritdoc />
        public override Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, int maxIterations)
        {
            if (iteratedPoint.Iterations <= 0) return SetColor;
            double magnitudeImpact = Math.Min(1, Math.Log(iteratedPoint.SquaredMagnitude) / Math.Log(2) / 5);
            double iterationIndex = iteratedPoint.Iterations - magnitudeImpact;
            double hue = 360d * Math.Pow(iterationIndex / maxIterations, Math.Pow(0.5, Math.Log10(maxIterations)));
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
