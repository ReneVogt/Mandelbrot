using System;

namespace MandelbrotGenerator
{
    public readonly struct MandelbrotArea : IEquatable<MandelbrotArea>
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
        public static bool operator ==(MandelbrotArea left, MandelbrotArea right) => left.Equals(right);
        public static bool operator !=(MandelbrotArea left, MandelbrotArea right) => !left.Equals(right);
        /// <inheritdoc />
        public bool Equals(MandelbrotArea other) => RealMin.Equals(other.RealMin) && RealMax.Equals(other.RealMax) && ImaginaryMin.Equals(other.ImaginaryMin) && ImaginaryMax.Equals(other.ImaginaryMax);
        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is MandelbrotArea other && Equals(other);
        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = RealMin.GetHashCode();
                hashCode = (hashCode * 397) ^ RealMax.GetHashCode();
                hashCode = (hashCode * 397) ^ ImaginaryMin.GetHashCode();
                hashCode = (hashCode * 397) ^ ImaginaryMax.GetHashCode();
                return hashCode;
            }
        }
    }
}
