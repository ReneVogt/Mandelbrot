using System;
using System.Windows.Forms;

#nullable enable

namespace Mandelbrot
{
    public sealed class FullScreen
    {
        readonly Form form;
        FormWindowState previousState = FormWindowState.Normal;
        FormBorderStyle previousBorderStyle = FormBorderStyle.Sizable;
        public bool IsFullScreen => form.WindowState == FormWindowState.Maximized && form.FormBorderStyle == FormBorderStyle.None;
        public FullScreen(Form form)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form));
            this.form.KeyPreview = true;
            this.form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.F11) Toggle();
            };
        }
        public void Toggle()
        {
            if (IsFullScreen)
                Leave();
            else
                Enter();
        }
        public void Enter()
        {
            if (IsFullScreen) return;
            previousBorderStyle = form.FormBorderStyle;
            previousState = form.WindowState;
            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
        }
        public void Leave()
        {
            if (!IsFullScreen) return;
            form.FormBorderStyle = previousBorderStyle;
            form.WindowState = previousState;
        }
    }
}
