using System;
using System.Drawing;

namespace MandelbrotGenerator.Colorizer
{
    /// <summary>
    /// Base class for colorization implementations.
    /// The colorization can be run during the actual
    /// calculation (using only the iteration count and
    /// escaping z value) or after the calculation (with
    /// the possibiilty to do statistics over all iterated
    /// pixels).
    /// </summary>
    public abstract class MandelbrotColorizer
    {
        /// <summary>
        /// A colorizer that uses <see cref="Color.Black"/>for pixels inside the set
        /// and <see cref="Color.White"/> for pixels outside of the set.
        /// </summary>
        public static MandelbrotColorizer BlackAndWhite { get; } = new BlackAndWhiteColorizer();
        /// <summary>
        /// A colorizer that uses <see cref="Color.Black"/>for pixels inside the set
        /// and a hue for the color of pixels outside of the set depending on the ratio of
        /// iterations needed to escape to the maximum iteration count. The escaping magnitude
        /// is also used for fine tuning.
        /// </summary>
        public static MandelbrotColorizer IterationRatioColorizer { get; } = new IterationRatioColorizer();
        /// <summary>
        /// A colorizer that uses <see cref="Color.Black"/>for pixels inside the set
        /// and the number of iterations needed to escape as the hue angle degrees for
        /// the color of pixels outside of the set. The escaping magnitude
        /// is also used for fine tuning.
        /// </summary>
        public static MandelbrotColorizer IterationModuloColorizer { get; } = new IterationModuloColorizer();
        internal bool UsePostCalculationColorization{ get; }
        
        /// <summary>
        /// Gets the color to use for pixels inside the set.
        /// </summary>
        public abstract Color SetColor { get; }

        /// <summary>
        /// Creates a new <see cref="MandelbrotColorizer"/> instance.
        /// </summary>
        /// <param name="usePostCalculationColorization">Indicates wether this colorizer uses immediate colorization (while calculation is running;
        /// using only the number of iterations and escaping z value) or post calculation colorization (with the possibility to do statistics
        /// on the overall calculation result.</param>
        protected MandelbrotColorizer(bool usePostCalculationColorization) => UsePostCalculationColorization = usePostCalculationColorization;

        /// <summary>
        /// This method is called by the <see cref="MandelbrotBitmapGenerator"/> when calculation starts.
        /// Override this method to initialize your colorizer with the given <paramref name="resolution"/>,
        /// <paramref name="scope"/> and <paramref name="maximumNumberOfIterations"/>. To keep your colorizer
        /// thread and usage safe, return your state and it will be provided as parameter to the other colorization methods.
        /// </summary>
        /// <param name="resolution">The used raster resolution.</param>
        /// <param name="scope">The scope in the complex plane that is analyzed.</param>
        /// <param name="maximumNumberOfIterations">The maximum number of iterations used by the <see cref="MandelbrotBitmapGenerator"/>.</param>
        /// <returns>A state object that is passed on to colorization methods. It is recommended to use this way to pass state instead of
        /// keeping shared fields in your colorizer.</returns>
        public virtual object? Initialize(Size resolution, ComplexScope scope, int maximumNumberOfIterations)
        {
            return null;
        }
        /// <summary>
        /// This method is called by the <see cref="MandelbrotBitmapGenerator"/> when calculation has finished
        /// to initialize post calculation colorization. It is only called if this colorizer was created with
        /// <see cref="UsePostCalculationColorization"/> set to <c>true</c>.
        /// Override this method to initialize your colorizer using the calculation result given in <paramref name="iteratedPoints"/>.
        /// To pass your analysis result on to the <see cref="GetColor"/> method, return it as state object from this method.
        /// </summary>
        /// <param name="iteratedPoints">The result of the calculation. The array's size
        /// depends on the of the <c>resolution</c> parameter of the earlier called <see cref="Initialize(Size,ComplexScope,int)"/> method.
        /// An elements index is <c>row*width+column</c>. See <see cref="IteratedPoint"/> for details about the per pixel information.</param>
        /// <param name="userState">The state object returned by the earlier called <see cref="Initialize(Size,ComplexScope,int)"/> method.</param>
        /// <returns>A state object that is passed on to <see cref="GetColor"/>. It is recommended to use this way to pass state instead of
        /// keeping shared fields in your colorizer.</returns>
        public virtual object? Initialize(IteratedPoint[] iteratedPoints, object? userState)
        {
            return null;
        }
        /// <summary>
        /// This method is called by the <see cref="MandelbrotBitmapGenerator"/> to determine the color of
        /// an calculated pixel. The calculation result is stored in <paramref name="iteratedPoint"/>. See
        /// <see cref="IteratedPoint"/> for details about information.
        /// </summary>
        /// <param name="pixel">The raster pixel that should be colorized.</param>
        /// <param name="iteratedPoint">The result of the calculation for this pixel. See <see cref="IteratedPoint"/>
        /// for details about the per pixel information</param>
        /// <param name="userState">The state object returned by the earlier called <see cref="Initialize(Size,ComplexScope,int)"/> or <see cref="Initialize(IteratedPoint[], object)"/> method..</param>
        /// <returns>The color to use for the given pixel.</returns>
        public abstract Color GetColor(Point pixel, IteratedPoint iteratedPoint, object? userState);
        
        /// <summary>
        /// Converts a hsv color information into a <see cref="Color"/>.
        /// </summary>
        /// <param name="hue">The hue of the color (0 to 360 degree).</param>
        /// <param name="saturation">The saturation of the color (0 to 1).</param>
        /// <param name="value">The value (intensity) of the color (0 to 1).</param>
        /// <returns>The <see cref="Color"/> representing the hsv color values.</returns>
        protected static Color ConvertHsvToRgb(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
            return hi switch
            {
                0 => Color.FromArgb(255, v, t, p),
                1 => Color.FromArgb(255, q, v, p),
                2 => Color.FromArgb(255, p, v, t),
                3 => Color.FromArgb(255, p, q, v),
                4 => Color.FromArgb(255, t, p, v),
                _ => Color.FromArgb(255, v, p, q)
            };
        }
    }
}
