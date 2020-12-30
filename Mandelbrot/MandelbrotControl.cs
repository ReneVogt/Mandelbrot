using System;
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
        readonly struct CalculationContext
        {
            public Task<Bitmap> Task { get; }
            public CancellationTokenSource Cts { get; }
            public MandelbrotImageGenerator Generator { get; }

            public CalculationContext(Task<Bitmap> task, CancellationTokenSource cts, MandelbrotImageGenerator generator)
            {
                Task = task;
                Cts = cts;
                Generator = generator;
            }
        }
        readonly ControlForm controlForm = new ControlForm();
        readonly AnalyticColorizer colorizer = new AnalyticColorizer();
        CalculationContext? context;
        double realMin = -2, realMax = 1, imaginaryMin = -1, imaginaryMax = 1;
        Point? mouseStartingPoint;
        Rectangle? mouseSelection;

        public MandelbrotControl()
        {
            InitializeComponent();
            controlForm.SetCurrentScope(realMin, realMax, imaginaryMin, imaginaryMax);
            controlForm.SetCurrentSelection(realMin, realMax, imaginaryMin, imaginaryMax);
            controlForm.MaximumNumberOfIterations = 100;
            controlForm.RefreshClicked += (sender, e) => Recalculate();
        }

        void Recalculate()
        {
            if (DesignMode)
                return;
            _ = Calculate(realMin, imaginaryMin, realMax, imaginaryMax);
        }
        async Task Calculate(double minR, double minI, double maxR, double maxI)
        {
            if (InvokeRequired)
                throw new InvalidOperationException($"Illegal cross thread call in {nameof(MandelbrotControl)} '{Name}'!");

            if (context != null)
            {
                context.Value.Cts.Cancel();
                await context.Value.Task;
            }
            var cts = new CancellationTokenSource();
            var task = new Task<Bitmap>(() => Calculate(minR, minI, maxR, maxI, Width, Height, cts.Token));
            context = new CalculationContext(task, cts, new MandelbrotImageGenerator {Colorizer = colorizer, MaximumNumberOfIterations = controlForm.MaximumNumberOfIterations});
            colorizer.Reset();
            task.Start();
            try
            {
                SwapImages(await task);
                colorizer.DumpAnalytics();
                realMax = maxR;
                realMin = minR;
                imaginaryMax = maxI;
                imaginaryMin = minI;
                controlForm.SetCurrentScope(realMin, realMax, imaginaryMin, imaginaryMax);
                controlForm.SetCurrentSelection(realMin, realMax, imaginaryMin, imaginaryMax);
            }
            catch (OperationCanceledException) { }
            catch (MandelbrotException mbe)
            {
                ReportError(mbe);
            }
            finally
            {
                context = null;
            }
        }
        Bitmap Calculate(double minR, double minI, double maxR, double maxI, int width, int height, CancellationToken cancellationToken)
        {
            return context!.Value.Generator.CreateBitmap(width, height, minR, maxR, minI, maxI, cancellationToken);
        }
        void ReportError(Exception error)
        {
            MessageBox.Show(this, error.ToString(), "Mandelbrot error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <inheritdoc />
        protected override void OnHandleDestroyed(EventArgs e)
        {
            context?.Cts.Cancel();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Recalculate();
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            context?.Cts.Cancel();
            SwapImages(null);
            base.OnClientSizeChanged(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mouseSelection != null)
                e.Graphics.DrawRectangle(Pens.White, mouseSelection.Value);
            if (BackgroundImage is null && context?.Cts.IsCancellationRequested != false)
                Recalculate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && mouseSelection.HasValue)
            {
                var selection = GetBoundsFromRect(mouseSelection.Value);
                _ = Calculate(selection.minR, selection.minI, selection.maxR, selection.maxI);
            }
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
            {
                var (minr, maxr, mini, maxi) = GetBoundsFromRect(mouseSelection.Value);
                controlForm.SetCurrentSelection(minr, maxr, mini, maxi);
            }
            else
            {
                var (r, i) = GetComplexFromPoint(e.Location);
                controlForm.SetCurrentSelection(r, r, i, i);
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
                controlForm.Show(this);
        }
        void OnProgressTimer(object sender, EventArgs e)
        {
            var p = context?.Generator.Progress ?? 0;
            pnProgress.Visible = p != 0;
            calculationProgressBar.Value = p;
        }
        void SwapImages(Bitmap? bitmap)
        {
            var old = BackgroundImage;
            BackgroundImage = bitmap;
            old?.Dispose();
        }
        (double minR, double maxR, double minI, double maxI) GetBoundsFromRect(Rectangle rect)
        {
            var topLeft = GetComplexFromPoint(rect.Location);
            var bottomRight = GetComplexFromPoint(rect.Location + rect.Size);
            return (topLeft.r, bottomRight.r, bottomRight.i, topLeft.i);
        }
        (double r, double i) GetComplexFromPoint(Point p) => (realMin + (realMax - realMin) * p.X / Width,
                                                                 imaginaryMax - (imaginaryMax - imaginaryMin) * p.Y / Height);
    }
}
