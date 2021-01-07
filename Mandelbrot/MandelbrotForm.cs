﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;

#nullable enable
#pragma warning disable IDE1006 // Benennungsstile

namespace Mandelbrot
{
    public partial class MandelbrotForm : FullscreenableForm
    {
        readonly ControlForm controlForm = new ControlForm();
        readonly Stack<ComplexScope> rewindStack = new Stack<ComplexScope>(), forwardStack = new Stack<ComplexScope>();
        readonly Font progressFont = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold);
        readonly Pen progressWheelPen = new Pen(Brushes.Green, 2);
        readonly Brush progressBackBrush = new SolidBrush(Color.FromArgb(96, Color.Black));
        readonly Pen calculationRectanglePen = new Pen(new SolidBrush(Color.LightGreen), 3);

        int progressToDraw = -1;
        bool resizing;
        MandelbrotBitmapGenerator? currentGenerator;
        ComplexScope currentScope;
        Rectangle? mouseSelection, calculatingRect;
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

            currentScope = AdjustScope(ComplexScope.Mandelbrot);

            controlForm.RecalculateClicked += (sender, e) => _ = RunCalculationAsync(currentScope, UpdateStackButtons);
            controlForm.CancelClicked += (sender, e) => CancelCalculation();
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
            Text += $" v{Application.ProductVersion}";
            controlForm.Location = PointToScreen(new Point((Width - controlForm.Width) / 2, (Height - controlForm.Height) / 2));
            controlForm.Show(this);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            CancelCalculation();
            base.OnClosing(e);
        }
        protected override void OnResizeBegin(EventArgs e)
        {
            resizing = true;
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            resizing = false;
            base.OnResizeEnd(e);
            _ = RunCalculationAsync(AdjustScope(currentScope), UpdateStackButtons);
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
                case Keys.Escape:
                    CancelCalculation();
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
            if (p == progressToDraw) return;
            controlForm.Progress = progressToDraw = p;
            InvalidateView();
        }
        #endregion
        #region PictureBox event handlers
        private void pbView_SizeChanged(object sender, EventArgs e)
        {
            CancelCalculation();
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
                InvalidateView();
            }

            if (mouseSelection?.Height > 0 && mouseSelection?.Width > 0)
                controlForm.SetCurrentSelection(GetScopeFromRect(mouseSelection.Value));
            else
                controlForm.SetCurrentSelection(GetComplexFromPoint(e.Location));
        }
        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseSelection.HasValue && mouseSelection.Value.Width > 0 && mouseSelection.Value.Height > 0)
            {
                _ = RunCalculationAsync(AdjustScope(GetScopeFromRect(mouseSelection.Value)), InsertToStack);
            }

            mouseSelection = null;
            mouseStartingPoint = null;
            InvalidateView();
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
            if (e.Button == MouseButtons.Left)
                ReturnToTotalView();
        }
        private void pbView_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentImage == null && currentGenerator?.IsCancelled != false && !resizing)
                _ = RunCalculationAsync(AdjustScope(currentScope), UpdateStackButtons);

            if (mouseSelection != null)
                e.Graphics.DrawRectangle(Pens.White, mouseSelection.Value);

            if (calculatingRect != null) 
                e.Graphics.DrawRectangle(calculationRectanglePen, calculatingRect.Value);

            if (progressToDraw > -1)
                DrawProgress(e.Graphics);
        }
        void DrawProgress(Graphics graphics)
        {
            var text = $"{progressToDraw}%";
            var textSize = graphics.MeasureString(text, progressFont);
            var textRect = new RectangleF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2, textSize.Width, textSize.Height);
            var s = Math.Max(textSize.Width, textSize.Height) + 50;
            var backRect = new Rectangle((int)(textRect.Left + textRect.Width / 2 - s / 2), (int)(textRect.Top + textRect.Height / 2 - s / 2), (int)s,
                                         (int)s);
            graphics.DrawEllipse(progressWheelPen, backRect);
            graphics.FillPie(progressBackBrush, backRect, 3.6f * progressToDraw - 90, 3.6f * (100 - progressToDraw));
            graphics.FillPie(Brushes.Green, backRect, -90, 3.6f * progressToDraw);
            graphics.DrawString(text, progressFont, Brushes.White, textRect);
        }
        void InvalidateView() => pbView.Invalidate();
        #endregion
        #region Calculation
        async Task RunCalculationAsync(ComplexScope scope, Action stackAction)
        {
            CancelCalculation();

            var pixels = Pixels;
            MandelbrotBitmapGenerator? generator = null;
            try
            {

                calculatingRect = GetRectFromScope(scope);
                generator = currentGenerator =
                                new MandelbrotBitmapGenerator(controlForm.Colorizer, pixels, scope, controlForm.MaximumNumberOfIterations);
                Cursor = Cursors.AppStarting;
                var bmp = await generator.CreateBitmapParallel();
                if (generator.IsCancelled) return;

                stackAction();
                ResetStatesAfterCalculation(bmp, scope);
            }
            catch (OperationCanceledException) { }
            catch (ArgumentException ae)
            {
                if (generator?.IsCancelled == true) return;
                OnCalculationError(ae);
            }
        }
        void OnCalculationError(Exception error)
        {
            ResetStatesAfterCalculation();
            MessageBox.Show(this, error.ToString(), "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void CancelCalculation()
        {
            currentGenerator?.Cancel();
            currentGenerator = null;
            ResetStatesAfterCalculation();
        }
        void ResetStatesAfterCalculation(Bitmap? bitmap = null, ComplexScope? scope = null)
        {
            Cursor = Cursors.Default;
            currentGenerator = null;
            mouseStartingPoint = null;
            mouseSelection = calculatingRect = null;

            if (bitmap is {}) CurrentImage = bitmap;
            if (scope is {})
            {
                currentScope = scope;
                controlForm.SetCurrentScope(scope);
                controlForm.SetCurrentSelection(scope);
            }
        }
        #endregion
        #region Coordinate transformation
        ComplexScope AdjustScope(ComplexScope scope)
        {
            if (!controlForm.AdjustAxes) return scope;

            var h = Pixels.Height;
            var w = Pixels.Width;
            var dx = scope.Real / w;
            var dy = scope.Imaginary / h;

            if (dx > dy)
            {
                double d = 0.5 * dx * h;
                double m = (scope.UpperRight.Imaginary + scope.LowerLeft.Imaginary) / 2;
                return ((scope.LowerLeft.Real, m - d), (scope.UpperRight.Real, m + d));
            }
            
            if (dy > dx)
            {
                double d = 0.5 * dy * w;
                double m = (scope.UpperRight.Real + scope.LowerLeft.Real) / 2;
                return ((m - d, scope.LowerLeft.Imaginary), (m + d, scope.UpperRight.Imaginary));
            }

            return scope;
        }
        Rectangle GetRectFromScope(ComplexScope scope)
        {
            var dx = Pixels.Width / currentScope.Real;
            var dy = Pixels.Height / currentScope.Imaginary;
            var x = (int)((scope.LowerLeft.Real - currentScope.LowerLeft.Real) * dx);
            var y = (int)((currentScope.UpperRight.Imaginary - scope.UpperRight.Imaginary) * dy);
            var width = (int)(scope.Real * dx);
            var height = (int)(scope.Imaginary * dy);
            return new Rectangle(x, y, width, height);
        }
        ComplexScope GetScopeFromRect(Rectangle rect)
        {
            var upperLeft = GetComplexFromPoint(rect.Location);
            var lowerRight = GetComplexFromPoint(rect.Location + rect.Size);
            return ((upperLeft.Real, lowerRight.Imaginary), (lowerRight.Real, upperLeft.Imaginary));
        }
        Complex GetComplexFromPoint(Point p) => new Complex(currentScope.LowerLeft.Real + currentScope.Real * p.X / Pixels.Width,
                                                            currentScope.UpperRight.Imaginary - currentScope.Imaginary * p.Y / Pixels.Height);
        #endregion
        #region ControlForm handlers
        void ReturnToTotalView()
        {
            var scope = AdjustScope(ComplexScope.Mandelbrot);
            if (scope == currentScope) return;
            _ = RunCalculationAsync(scope, InsertToStack);
        }
        void AdjustAxes()
        {
            var scope = AdjustScope(currentScope);
            if (scope == currentScope) return;
            _ = RunCalculationAsync(scope, InsertToStack);
        }
        void GotoPreviousScope()
        {
            if (rewindStack.Count <= 0) return;
            var scope = rewindStack.Peek();
            _ = RunCalculationAsync(scope, OnPoppedPrevious);
        }
        void GotoNextScope()
        {
            if (forwardStack.Count <= 0) return;
            var scope = forwardStack.Peek();
            _ = RunCalculationAsync(scope, OnPoppedNext);
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
        #endregion
        #region Stack actions
        void InsertToStack()
        {
            if (rewindStack.Count == 0 || rewindStack.Peek() != currentScope)
                rewindStack.Push(currentScope);
            forwardStack.Clear();
            UpdateStackButtons();
        }
        void OnPoppedPrevious()
        {
            if (forwardStack.Count == 0 || forwardStack.Peek() != currentScope)
                forwardStack.Push(currentScope);
            rewindStack.Pop();
            UpdateStackButtons();
        }
        void OnPoppedNext()
        {
            if (rewindStack.Count == 0 || rewindStack.Peek() != currentScope)
                rewindStack.Push(currentScope);
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
