using System.Drawing;

namespace MandelbrotGenerator
{
    public class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new MandelbrotColorizer();
        public virtual Color GetInsideColor() => Color.Black;
        public virtual Color GetOutsideColor(int neededIterations, int maximumIterations, double squaredMagnitude)
        {
            int r = 256 * neededIterations / maximumIterations;
            return Color.FromArgb(r, r, r);

        }
    }
}
