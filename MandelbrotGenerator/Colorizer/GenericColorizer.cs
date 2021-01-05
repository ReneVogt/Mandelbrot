using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public abstract class GenericColorizer<TState> : MandelbrotColorizer where TState : class
    {
        /// <inheritdoc />
        protected GenericColorizer(bool usePostCalculationColorization)
            : base(usePostCalculationColorization) { }

        /// <inheritdoc />
        public sealed override object? Initialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations) =>
            OnInitialize(resolution, area, maximumNumberOfIterations);
        public virtual TState? OnInitialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations) => default;
        /// <inheritdoc />
        public sealed override object? Initialize(MandelbrotPoint[] iteratedPoints, object? userState) => OnInitialize(iteratedPoints, (TState?)userState);
        public virtual TState? OnInitialize(MandelbrotPoint[] iteratedPoints, TState? userState) => default;
        /// <inheritdoc />
        public sealed override Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, object? userState) => GetColor(pixel, iteratedPoint, (TState?)userState);
        public abstract Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, TState? userState);
    }
}
