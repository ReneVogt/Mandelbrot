using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    sealed class BlackAndWhiteColorizer : MandelbrotColorizer
    {
        public override Color SetColor => Color.Black;

        internal BlackAndWhiteColorizer()
            : base(false)
        {
        }

        public override Color GetColor(Point pixel, IteratedPoint iteratedPoint, object? userState) => iteratedPoint.Iterations == 0 ? Color.Black : Color.White;
    }
}
