using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;
using MandelbrotGenerator.Exceptions;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot
{
    public partial class MandelbrotForm : FullscreenableForm
    {
        readonly ControlForm controlForm = new ControlForm();
        readonly MandelbrotColorizer colorizer = new Colorizer();
        readonly Stack<MandelbrotArea> rewindStack = new Stack<MandelbrotArea>(), forwardStack = new Stack<MandelbrotArea>();
        readonly Font progressFont = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold);
        readonly Pen progressWheelPen = new Pen(Brushes.Green, 2);

        int progress = -1;
        MandelbrotImageGenerator? currentGenerator;
        MandelbrotArea currentArea;
        CancellationTokenSource? cancellationTokenSource;
        Rectangle? mouseSelection;
        Point? mouseStartingPoint;

        Size Pixels => pbView.Size;
        Image? CurrentImage
        {
            get => pbView.BackgroundImage;
            set
            {
                var old = CurrentImage;
                pbView.BackgroundImage = value;
                old?.Dispose();
            }
        }

        public MandelbrotForm()
        {
            Icon = Resources.Mandelbrot;
            InitializeComponent();

            typeof(Panel).InvokeMember(nameof(DoubleBuffered), BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null,
                                       pbView, new object[] {true});

            currentArea = AdjustArea(MandelbrotArea.Default);

            controlForm.RecalculateClicked += (sender, e) => _ = RunCalculationAsync(currentArea, UpdateStackButtons);
            controlForm.CancelClicked += (sender, e) => 
            {
                cancellationTokenSource?.Cancel();
                cancellationTokenSource = null;
                currentGenerator = null;
                Cursor = Cursors.Default;
            };
            controlForm.PreviousClicked += (sender, e) => GotoPreviousScope();
            controlForm.NextClicked += (sender, e) => GotoNextScope();
            controlForm.TotalClicked += (sender, e) => ReturnToTotalView();
            controlForm.AdjustClicked += (sender, e) => AdjustAxes();
            controlForm.SaveClicked += (sender, e) => SaveImage();
            controlForm.FullscreenChanged += (sender, e) => Fullscreen = controlForm.Fullscreen;
        }
        #region Form event handlers
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            controlForm.Location = PointToScreen(new Point((Width - controlForm.Width) / 2, (Height - controlForm.Height) / 2));
            controlForm.Show(this);
        }
        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            SuspendLayout();
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            ResumeLayout();
        }
        protected override void OnFullscreenChanged(EventArgs e)
        {
            base.OnFullscreenChanged(e);
            controlForm.Fullscreen = Fullscreen;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Escape: ReturnToTotalView();
                    break;
                case Keys.Z: if (e.Control) GotoPreviousScope();
                    break;
                case Keys.Y:
                    if (e.Control) GotoNextScope();
                    break;
            }
        }
        private void OnProgressTimer(object sender, EventArgs e)
        {
            var p = currentGenerator?.Progress ?? -1;
            if (p == progress) return;
            controlForm.Progress = progress = p;
            pbView.Invalidate();
        }
        #endregion
        #region PictureBox event handlers
        private void pbView_SizeChanged(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            CurrentImage = null;
        }
        private void pbView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                mouseStartingPoint = null;
                mouseSelection = null;
            }
            else
            {
                mouseStartingPoint ??= e.Location;
                Point topLeft = new Point(Math.Min(mouseStartingPoint.Value.X, e.Location.X), Math.Min(e.Location.Y, mouseStartingPoint.Value.Y));
                Size width = new Size(Math.Abs(e.Location.X - mouseStartingPoint.Value.X), Math.Abs(e.Location.Y - mouseStartingPoint.Value.Y));
                mouseSelection = new Rectangle(topLeft, width);
                pbView.Invalidate();
            }

            if (mouseSelection != null)
                controlForm.SetCurrentSelection(GetMandelbrotAreaFromRect(mouseSelection.Value));
            else
            {
                var (r, i) = GetComplexFromPoint(e.Location);
                controlForm.SetCurrentSelection((r, r, i, i));
            }
        }
        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseSelection.HasValue)
                _ = RunCalculationAsync(GetMandelbrotAreaFromRect(mouseSelection.Value), InsertToStack);
            mouseSelection = null;
            mouseStartingPoint = null;
            Invalidate();
        }
        private void pbView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                controlForm.Location = PointToScreen(e.Location);
                if (!controlForm.Visible)
                {
                    controlForm.Show(this);
                    controlForm.BringToFront();
                }
            }
        }
        private void pbView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReturnToTotalView();
        }
        private void pbView_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentImage == null && cancellationTokenSource?.IsCancellationRequested != false)
                _ = RunCalculationAsync(currentArea, UpdateStackButtons);

            if (mouseSelection != null)
                e.Graphics.DrawRectangle(Pens.White, mouseSelection.Value);

            if (progress > -1)
                DrawProgress(e.Graphics);
        }
        #endregion
        #region Calculation
        async Task RunCalculationAsync(MandelbrotArea area, Action stackAction)
        {
            cancellationTokenSource?.Cancel();
            var cts = cancellationTokenSource = new CancellationTokenSource();
            var generator = currentGenerator = new MandelbrotImageGenerator(colorizer)
                                {MaximumNumberOfIterations = controlForm.MaximumNumberOfIterations};
            var pixels = Pixels;
            try
            {
                Cursor = Cursors.AppStarting;
                var bmp = await Task.Run(() => generator.CreateBitmap(pixels.Width, pixels.Height, area, cts.Token), cts.Token);
                if (cts.IsCancellationRequested) return;

                stackAction();
                currentArea = area;
                Cursor = Cursors.Default;
                cancellationTokenSource = null;
                currentGenerator = null;
                mouseSelection = null;
                mouseStartingPoint = null;
                controlForm.SetCurrentScope(area);
                controlForm.SetCurrentSelection(area);
                CurrentImage = bmp;
            }
            catch (OperationCanceledException) { }
            catch (MandelbrotException mbe)
            {
                if (cts.IsCancellationRequested) return;
                OnCalculationError(mbe);
            }
            catch (ArgumentException ae)
            {
                if (cts.IsCancellationRequested) return;
                OnCalculationError(ae);
            }
        }
        void OnCalculationError(Exception error)
        {
            Cursor = Cursors.Default;
            cancellationTokenSource = null;
            currentGenerator = null;
            MessageBox.Show(this, error.ToString(), "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region Helper methods
        void DrawProgress(Graphics graphics)
        {
            var text = $"{progress}%";
            var textSize = graphics.MeasureString(text, progressFont);
            var textRect = new RectangleF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2, textSize.Width, textSize.Height);
            var s = Math.Max(textSize.Width, textSize.Height) + 50;
            var backRect = new Rectangle((int)(textRect.Left + textRect.Width / 2 - s / 2), (int)(textRect.Top + textRect.Height / 2 - s / 2), (int)s,
                                         (int)s);
            graphics.DrawEllipse(progressWheelPen, backRect);
            graphics.FillPie(Brushes.Black, backRect, 3.6f * progress - 90, 3.6f * (100 - progress));
            graphics.FillPie(Brushes.Green, backRect, -90, 3.6f * progress);
            graphics.DrawString(text, progressFont, Brushes.White, textRect);
        }
        MandelbrotArea AdjustArea(MandelbrotArea area)
        {
            var (realMin, realMax, imaginaryMin, imaginaryMax) = area;
            var h = Pixels.Height;
            var w = Pixels.Width;
            var dx = (realMax - realMin) / w;
            var dy = (imaginaryMax - imaginaryMin) / h;
            if (dx > dy)
            {
                double d = 0.5 * dx * h;
                double m = (imaginaryMax + imaginaryMin) / 2;
                return (realMin, realMax, m - d, m + d);
            }
            
            if (dy > dx)
            {
                double d = 0.5 * dy * w;
                double m = (realMax + realMin) / 2;
                return (m - d, m + d, imaginaryMin, imaginaryMax);
            }

            return area;
        }
        MandelbrotArea GetMandelbrotAreaFromRect(Rectangle rect)
        {
            (double rmin, double imax) = GetComplexFromPoint(rect.Location);
            (double rmax, double imin) = GetComplexFromPoint(rect.Location + rect.Size);
            return (rmin, rmax, imin, imax);
        }
        (double r, double i) GetComplexFromPoint(Point p) => (currentArea.RealMin + (currentArea.RealMax - currentArea.RealMin) * p.X / Pixels.Width,
                                                                 currentArea.ImaginaryMax - (currentArea.ImaginaryMax - currentArea.ImaginaryMin) * p.Y / Pixels.Height);

        void ReturnToTotalView()
        {
            var area = AdjustArea(MandelbrotArea.Default);
            if (area == currentArea) return;
            _ = RunCalculationAsync(area, InsertToStack);
        }
        void AdjustAxes()
        {
            var area = AdjustArea(currentArea);
            if (area == currentArea) return;
            _ = RunCalculationAsync(area, InsertToStack);
        }
        void GotoPreviousScope()
        {
            if (rewindStack.Count <= 0) return;
            var area = rewindStack.Peek();
            _ = RunCalculationAsync(area, OnPoppedPrevious);
        }
        void GotoNextScope()
        {
            if (forwardStack.Count <= 0) return;
            var area = forwardStack.Peek();
            _ = RunCalculationAsync(area, OnPoppedNext);
        }
        void SaveImage()
        {
            var bmp = CurrentImage;
            if (bmp == null) return;

            using var imageFormatDialog = new DlgSelectImageFormat();
            if (imageFormatDialog.ShowDialog(this) != DialogResult.OK) return;
            var imageFormatName = imageFormatDialog.ImageFormatName;
            var imageFormat = imageFormatDialog.ImageFormat;

            using var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = "bmp",
                Filter = $"{imageFormatName} files (*.{imageFormatName})|*.{imageFormatName}|All files (*.*)|*",
                FilterIndex = 1,
                OverwritePrompt = true,
                RestoreDirectory = true,
                Title = "Save Mandelbrot image"
            };
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                bmp.Save(saveFileDialog.FileName, imageFormat);
                MessageBox.Show(this, $"Successfully saved image as {imageFormatName} in {saveFileDialog.FileName}.", "Mandelbrot image",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, $"Failed to save image: {e}", "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void InsertToStack()
        {
            if (rewindStack.Count == 0 || rewindStack.Peek() != currentArea)
                rewindStack.Push(currentArea);
            forwardStack.Clear();
            UpdateStackButtons();
        }
        void OnPoppedPrevious()
        {
            if (forwardStack.Count == 0 || forwardStack.Peek() != currentArea)
                forwardStack.Push(currentArea);
            rewindStack.Pop();
            UpdateStackButtons();
        }
        void OnPoppedNext()
        {
            if (rewindStack.Count == 0 || rewindStack.Peek() != currentArea)
                rewindStack.Push(currentArea);
            forwardStack.Pop();
            UpdateStackButtons();
        }
        void UpdateStackButtons()
        {
            controlForm.CanGotoNext = forwardStack.Count > 0;
            controlForm.CanGotoPrevious = rewindStack.Count > 0;
        }
        #endregion
    }
}
