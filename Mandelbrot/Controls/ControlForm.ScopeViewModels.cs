﻿using System.ComponentModel;
using System.Drawing;
using System.Numerics;

// ReSharper disable UnusedAutoPropertyAccessor.Local

#nullable enable

namespace Mandelbrot.Controls
{
    public partial class ControlForm
    {
        sealed class ComplexViewModel
        {
            [NotifyParentProperty(true)]
            public double Real { get; set; }
            [NotifyParentProperty(true)]
            public double Imaginary { get; set; }
            public override string ToString() => new Complex(Real, Imaginary).ToString();
        }
        sealed class ScopeViewModel
        {
            [DisplayName("Lower left corner")]
            [Category("Complex rectangle")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public ComplexViewModel LowerLeft { get; } = new();
            [DisplayName("Upper right corner")]
            [Category("Complex rectangle")]
            [DefaultValue(typeof(ComplexViewModel), "(-2, -2")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public ComplexViewModel UpperRight { get; } = new();
            [DisplayName("Resolution")]
            [Category("Complex rectangle")]
            [ReadOnly(true)]
            public Size Resolution { get; set; }
        }
    }
}