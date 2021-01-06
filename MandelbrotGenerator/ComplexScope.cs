using System;
using System.Globalization;
using System.Numerics;

namespace MandelbrotGenerator
{
    public sealed class ComplexScope : IEquatable<ComplexScope>, IFormattable
    {
        #region Static properties
        public static ComplexScope Mandelbrot { get; } = ((-2, -2), (2, 2));
        #endregion
        #region Properties
        public Complex LowerLeft { get; }
        public Complex UpperRight { get; }
        public double Real => UpperRight.Real - LowerLeft.Real;
        public double Imaginary => UpperRight.Imaginary - LowerLeft.Imaginary;
        #endregion
        #region Construction
        public ComplexScope(Complex lowerLeft, Complex upperRight)
        {
            if (lowerLeft.Real >= upperRight.Real || lowerLeft.Imaginary >= upperRight.Imaginary)
                throw new ArgumentException("The specified scope coordinates are invalid. They must define a non-empty positive rectangle in the complex plane.");
            LowerLeft = lowerLeft;
            UpperRight = upperRight;
        }
        public void Deconstruct(out (double real, double imaginary) lowerLeft, out (double real, double imaginary) upperRight)
        {
            lowerLeft = (LowerLeft.Real, LowerLeft.Imaginary);
            upperRight = (UpperRight.Real, UpperRight.Imaginary);
        }
        #endregion
        #region Equality
        public bool Equals(ComplexScope? other) => other is {LowerLeft: var lowerLeft, UpperRight: var upperRight} && LowerLeft.Equals(lowerLeft) &&
                                                   UpperRight.Equals(upperRight);
        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is ComplexScope other && Equals(other);
        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = LowerLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ UpperRight.GetHashCode();
                return hashCode;
            }
        }
        #endregion
        #region ToString
        /// <inheritdoc />
        public override string ToString() => ToString("G20", CultureInfo.CurrentCulture);
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);
        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider) => format switch
        {
            _ => ToCustomString(format, formatProvider)
        };
        //string ToCustomString(string format, IFormatProvider formatProvider) => string.Format(formatProvider, $"[({{0:{format}}}; {{1:{format}}}), ({{2:{format}}}; {{3:{format}}})]", RealMin, ImaginaryMin, RealMax, ImaginaryMax);
        string ToCustomString(string format, IFormatProvider formatProvider) => string.Format(formatProvider, $"[{{0:{format}}}; {{1:{format}}}]", LowerLeft, UpperRight);
        #endregion
        #region Operators
        public static ComplexScope FromValueTuple(((double real, double imaginary) lowerLeft, (double real, double imaginary) upperRight) scope) =>
            scope;
        public static implicit operator ComplexScope(((double real, double imaginary) lowerLeft, (double real, double imaginary) upperRight) scope)
        {
            var ll = new Complex(scope.lowerLeft.real, scope.lowerLeft.imaginary);
            var ur = new Complex(scope.upperRight.real, scope.upperRight.imaginary);
            return new ComplexScope(ll, ur);
        }
        public static bool operator ==(ComplexScope? left, ComplexScope? right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(ComplexScope? left, ComplexScope? right) => !(left?.Equals(right) ?? right is null);
        #endregion
        #region Parse
        public static ComplexScope Parse(string s) => Parse(s, NumberStyles.Any, CultureInfo.CurrentCulture);
        public static ComplexScope Parse(string s, IFormatProvider formatProvider) => Parse(s, NumberStyles.Any, formatProvider);
        public static ComplexScope Parse(string s, NumberStyles style, IFormatProvider formatProvider) => throw new NotImplementedException();

        public static bool TryParse(string s, out ComplexScope area) => TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out area);
        public static bool TryParse(string s, IFormatProvider formatProvider, out ComplexScope area) =>
            TryParse(s, NumberStyles.Any, formatProvider, out area);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out ComplexScope area)
        {
            area = default!;
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
        #endregion
    }
}
