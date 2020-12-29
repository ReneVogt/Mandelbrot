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
        readonly AnalyticColorizer colorizer = new AnalyticColorizer();
        CalculationContext? context;
        double realMin = -2, realMax = 1, imaginaryMin = -1, imaginaryMax = 1;

        public MandelbrotControl()
        {
            InitializeComponent();
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
            context = new CalculationContext(task, cts, new MandelbrotImageGenerator {Colorizer = colorizer});
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
            if (BackgroundImage is null && context?.Cts.IsCancellationRequested != false)
                Recalculate();
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
    }
}
