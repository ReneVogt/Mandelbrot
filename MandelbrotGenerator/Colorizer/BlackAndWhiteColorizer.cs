using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    public sealed class BlackAndWhiteColorizer : MandelbrotColorizer
    {
        /// <inheritdoc />
        public override Color SetColor => Color.Black;

        public BlackAndWhiteColorizer()
            : base(false)
        {
        }

        /// <inheritdoc />
        public override Color GetColor(Point pixel, MandelbrotPoint iteratedPoint, object? userState) => iteratedPoint.Iterations == 0 ? Color.Black : Color.White;
    }
}
