using System.Drawing;

namespace MandelbrotGenerator
{
    public delegate Color MandelbrotColorizer(int remainingIterations, int maxIterations, double magnitude);
}
