using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;
using MandelbrotGenerator.Colorizer;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot.Controls
{
    public partial class ControlForm : Form
    {
        #region Constants
        readonly Dictionary<string, ImageFormat> imageFormats = new()
        {
            ["Bmp"] = ImageFormat.Bmp,
            ["Emf"] = ImageFormat.Emf,
            ["Exif"] = ImageFormat.Exif,
            ["Gif"] = ImageFormat.Gif,
            ["Icon"] = ImageFormat.Icon,
            ["Jpeg"] = ImageFormat.Jpeg,
            ["Png"] = ImageFormat.Png,
            ["Tiff"] = ImageFormat.Tiff,
            ["Wmf"] = ImageFormat.Wmf
        };
        #endregion
        #region Fields
        readonly CalculationSettingsViewModel calculationSettings;
        readonly ScopeViewModel currentScopeViewModel = new();
        readonly HashSet<string> expandedSelectionProperties = new();
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
        public event EventHandler<SaveImageClickedEventArgs>? SaveImageClicked;
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
        public Size CurrentResolution
        {
            get => calculationSettings.Resolution;
            set
            {
                calculationSettings.Resolution = value;
                pgCalculationSettings.Refresh();
            }
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
                ParentForm?.BringToFront();
                ParentForm?.Focus();
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
        private void btPrevioius_Click(object sender, EventArgs e)
        {
            PreviousClicked?.Invoke(this, e);
        }
        private void btNext_Click(object sender, EventArgs e)
        {
            NextClicked?.Invoke(this, e);
        }
        private void btStartScreen_Click(object sender, EventArgs e)
        {
            TotalClicked?.Invoke(this, e);
        }
        private void cbFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            FullscreenChanged?.Invoke(this, e);
        }
        private void cbAdjustAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdjustAxes.Checked)
                AdjustClicked?.Invoke(this, e);
        }
        private void OnImageFormatClicked(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem { Text: var formatName } && imageFormats.TryGetValue(formatName, out var format))
                SaveImageClicked?.Invoke(this, new SaveImageClickedEventArgs(formatName, format));

        }
        public void SetCurrentScope(ComplexScope scope, Size resolution)
        {
            currentScope = new ComplexScope(scope.LowerLeft, scope.UpperRight);
            currentScopeViewModel.Resolution = resolution;
            pgCurrentScope.Refresh();
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
        #region Favorites
        private void cmbFavorites_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void menuItemAddToFavorites_Click(object sender, EventArgs e)
        {
        }
        private void menuItemManageFavorites_Click(object sender, EventArgs e)
        {
        }
        #endregion
        #endregion
        #region Current selection
        public void SetCurrentSelection(ComplexScope scope, Rectangle mouseSelection)
        {
            var model = new SelectionViewModel
            {
                Pixels = mouseSelection,
                UpperRight = new ComplexViewModel
                {
                    Imaginary = scope.UpperRight.Imaginary,
                    Real = scope.UpperRight.Real
                },
                LowerLeft = new ComplexViewModel
                {
                    Imaginary = scope.LowerLeft.Imaginary, 
                    Real = scope.LowerLeft.Real
                }
            };

            SaveExpandedSelectionProperties();
            pgCurrentSelction.SelectedObject = model;
            ExpandSelectionProperties();
        }
        public void SetCurrentSelection(Complex point, Point mouseLocation)
        {
            var complex = new ComplexViewModel {Real = point.Real, Imaginary = point.Imaginary};
            var model = new SelectionViewModel
            {
                Pixels = new Rectangle(mouseLocation, Size.Empty),
                UpperRight = complex,
                LowerLeft = complex
            };

            SaveExpandedSelectionProperties();
            pgCurrentSelction.SelectedObject = model;
            ExpandSelectionProperties();
        }
        void SaveExpandedSelectionProperties()
        {
            var root = pgCurrentSelction.SelectedGridItem;
            if (root is null) return;
            while (!(root.Parent is null)) root = root.Parent;

            expandedSelectionProperties.Clear();
            foreach (var item in root.GridItems.Cast<GridItem>().Where(item => item.Expanded))
                expandedSelectionProperties.Add(item.Label);
        }
        void ExpandSelectionProperties()
        {
            var root = pgCurrentSelction.SelectedGridItem;
            if (root is null) return;
            while (root.Parent is { }) root = root.Parent;

            foreach (var item in root.GridItems.Cast<GridItem>().Where(item => item.Expandable && expandedSelectionProperties.Contains(item.Label)))
                item.Expanded = true;
        }
        #endregion

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
