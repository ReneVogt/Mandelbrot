using System.ComponentModel;

namespace Mandelbrot;

public class FullscreenableForm : Form
{
    FormWindowState previousState = FormWindowState.Normal;
    FormBorderStyle previousBorderStyle = FormBorderStyle.Sizable;

    public event EventHandler? FullscreenChanged;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Fullscreen
    {
        get => WindowState == FormWindowState.Maximized && FormBorderStyle == FormBorderStyle.None;
        set
        {
            if (value) EnterFullscreenMode();
            else LeaveFullscreenMode();
        }
    }

    public FullscreenableForm()
    {
        KeyPreview = true;
    }

    protected virtual void OnFullscreenChanged(EventArgs e) => FullscreenChanged?.Invoke(this, e);
    protected override void OnKeyDown(KeyEventArgs e)
    {
        _ = e ?? throw new ArgumentNullException(nameof(e));
        if (e.KeyCode == Keys.F11 && e.Modifiers == Keys.None)
        {
            e.Handled = true;
            Fullscreen = !Fullscreen;
        }
        base.OnKeyDown(e);
    }
    void EnterFullscreenMode()
    {
        if (Fullscreen) return;
        previousBorderStyle = FormBorderStyle;
        previousState = WindowState;
        WindowState = FormWindowState.Normal;
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        OnFullscreenChanged(EventArgs.Empty);
    }
    void LeaveFullscreenMode()
    {
        if (!Fullscreen) return;
        FormBorderStyle = previousBorderStyle;
        WindowState = previousState;
        OnFullscreenChanged(EventArgs.Empty);
    }
}
