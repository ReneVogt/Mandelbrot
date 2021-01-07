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
        #region Fields
        readonly CalculationSettingsViewModel calculationSettings;
        readonly ScopeViewModel currentScopeViewModel = new ScopeViewModel();

        ComplexScope currentScope = ComplexScope.Mandelbrot;
        #endregion
        #region Events
        public event EventHandler? RecalculationRequested;
        public event EventHandler? CancelClicked;
        public event EventHandler? AdjustClicked;
        public event EventHandler? TotalClicked;
        public event EventHandler? PreviousClicked;
        public event EventHandler? NextClicked;
        public event EventHandler? FullscreenChanged;
        public event EventHandler? SaveClicked;
        #endregion
        #region Properties
        public int MaximumNumberOfIterations { get; private set; }
        public MandelbrotColorizer Colorizer { get; private set; }
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
        #endregion
        #region Construction
        public ControlForm()
        {
            InitializeComponent();

            MaximumNumberOfIterations = Settings.Default.MaximumNumberOfIterations;
            Colorizer = GetColorizer((Colorizers)Settings.Default.Colorizer);
            calculationSettings = new CalculationSettingsViewModel
            {
                MaximumNumberOfIterations = MaximumNumberOfIterations,
                Colorizer = (Colorizers)Settings.Default.Colorizer
            };
            pgCalculationSettings.SelectedObject = calculationSettings;

            pgCurrentScope.SelectedObject = currentScopeViewModel;
            cbAdjustAxes.Checked = Settings.Default.AdjustAxesd;
        }
        #endregion
        #region Form event handlers
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Settings.Default.AdjustAxesd = cbAdjustAxes.Checked;
            Settings.Default.Save();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }
        #endregion

        #region Calculation proress
        public void SetProgress(int progress, TimeSpan? elapsed = null)
        {
            if (progress >= 0)
            {
                btCancel.Enabled = true;
                Cursor = Cursors.AppStarting;
                pbProgress.Value = progress;
                lbProgress.Text = $"{progress}%";
            }
            else
            {
                btCancel.Enabled = false;
                Cursor = Cursors.Default;
                lbProgress.Text = string.Empty;
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
            Settings.Default.Colorizer = (int)calculationSettings.Colorizer;
            Colorizer = GetColorizer(calculationSettings.Colorizer);
            MaximumNumberOfIterations = Settings.Default.MaximumNumberOfIterations = calculationSettings.MaximumNumberOfIterations;
            Settings.Default.Save();
            btApplyCalculationSettings.Enabled = btResetCalculationSettings.Enabled = false;
            RecalculationRequested?.Invoke(this, e);
        }
        private void btResetCalculationSettings_Click(object sender, EventArgs e)
        {
            calculationSettings.Colorizer = (Colorizers)Settings.Default.Colorizer;
            calculationSettings.MaximumNumberOfIterations = Settings.Default.MaximumNumberOfIterations;
            btApplyCalculationSettings.Enabled = btResetCalculationSettings.Enabled = false;
            pgCalculationSettings.Refresh();
        }
        private void pgCalculationSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            btResetCalculationSettings.Enabled = btApplyCalculationSettings.Enabled =
                                                     Settings.Default.MaximumNumberOfIterations != calculationSettings.MaximumNumberOfIterations ||
                                                     Settings.Default.Colorizer != (int)calculationSettings.Colorizer;
        }
        static MandelbrotColorizer GetColorizer(Colorizers colorizer) => colorizer switch
        {
            Colorizers.IterationRatio => MandelbrotColorizer.IterationRatioColorizer,
            Colorizers.IterationModulo => MandelbrotColorizer.IterationModuloColorizer,
            _ => MandelbrotColorizer.BlackAndWhite
        };
        #endregion
        #region Current scope
        private void pgCurrentScope_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            btApplyScope.Enabled = btResetScope.Enabled = !(
                                                               currentScopeViewModel.LowerLeft.Real.Equals(currentScope.LowerLeft.Real) &&
                                                               currentScopeViewModel.LowerLeft.Imaginary.Equals(currentScope.LowerLeft.Imaginary) &&
                                                               currentScopeViewModel.UpperRight.Real.Equals(currentScope.UpperRight.Real) &&
                                                               currentScopeViewModel.UpperRight.Imaginary.Equals(currentScope.UpperRight.Imaginary));
        }
        private void btApplyScope_Click(object sender, EventArgs e)
        {
            ResetCurrentScope();
        }
        private void btResetScope_Click(object sender, EventArgs e) => ResetCurrentScope();
        public void SetCurrentScope(ComplexScope scope)
        {
            currentScope = new ComplexScope(scope.LowerLeft, scope.UpperRight);
            if (!btApplyScope.Enabled)
                ResetCurrentScope();
        }
        void ResetCurrentScope()
        {
            currentScopeViewModel.LowerLeft.Real = currentScope.LowerLeft.Real;
            currentScopeViewModel.LowerLeft.Imaginary = currentScope.LowerLeft.Imaginary;
            currentScopeViewModel.UpperRight.Real = currentScope.UpperRight.Real;
            currentScopeViewModel.UpperRight.Imaginary = currentScope.UpperRight.Imaginary;
            pgCurrentScope.Refresh();
            btApplyScope.Enabled = btResetScope.Enabled = false;
        }
        #endregion


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

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
