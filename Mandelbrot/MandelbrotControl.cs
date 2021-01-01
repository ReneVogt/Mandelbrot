using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MandelbrotGenerator;
using MandelbrotGenerator.Exceptions;

#nullable enable

namespace Mandelbrot
{
    public partial class MandelbrotControl : UserControl
    {
        readonly Font progressFont = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold);
        
        CancellationTokenSource? cancellationTokenSource;
        MandelbrotArea? nextCalculation;
        MandelbrotArea currentArea = MandelbrotArea.Default;
        Point? mouseStartingPoint;
        Rectangle? mouseSelection;
        MandelbrotImageGenerator? currentGenerator;
        int progress = -1;

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
                Debug.WriteLine("CANCELLING CALCULATION");
                nextCalculation = area;
                cancellationTokenSource.Cancel();
                return;
            }

            Debug.WriteLine("STARTING CALCULATION");
            nextCalculation = null;
            var generator = currentGenerator = new MandelbrotImageGenerator {MaximumNumberOfIterations = ControlForm.MaximumNumberOfIterations};
            var cts = cancellationTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => CalculateAsync(generator, Width, Height, area, cts.Token), cts.Token);
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
            Debug.WriteLine("CALCULATION FINISHED");
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

            cancellationTokenSource = null;
            currentGenerator = null;
            progress = -1;
            currentArea = area;
            ControlForm.SetCurrentScope(area);
            ControlForm.SetCurrentSelection(area);
            SwapImages(bitmap);
        }
        void OnCalculationError(Exception error)
        {
            Debug.WriteLine($"CALCULATION ERROR: Invoke: {InvokeRequired} Cancel: {cancellationTokenSource?.IsCancellationRequested.ToString() ?? "null"} Error: {error}");
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
            cancellationTokenSource = null;
            currentGenerator = null;
            progress = -1;
            MessageBox.Show(this, error.ToString(), "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void OnCalculationAborted()
        {
            Debug.WriteLine("CALCULATION ABORTED");
            cancellationTokenSource = null;
            currentGenerator = null;
            progress = -1;
            if (nextCalculation is {}) 
                StartCalculation(nextCalculation.Value);
        }
        void OnProgressTimer(object sender, EventArgs e)
        {
            var p = currentGenerator?.Progress ?? -1;
            if (p != -1 || progress != -1) Debug.WriteLine($"ONPROGRESSTIMER: {p} {progress}");
            if (p != progress)
            {
                Debug.WriteLine("ONPROGRESSTIMER: invalidate");
                progress = p;
                Invalidate();
            }
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
            Debug.WriteLine($"ONPAINT: {progress} {cancellationTokenSource?.IsCancellationRequested.ToString() ?? "null"} {currentGenerator?.ToString() ?? "null"}");
            if (progress > -1 && cancellationTokenSource?.IsCancellationRequested != true && currentGenerator != null)
            {
                Debug.WriteLine("ONPAINT: Painting");
                var text = $"{progress}%";
                var textSize = e.Graphics.MeasureString(text, progressFont);
                var textRect = new RectangleF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2, textSize.Width, textSize.Height);
                var s = Math.Max(textSize.Width, textSize.Height) + 50;
                var backRect = new RectangleF(textRect.Left + textRect.Width / 2 - s / 2, textRect.Top + textRect.Height/2 - s / 2, s, s);
                e.Graphics.FillEllipse(Brushes.Black, backRect);
                e.Graphics.DrawString(text, progressFont, Brushes.Green, textRect);
            }
            else
                Debug.WriteLine("ONPAINT: Not painting");
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
