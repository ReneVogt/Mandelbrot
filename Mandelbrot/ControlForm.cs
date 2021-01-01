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
        Adjustment adjustment = Adjustment.None;

        public event EventHandler? RefreshClicked;
        public event EventHandler? AdjustmentChanged;
        public event EventHandler? ReturnToTotalViewClicked;
        public event EventHandler? PreviousClicked;
        public event EventHandler? NextClicked;
        public event EventHandler? FullscreenChanged;

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
        public Adjustment Adjustment
        {
            get => rbAdjustToReal.Checked ? Adjustment.Real : rbAdjustToImaginary.Checked ? Adjustment.Imaginary : Adjustment.None;
            set
            {
                var rb = value switch
                {
                    Adjustment.Imaginary => rbAdjustToImaginary,
                    Adjustment.Real => rbAdjustToReal,
                    _ => rbAdjustToNone
                };
                rb.Checked = true;
            }
        }
        public bool Fullscreen
        {
            get => cbFullscreen.Checked;
            set => cbFullscreen.Checked = value;
        }

        public ControlForm()
        {
            Icon = Resources.Mandelbrot;
            InitializeComponent();
            rbAdjustToNone.Tag = Adjustment.None;
            rbAdjustToReal.Tag = Adjustment.Real;
            rbAdjustToImaginary.Tag = Adjustment.Imaginary;
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
        private void btStartScreen_Click(object sender, EventArgs e)
        {
            ReturnToTotalViewClicked?.Invoke(this, e);
        }
        private void cbFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            FullscreenChanged?.Invoke(this, e);
        }

        private void OnAdjustmentChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb)) return;
            if (!rb.Checked) return;
            var old = adjustment;
            adjustment = (Adjustment)rb.Tag;
            if (old == adjustment || old != Adjustment.None) return;
            AdjustmentChanged?.Invoke(this, e);
        }
    }
}
