using System;
using System.Numerics;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace MandelbrotGenerator
{
    /// <summary>
    /// Represents a rectangular scope in the complex plain, defined
    /// by its "lower left corner" (<see cref="LowerLeft"/>) (that is,
    /// the point of the rectangle with the smallest real and imaginary values),
    /// and its "upper right corner" (<see cref="UpperRight"/>) (that is,
    /// the point of the rectangle with the greatest real and imaginary values).
    /// </summary>
    public sealed class ComplexScope : IEquatable<ComplexScope>
    {
        #region Constants
        static readonly ArgumentException invalidScopeException = new("The specified scope coordinates are invalid. They must define a non-empty positive finite rectangle in the complex plane.");
        #endregion
        #region Static properties
        /// <summary>
        /// The default scope for the Mandelbrot image creation. Ranging
        /// from <c>-2 - 2i</c> to <c>2 + 2i</c>.
        /// </summary>
        public static ComplexScope Mandelbrot { get; } = ((-2, -2), (2, 2));
        #endregion
        #region Properties
        /// <summary>
        /// Gets the "lower left corner" of this complex rectangle. This is the
        /// point of the rectangle with the smallest real and imaginary values.
        /// </summary>
        public Complex LowerLeft { get; }
        /// <summary>
        /// Gets the "upper right corner" of this complex rectangle. This is the
        /// point of the rectangle with the greates real and imaginary values.
        /// </summary>
        public Complex UpperRight { get; }
        /// <summary>
        /// Gets the "width" of this complex rectangle. The difference of the
        /// real parts of <see cref="UpperRight"/> and <see cref="LowerLeft"/>.
        /// </summary>
        public double Real => UpperRight.Real - LowerLeft.Real;
        /// <summary>
        /// Gets the "height" of this complex rectangle. The difference of the
        /// imaginary parts of <see cref="UpperRight"/> and <see cref="LowerLeft"/>.
        /// </summary>
        public double Imaginary => UpperRight.Imaginary - LowerLeft.Imaginary;
        #endregion
        #region Construction
        /// <summary>
        /// Creates a new <see cref="ComplexScope"/> instance from the given coordinates.
        /// The coordinates must specify a non-empty positive rectangle in the complex plane.
        /// The <see cref="Complex.Real"/> and <see cref="Complex.Imaginary"/> values of
        /// <paramref name="lowerLeft"/> must be smaller than the respective values of
        /// <paramref name="upperRight"/>.
        /// </summary>
        /// <param name="lowerLeft">The "lower left corner" of the scope. This is the
        /// point of the rectangle with the smallest real and imaginary values.</param>
        /// <param name="upperRight">The "upper right corner" of the scope. This is the
        /// point of the rectangle with the greatest real and imaginary values.</param>
        /// <exception cref="ArgumentException">The scope coordinates are invalid. The
        /// real or imaginary part of <paramref name="lowerLeft"/> are not smaller than
        /// the respective part of <paramref name="upperRight"/>.</exception>
        public ComplexScope(Complex lowerLeft, Complex upperRight)
        {
            ValidateScope(lowerLeft, upperRight);
            LowerLeft = lowerLeft;
            UpperRight = upperRight;
        }
        static void ValidateScope(Complex lowerLeft, Complex upperRight)
        {
            if (double.IsNaN(lowerLeft.Real) || double.IsNaN(lowerLeft.Imaginary) ||
                double.IsNaN(upperRight.Real) || double.IsNaN(upperRight.Imaginary) || 
                double.IsInfinity(lowerLeft.Real) || double.IsInfinity(lowerLeft.Imaginary) ||
                double.IsInfinity(upperRight.Real) || double.IsInfinity(upperRight.Imaginary) ||
                lowerLeft.Real >= upperRight.Real || lowerLeft.Imaginary >= upperRight.Imaginary)
                throw invalidScopeException;
        }
        /// <summary>
        /// Deconstructs this <see cref="ComplexScope"/> into to tuples representing
        /// the lower left and upper right corners. Each tuple consists of two
        /// <see cref="double"/> values representing the real and imaginary parts.
        /// </summary>
        /// <param name="minr">The real part of the lower left corner.</param>
        /// <param name="mini">The imaginary part of the lower left corner.</param>
        /// <param name="maxr">The real part of the upper right corner.</param>
        /// <param name="maxi">The imaginary part of the upper right corner.</param>
        public void Deconstruct(out double minr, out double mini, out double maxr, out double maxi)
        {
            minr = LowerLeft.Real;
            mini = LowerLeft.Imaginary;
            maxr = UpperRight.Real;
            maxi = UpperRight.Imaginary;
        }
        #endregion
        #region Equality
        /// <summary>
        /// Compares two <see cref="ComplexScope"/> instances for equality.
        /// </summary>
        /// <param name="other">The <see cref="ComplexScope"/> to test for equality with this instance.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is not <c>null</c> and has equal <see cref="LowerLeft"/> and <see cref="UpperRight"/> values.</returns>
        public bool Equals(ComplexScope? other) => other is {LowerLeft: var lowerLeft, UpperRight: var upperRight} && LowerLeft.Equals(lowerLeft) &&
                                                   UpperRight.Equals(upperRight);
        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is ComplexScope other && Equals(other);
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(LowerLeft, UpperRight);
        #endregion
        #region ToString
        /// <inheritdoc />
        public override string ToString() => $"[{LowerLeft}; {UpperRight}]";
        #endregion
        #region Operators
        /// <summary>
        /// Creates a <see cref="ComplexScope"/> instance from two tuples representing
        /// the lower left and upper right corners of the scope. Each tuple needs to
        /// consist of two <see cref="double"/> values representing the real and
        /// imaginary parts.
        /// </summary>
        /// <returns>A new <see cref="ComplexScope"/> instance representing the scope defined
        /// by the given tuples.</returns>
        /// <exception cref="ArgumentException">The scope coordinates are invalid. The
        /// real or imaginary part of the first tuple is not smaller than
        /// the respective part of the second tuple.</exception>
        public static ComplexScope FromValueTuple(((double real, double imaginary) lowerLeft, (double real, double imaginary) upperRight) scope) =>
            scope;
        /// <summary>
        /// Creates a <see cref="ComplexScope"/> instance from two tuples representing
        /// the lower left and upper right corners of the scope. Each tuple needs to
        /// consist of two <see cref="double"/> values representing the real and
        /// imaginary parts.
        /// </summary>
        /// <returns>A new <see cref="ComplexScope"/> instance representing the scope defined
        /// by the given tuples.</returns>
        /// <exception cref="ArgumentException">The scope coordinates are invalid. The
        /// real or imaginary part of the first tuple is not smaller than
        /// the respective part of the second tuple.</exception>
        public static implicit operator ComplexScope(((double real, double imaginary) lowerLeft, (double real, double imaginary) upperRight) scope)
        {
            var ll = new Complex(scope.lowerLeft.real, scope.lowerLeft.imaginary);
            var ur = new Complex(scope.upperRight.real, scope.upperRight.imaginary);
            return new(ll, ur);
        }
        /// <summary>
        /// Compares two <see cref="ComplexScope"/> instances for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if either <paramref name="left"/> and <paramref name="right"/> are <c>null</c> or both have equal <see cref="LowerLeft"/> and <see cref="UpperRight"/> values.</returns>
        public static bool operator ==(ComplexScope? left, ComplexScope? right) => left?.Equals(right) ?? right is null;
        /// <summary>
        /// Compares two <see cref="ComplexScope"/> instances for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if either only <paramref name="left"/> or <paramref name="right"/> is <c>null</c> or both have inequal <see cref="LowerLeft"/> and <see cref="UpperRight"/> values.</returns>
        public static bool operator !=(ComplexScope? left, ComplexScope? right) => !(left?.Equals(right) ?? right is null);
        #endregion
    }
}
