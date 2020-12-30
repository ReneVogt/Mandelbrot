using System.Drawing;

namespace MandelbrotGenerator
{
    public class MandelbrotColorizer
    {
        public static MandelbrotColorizer Default { get; } = new MandelbrotColorizer();
        public virtual Color SetColor => Color.Black;
        public virtual Color Colorize(int neededIterations, double squaredMagnitude, MandelbrotStatistics statistics)
        {
            if (neededIterations == 0) return Color.Black;
            double q = 2 * neededIterations / (double)statistics.MaximumNumberOfIterations;
            if (q > 1) q = 1;
            int r = (int)(255 * q);
            return Color.FromArgb(255, r, r, r);
        }
    }
}
