using System;
using System.Diagnostics.CodeAnalysis;
using Mandelbrot.Properties;

#nullable enable

namespace Mandelbrot
{
    public partial class MainForm : FullscreenableForm
    {
        public MainForm()
        {
            Icon = Resources.Mandelbrot;
            InitializeComponent();
        }
        protected override void OnFullscreenChanged(EventArgs e)
        {
            base.OnFullscreenChanged(e);
            mandelbrotControl1.ControlForm.Fullscreen = Fullscreen;
        }
    }
}
