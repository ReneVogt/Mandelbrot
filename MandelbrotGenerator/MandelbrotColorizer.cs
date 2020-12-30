using System.Drawing;

namespace MandelbrotGenerator
{
    public class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new MandelbrotColorizer();
        public virtual Color GetInsideColor() => Color.Black;
        public virtual Color GetOutsideColor(int neededIterations, int maximumIterations, double squaredMagnitude)
        {
            if (neededIterations == 0) return Color.Black;
            double q = 2 * neededIterations / (double)maximumIterations;
            if (q > 1) q = 1;
            int r = (int)(255 * q);
            return Color.FromArgb(255, r, r, r);
        }
    }
}
