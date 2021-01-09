using System.ComponentModel;
using System.Drawing;

// ReSharper disable UnusedAutoPropertyAccessor.Local

#nullable enable

namespace Mandelbrot.Controls
{
    public partial class ControlForm
    {
        sealed class SelectionViewModel
        {
            [DisplayName("Lower left corner")]
            [Category("Mouse selection")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            [ReadOnly(true)]
            public ComplexViewModel? LowerLeft { get; set;  }
            [DisplayName("Upper right corner")]
            [Category("Mouse selection")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            [ReadOnly(true)]
            public ComplexViewModel? UpperRight { get; set; }
            [DisplayName("Pixels")]
            [Category("Mouse selection")]
            [ReadOnly(true)]
            public Rectangle Pixels { get; set; }
        }
    }
}