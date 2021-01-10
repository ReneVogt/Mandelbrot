using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable enable

namespace Mandelbrot.Controls
{
    public sealed class DropDownButton : Button
    {
        /// <summary>
        /// The drop down menu to show when the button
        /// is clicked.
        /// </summary>
        [Category("Behaviour")]
        [DefaultValue(null)]
        [Description("The drop down menu to show when the button is clicked.")]
        public ContextMenuStrip? DropDownMenu { get; set; }

        /// <inheritdoc />
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            DropDownMenu?.Show(this, 0, Height);
        }
    }
}
