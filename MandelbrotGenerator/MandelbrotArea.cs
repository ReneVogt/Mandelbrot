namespace MandelbrotGenerator
{
    public readonly struct MandelbrotArea
    {
        public static MandelbrotArea Default { get; } = (-2, 2, -2, 2);

        public double RealMin { get; }
        public double RealMax { get; }
        public double ImaginaryMin { get; }
        public double ImaginaryMax { get; }
        public MandelbrotArea(double realMin, double realMax, double imaginaryMin, double imaginaryMax)
        {
            RealMin = realMin;
            RealMax = realMax;
            ImaginaryMin = imaginaryMin;
            ImaginaryMax = imaginaryMax;
        }
        public void Deconstruct(out double realMin, out double realMax, out double imaginaryMin, out double imaginaryMax)
        {
            realMin = RealMin;
            realMax = RealMax;
            imaginaryMin = ImaginaryMin;
            imaginaryMax = ImaginaryMax;
        }
        public static implicit operator MandelbrotArea((double realMin, double realMax, double imaginaryMin, double imaginaryMax) tuple) =>
            new MandelbrotArea(tuple.realMin, tuple.realMax, tuple.imaginaryMin, tuple.imaginaryMax);
    }
}
