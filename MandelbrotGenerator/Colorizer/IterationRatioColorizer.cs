using System;
using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    sealed class IterationRatioColorizer : MandelbrotColorizer
    {
        sealed class UserState
        {
            internal int MaximumNumberOfIterations { get; }
            internal UserState(int maximumNumberOfIterations) =>  MaximumNumberOfIterations = maximumNumberOfIterations;
        }

        /// <inheritdoc />
        public override Color SetColor => Color.Black;

        internal IterationRatioColorizer() : base(false) { }

        /// <inheritdoc />
        public override object Initialize(Size resolution, ComplexScope scope, int maximumNumberOfIterations)
        {
            base.Initialize(resolution, scope, maximumNumberOfIterations);
            return new UserState(maximumNumberOfIterations);
        }
        /// <inheritdoc />
        public override Color GetColor(Point pixel, IteratedPoint iteratedPoint, object? userState)
        {
            if (!(userState is UserState { MaximumNumberOfIterations: var maxIterations})) 
                        throw new ArgumentException($"{nameof(userState)} must be an instance of {nameof(UserState)}!", nameof(userState));
            if (iteratedPoint.Iterations <= 0) return SetColor;
            double magnitudeImpact = Math.Min(1, Math.Log(iteratedPoint.Z.Magnitude) / Math.Log(2) / 3);
            double iterationIndex = iteratedPoint.Iterations - magnitudeImpact;
            double hue = 360d * Math.Pow(iterationIndex / maxIterations, Math.Pow(0.5, Math.Log10(maxIterations)));
            return ConvertHsvToRgb(hue, 1, 1);
        }
    }
}
