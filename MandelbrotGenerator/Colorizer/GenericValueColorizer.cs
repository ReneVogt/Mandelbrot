using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public abstract class GenericValueColorizer<TState> : MandelbrotColorizer where TState : struct
    {
        /// <inheritdoc />
        protected GenericValueColorizer(bool usePostCalculationColorization)
            : base(usePostCalculationColorization) { }

        /// <inheritdoc />
        public sealed override object? Initialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations) =>
            OnInitialize(resolution, area, maximumNumberOfIterations);
        public virtual TState OnInitialize(Size resolution, MandelbrotArea area, int maximumNumberOfIterations) => default;
        /// <inheritdoc />
        public sealed override object? Initialize(MandelbrotPoint[] iteratedPoints, object? userState) => OnInitialize(iteratedPoints, userState is TState t ? t : default);
        public virtual TState OnInitialize(MandelbrotPoint[] iteratedPoints, TState userState) => default;
        /// <inheritdoc />
        public sealed override Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, object? userState) => GetColor(pixel, iteratedPoint, userState is TState t ? t : default);
        public abstract Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, TState userState);
    }
}
