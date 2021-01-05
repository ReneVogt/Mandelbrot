using System;
using System.Globalization;

namespace MandelbrotGenerator
{
    public readonly struct MandelbrotArea : IEquatable<MandelbrotArea>, IFormattable
    {
        public static MandelbrotArea Default { get; } = (-2, -2, 2, 2);

        public double RealMin { get; }
        public double RealMax { get; }
        public double ImaginaryMin { get; }
        public double ImaginaryMax { get; }
        public double Real { get; }
        public double Imaginary { get; }

        public MandelbrotArea(double realMin, double imaginaryMin, double realMax, double imaginaryMax)
        {
            CheckArg(nameof(realMin), realMin);
            CheckArg(nameof(realMax), realMax);
            CheckArg(nameof(imaginaryMin), imaginaryMin);
            CheckArg(nameof(imaginaryMax), imaginaryMax);

            if (realMax < realMin || imaginaryMax < imaginaryMin)
                throw new ArgumentException("The specified area coordinates are invalid. They must define a non-empty positive rectangle in the complex plane.");

            RealMin = realMin;
            RealMax = realMax;
            Real = RealMax - RealMin;
            ImaginaryMin = imaginaryMin;
            ImaginaryMax = imaginaryMax;
            Imaginary = ImaginaryMax - ImaginaryMin;

            void CheckArg(string paramName, double actualValue)
            {
                if (double.IsNaN(actualValue) || double.IsInfinity(actualValue))
                    throw new ArgumentOutOfRangeException(message: $"The parameter `{paramName}' is not a valid number!", paramName: paramName, actualValue: actualValue);
            }
        }
        public void Deconstruct(out double realMin, out double imaginaryMin, out double realMax, out double imaginaryMax)
        {
            realMin = RealMin;
            realMax = RealMax;
            imaginaryMin = ImaginaryMin;
            imaginaryMax = ImaginaryMax;
        }
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
        /// <inheritdoc />
        public override string ToString() => ToString("G20", CultureInfo.CurrentCulture);
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);
        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider) => format switch
        {
            _ => ToCustomString(format, formatProvider)
        };
        string ToCustomString(string format, IFormatProvider formatProvider) => string.Format(formatProvider, $"[({{0:{format}}}; {{1:{format}}}), ({{2:{format}}}; {{3:{format}}})]", RealMin, ImaginaryMin, RealMax, ImaginaryMax);

        public static implicit operator MandelbrotArea((double realMin, double imaginaryMin, double realMax, double imaginaryMax) tuple) =>
            new MandelbrotArea(tuple.realMin, tuple.imaginaryMin, tuple.realMax, tuple.imaginaryMax);
        public static bool operator ==(MandelbrotArea left, MandelbrotArea right) => left.Equals(right);
        public static bool operator !=(MandelbrotArea left, MandelbrotArea right) => !left.Equals(right);

        public static MandelbrotArea Parse(string s) => Parse(s, NumberStyles.Any, CultureInfo.CurrentCulture);
        public static MandelbrotArea Parse(string s, IFormatProvider formatProvider) => Parse(s, NumberStyles.Any, formatProvider);
        public static MandelbrotArea Parse(string s, NumberStyles style, IFormatProvider formatProvider) => throw new NotImplementedException();

        public static bool TryParse(string s, out MandelbrotArea area) => TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out area);
        public static bool TryParse(string s, IFormatProvider formatProvider, out MandelbrotArea area) =>
            TryParse(s, NumberStyles.Any, formatProvider, out area);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out MandelbrotArea area)
        {
            area = default;
            try
            {
                area = Parse(s, style, formatProvider);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
