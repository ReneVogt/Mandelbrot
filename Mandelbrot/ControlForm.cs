using System;
using System.Globalization;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot
{
    public partial class ControlForm : Form
    {
        public event EventHandler? RefreshClicked;
        public event EventHandler? AdjustImaginaryAxisClicked;
        public event EventHandler? AdjustRealAxisClicked;
        public event EventHandler? ReturnToTotalViewClicked;

        public int MaximumNumberOfIterations
        {
            get => (int)nupIterations.Value;
            set
            {
                try
                {
                    nupIterations.Value = value;
                }
                catch(ArgumentOutOfRangeException){}
            }
        }

        public ControlForm()
        {
            Icon = Resources.Mandelbrot;
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }
        public void SetCurrentScope(MandelbrotArea area)
        {
            lbCurrentReal.Text = CreateAxisString(area.RealMin, area.RealMax);
            lbCurrentImaginary.Text = CreateAxisString(area.ImaginaryMin, area.ImaginaryMax);
        }
        public void SetCurrentSelection(MandelbrotArea area)
        {
            lbSelectionReal.Text = CreateAxisString(area.RealMin, area.RealMax);
            lbSelectionImaginary.Text = CreateAxisString(area.ImaginaryMin, area.ImaginaryMax);
        }
        static string CreateAxisString(double min, double max) => min.Equals(max) ? FormatDouble(min) : $"{FormatDouble(min)} to {FormatDouble(max)}";
        static string FormatDouble(double d) =>
            d.ToString("G17").TrimEnd('0').TrimEnd(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray());

        void btRefresh_Click(object sender, EventArgs e)
        {
            RefreshClicked?.Invoke(this, e);
        }
        private void btAdjustImaginary_Click(object sender, EventArgs e)
        {
            AdjustImaginaryAxisClicked?.Invoke(this, e);
        }
        private void btAdjustReal_Click(object sender, EventArgs e)
        {
            AdjustRealAxisClicked?.Invoke(this, e);
        }
        private void btStartScreen_Click(object sender, EventArgs e)
        {
            ReturnToTotalViewClicked?.Invoke(this, e);
        }
    }
}
