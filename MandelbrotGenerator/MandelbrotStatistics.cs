namespace MandelbrotGenerator
{
    public readonly struct MandelbrotStatistics
    {
        public int MaximumNumberOfIterations { get; }

        internal MandelbrotStatistics(int maximumNumberOfIterations, MandelbrotPoint[] points)
        {
            MaximumNumberOfIterations = maximumNumberOfIterations;
        }
    }
}
