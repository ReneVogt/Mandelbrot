using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mandelbrot.Properties;
using MandelbrotGenerator;
using MandelbrotGenerator.Exceptions;

#nullable enable

namespace Mandelbrot
{
    public partial class MandelbrotControl : UserControl
    {
        readonly Cursor waitCursor = new Cursor(Resources.WaitCursor.GetHicon());
        CancellationTokenSource? cancellationTokenSource;
        MandelbrotArea? nextCalculation;
        MandelbrotArea currentArea = MandelbrotArea.Default;
        Point? mouseStartingPoint;
        Rectangle? mouseSelection;

        public ControlForm ControlForm { get; } = new ControlForm();

        public MandelbrotControl()
        {
            InitializeComponent();

            ControlForm.SetCurrentScope(currentArea);
            ControlForm.SetCurrentSelection(currentArea);
            ControlForm.MaximumNumberOfIterations = 100;
            ControlForm.RefreshClicked += (sender, e) => Recalculate();
            ControlForm.AdjustImaginaryAxisClicked += (sender, e) => AdjustToImaginaryAxis();
            ControlForm.AdjustRealAxisClicked += (sender, e) => AdjustToRealAxis();
            ControlForm.ReturnToTotalViewClicked += (sender, e) => ReturnToTotalView();
            ControlForm.FullscreenChanged += (sender, e) =>
            {
                if (ParentForm is FullscreenableForm fsf)
                    fsf.Fullscreen = ControlForm.Fullscreen;
            };
        }

        void Recalculate()
        {
            if (DesignMode)
                return;
            StartCalculation(currentArea);
        }
        void StartCalculation(MandelbrotArea area)
        {
            if (InvokeRequired)
                throw new InvalidOperationException($"Illegal cross thread call in {nameof(MandelbrotControl)} '{Name}'!");

            if (cancellationTokenSource is {})
            {
                nextCalculation = area;
                cancellationTokenSource.Cancel();
                return;
            }

            nextCalculation = null;
            Cursor = waitCursor;
            var cts = new CancellationTokenSource();
            _ = Task.Run(() => CalculateAsync(new MandelbrotImageGenerator {MaximumNumberOfIterations = ControlForm.MaximumNumberOfIterations}, Width, Height, area, cts.Token), cts.Token);
        }
        void CalculateAsync(MandelbrotImageGenerator generator, int width, int height, MandelbrotArea area, CancellationToken cancellationToken)
        {
            try
            {
                var bmp = generator.CreateBitmap(width, height, area, cancellationToken);
                OnCalculationFinished(bmp, area);
            }
            catch (OperationCanceledException)
            {
                OnCalculationAborted();
            }
            catch (MandelbrotException mbe)
            {
                OnCalculationError(mbe);
            }
            catch (ArgumentException ae)
            {
                OnCalculationError(ae);
            }
        }
        void OnCalculationFinished(Bitmap bitmap, MandelbrotArea area)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<Bitmap,MandelbrotArea>)OnCalculationFinished, bitmap, area);
                return;
            }

            if (cancellationTokenSource?.IsCancellationRequested == true)
            {
                OnCalculationAborted();
                return;
            }

            Cursor = Cursors.Default;
            cancellationTokenSource = null;
            currentArea = area;
            ControlForm.SetCurrentScope(area);
            ControlForm.SetCurrentSelection(area);
            SwapImages(bitmap);
        }
        void OnCalculationError(Exception error)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<Exception>)OnCalculationError, error);
                return;
            }
            if (cancellationTokenSource?.IsCancellationRequested == true)
            {
                OnCalculationAborted();
                return;
            }
            Cursor = Cursors.Default;
            cancellationTokenSource = null;
            MessageBox.Show(this, error.ToString(), "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void OnCalculationAborted()
        {
            Cursor = Cursors.Default;
            cancellationTokenSource = null;
            if (nextCalculation is {}) 
                StartCalculation(nextCalculation.Value);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            SwapImages(null);
            base.OnClientSizeChanged(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mouseSelection != null)
                e.Graphics.DrawRectangle(Pens.White, mouseSelection.Value);
            if (BackgroundImage is null && cancellationTokenSource?.IsCancellationRequested != false)
                Recalculate();
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            ReturnToTotalView();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && mouseSelection.HasValue)
                StartCalculation(GetMandelbrotAreaFromRect(mouseSelection.Value));
            mouseSelection = null;
            mouseStartingPoint = null;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button != MouseButtons.Left)
            {
                mouseSelection = null;
                mouseStartingPoint = null;
            }
            else
            {
                mouseStartingPoint ??= e.Location;

                Point topLeft = new Point(Math.Min(mouseStartingPoint.Value.X, e.Location.X), Math.Min(e.Location.Y, mouseStartingPoint.Value.Y));
                Size width = new Size(Math.Abs(e.Location.X - mouseStartingPoint.Value.X), Math.Abs(e.Location.Y - mouseStartingPoint.Value.Y));
                mouseSelection = new Rectangle(topLeft, width);
                Invalidate();
            }

            if (mouseSelection != null)
                ControlForm.SetCurrentSelection(GetMandelbrotAreaFromRect(mouseSelection.Value));
            else
            {
                var (r, i) = GetComplexFromPoint(e.Location);
                ControlForm.SetCurrentSelection((r, r, i, i));
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                if (!ControlForm.Visible)
                    ControlForm.Show(this);
                ControlForm.BringToFront();
                ControlForm.Location = PointToScreen(e.Location);
            }
        }
        void SwapImages(Bitmap? bitmap)
        {
            var old = BackgroundImage;
            BackgroundImage = bitmap;
            old?.Dispose();
        }
        void ReturnToTotalView()
        {
            StartCalculation(MandelbrotArea.Default);
        }
        void AdjustToImaginaryAxis()
        {
            var (realMin, realMax, imaginaryMin, imaginaryMax) = currentArea;
            double h = 0.5 * (realMax - realMin) * Height / Width;
            double m = (imaginaryMax + imaginaryMin) / 2;
            StartCalculation((realMin, realMax, m - h, m + h));
        }
        void AdjustToRealAxis()
        {
            var (realMin, realMax, imaginaryMin, imaginaryMax) = currentArea;
            double w = 0.5 * (imaginaryMax - imaginaryMin) * Width / Height;
            double m = (realMax + realMin) / 2;
            StartCalculation((m - w, m + w, imaginaryMin, imaginaryMax));
        }
        MandelbrotArea GetMandelbrotAreaFromRect(Rectangle rect)
        {
            (double rmin, double imax) = GetComplexFromPoint(rect.Location);
            (double rmax, double imin) = GetComplexFromPoint(rect.Location + rect.Size);
            return (rmin, rmax, imin, imax);
        }
        (double r, double i) GetComplexFromPoint(Point p) => (currentArea.RealMin + (currentArea.RealMax - currentArea.RealMin) * p.X / Width,
                                                                 currentArea.ImaginaryMax - (currentArea.ImaginaryMax - currentArea.ImaginaryMin) * p.Y / Height);
    }
}
