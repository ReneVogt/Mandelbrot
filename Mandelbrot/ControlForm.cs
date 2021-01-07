using System;
using System.Globalization;
using System.Numerics;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;
using MandelbrotGenerator.Colorizer;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot
{
    public partial class ControlForm : Form
    {
        readonly CalculationSettingsViewModel calculationSettings;
        bool calculationRunning;

        public event EventHandler? RecalculateClicked;
        public event EventHandler? CancelClicked;
        public event EventHandler? AdjustClicked;
        public event EventHandler? TotalClicked;
        public event EventHandler? PreviousClicked;
        public event EventHandler? NextClicked;
        public event EventHandler? FullscreenChanged;
        public event EventHandler? SaveClicked;

        public int MaximumNumberOfIterations => calculationSettings.MaximumNumberOfIterations;
        public MandelbrotColorizer Colorizer => calculationSettings.Colorizer switch
        {
            Colorizers.IterationModulo => MandelbrotColorizer.IterationModuloColorizer,
            Colorizers.IterationRatio => MandelbrotColorizer.IterationRatioColorizer,
            _ => MandelbrotColorizer.BlackAndWhite};
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
        public bool AdjustAxes
        {
            get => cbAdjustAxes.Checked;
            set => cbAdjustAxes.Checked = value;
        }
        public bool Fullscreen
        {
            get => cbFullscreen.Checked;
            set => cbFullscreen.Checked = value;
        }

        public ControlForm()
        {
            InitializeComponent();
            
            calculationSettings = new CalculationSettingsViewModel
            {
                MaximumNumberOfIterations = Settings.Default.MaximumNumberOfIterations,
                Colorizer = (Colorizers)Settings.Default.Colorizer
            };
            pgCalculationSettings.SelectedObject = calculationSettings;

            cmbColorizer.SelectedIndex = 0;
            cbAdjustAxes.Checked = Settings.Default.AdjustAxesd;
            try
            {
                cmbColorizer.SelectedIndex = Settings.Default.Colorizer;
            }
            catch(ArgumentException){}
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Settings.Default.MaximumNumberOfIterations = MaximumNumberOfIterations;
            Settings.Default.Colorizer = cmbColorizer.SelectedIndex;
            Settings.Default.AdjustAxesd = cbAdjustAxes.Checked;
            Settings.Default.Save();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }
        #region Calculation proress
        public void SetProgress(int progress, TimeSpan? elapsed = null)
        {
            btCancel.Enabled = calculationRunning = progress >= 0;
            if (calculationRunning)
            {
                Cursor = Cursors.AppStarting;
                pbProgress.Value = progress;
                lbProgress.Text = $"{progress}%";
            }
            else
            {
                Cursor = Cursors.Default;
                lbProgress.Text = String.Empty;
                pbProgress.Value = 0;
            }

            if (elapsed.HasValue)
                lbElapsed.Text = elapsed.Value.ToString();
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, e);
        }
        #endregion
        #region Calculation settings
        private void btApplyCalculationSettings_Click(object sender, EventArgs e)
        {
        }
        private void btResetCalculationSettings_Click(object sender, EventArgs e)
        {
        }
        #endregion
        public void SetCurrentScope(ComplexScope scope)
        {
            lbCurrentReal.Text = CreateAxisString(scope.LowerLeft.Real, scope.UpperRight.Real);
            lbCurrentImaginary.Text = CreateAxisString(scope.LowerLeft.Imaginary, scope.UpperRight.Imaginary);
        }
        public void SetCurrentSelection(ComplexScope scope)
        {
            lbSelectionReal.Text = CreateAxisString(scope.LowerLeft.Real, scope.UpperRight.Real);
            lbSelectionImaginary.Text = CreateAxisString(scope.LowerLeft.Imaginary, scope.UpperRight.Imaginary);
        }
        public void SetCurrentSelection(Complex point)
        {
            lbSelectionReal.Text = point.Real.ToString("G20");
            lbSelectionImaginary.Text = point.Imaginary.ToString("G20");
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
        private void cbAdjustAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdjustAxes.Checked)
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
