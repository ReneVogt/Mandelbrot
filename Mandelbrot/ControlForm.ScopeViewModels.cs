using System.ComponentModel;
using System.Numerics;

#nullable enable

namespace Mandelbrot
{
    public partial class ControlForm
    {
        sealed class ComplexViewModel
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }
            public override string ToString() => new Complex(Real, Imaginary).ToString();
        }
        sealed class ComplexTypeConverter : ExpandableObjectConverter
        {

        }
        sealed class ScopeViewModel
        {
            [DisplayName("Lower left corner")]
            [Category("Complex rectangle")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ComplexTypeConverter))]
            public ComplexViewModel LowerLeft { get; } = new ComplexViewModel();
            [DisplayName("Upper right corner")]
            [Category("Complex rectangle")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ComplexTypeConverter))]
            public ComplexViewModel UpperRight { get; } = new ComplexViewModel();
        }
    }
}