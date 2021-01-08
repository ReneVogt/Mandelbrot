using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

// ReSharper disable MemberCanBePrivate.Local

#nullable enable

namespace Mandelbrot.Controls
{
    public partial class ControlForm
    {
        sealed class ColorizersTypeConverter : EnumConverter
        {
            const string BlackAndWhite = "Black & White";
            const string IterationRatio = "Based on iteration ratio";
            const string IterationModulo = "Based on iteration modulo";

            public ColorizersTypeConverter(Type type)
                : base(type)
            {
                if (type != typeof(Colorizers))
                    throw new ArgumentException($"The {nameof(ColorizersTypeConverter)} is only for use with {nameof(Colorizers)} enum type!");
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(string);
            public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => value switch
            {
                BlackAndWhite => Colorizers.BlackAndWhite,
                IterationRatio => Colorizers.IterationRatio,
                IterationModulo => Colorizers.IterationModulo,
                _ => base.ConvertFrom(context, culture, value)

            };
            public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => value switch
            {
                Colorizers.BlackAndWhite => BlackAndWhite,
                Colorizers.IterationModulo => IterationModulo,
                Colorizers.IterationRatio => IterationRatio,
                _ => base.ConvertTo(context, culture, value, destinationType)
            };
        }
        enum Colorizers
        {
            BlackAndWhite,
            IterationRatio,
            IterationModulo
        }
        sealed class CalculationSettingsViewModel
        {
            [DisplayName("Maximum iteration count")]
            [Category("Calculation settings")]
            [DefaultValue(500)]
            public int MaximumNumberOfIterations { get; set; }
            [DisplayName("Colorizer")]
            [Category("Calculation settings")]
            [DefaultValue(Colorizers.BlackAndWhite)]
            [TypeConverter(typeof(ColorizersTypeConverter))]
            public Colorizers Colorizer { get; set; }
            [DisplayName("Resolution")]
            [Category("Calculation settings")]
            public Size Resolution { get; private set; }

            public void SetResolution(Size r) => Resolution = r;
        }
    }
}