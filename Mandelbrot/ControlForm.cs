using System;
using System.Globalization;
using System.Windows.Forms;
using MandelbrotGenerator;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot
{
    public partial class ControlForm : Form
    {
        readonly MandelbrotColorizer[] colorizers = {MandelbrotColorizer.Default, new IterationRatioColorizer(), new RoundTripColorizer()};
        bool calculationRunning;

        public event EventHandler? RecalculateClicked;
        public event EventHandler? CancelClicked;
        public event EventHandler? AdjustClicked;
        public event EventHandler? TotalClicked;
        public event EventHandler? PreviousClicked;
        public event EventHandler? NextClicked;
        public event EventHandler? FullscreenChanged;
        public event EventHandler? SaveClicked;

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
        public MandelbrotColorizer Colorizer => colorizers[cmbColorizer.SelectedIndex];
        public bool CanGotoPrevious
        {
            get => btPrevioius.Enabled;
            set => btPrevioius.Enabled = value;
        }
        public bool CanGotoNext
        {
            get => btNext.Enabled;
            set => btNext.Enabled = value;
        }
        public bool Fullscreen
        {
            get => cbFullscreen.Checked;
            set => cbFullscreen.Checked = value;
        }
        public int Progress
        {
            get => calculationRunning ? pbProgress.Value : -1;
            set
            {
                calculationRunning = value >= 0;
                btRecalculate.Text = calculationRunning ? "Cancel" : "Recalculate";
                pbProgress.Value = calculationRunning ? value : 0;
            }
        }

        public ControlForm()
        {
            InitializeComponent();
            cmbColorizer.SelectedIndex = 0;
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


        void btRecalculate_Click(object sender, EventArgs e)
        {
            if (calculationRunning)
                CancelClicked?.Invoke(this, e);
            else
                RecalculateClicked?.Invoke(this, e);
        }

        private void btPrevioius_Click(object sender, EventArgs e)
        {
            PreviousClicked?.Invoke(this, e);
        }
        private void btNext_Click(object sender, EventArgs e)
        {
            NextClicked?.Invoke(this, e);
        }
        private void btAdjust_Click(object sender, EventArgs e)
        {
            AdjustClicked?.Invoke(this, e);
        }
        private void btStartScreen_Click(object sender, EventArgs e)
        {
            TotalClicked?.Invoke(this, e);
        }
        private void cbFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            FullscreenChanged?.Invoke(this, e);
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            SaveClicked?.Invoke(this, e);
        }
    }
}
