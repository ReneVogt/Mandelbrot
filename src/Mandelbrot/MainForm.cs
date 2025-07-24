using Mandelbrot.Shaders;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using MouseButton = OpenTK.Windowing.GraphicsLibraryFramework.MouseButton;

namespace Mandelbrot;

public partial class MainForm : FullscreenableForm
{
    const float totalZoom = 2f;
    const double centerX = -0.5f;
    const double centerY = 0f;
    const float zoomStep = 1.1f;
    const float translateStep = 10f;

    int _vao, _vbo, _shaderProgram;
    Vector2d _center = new(centerX, centerY);
    float _zoom = totalZoom;
    int _maxIterations;

    INativeInput? _nativeInput;

    public MainForm()
    {
        InitializeComponent();
    }

    private void OnLoadGL(object sender, EventArgs e)
    {
        GL.ClearColor(Color4.Black);

        float[] quadVertices = [
            -1f, -1f,  1f, -1f,  -1f, 1f,
            -1f,  1f,  1f, -1f,   1f, 1f
        ];

        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.StaticDraw);

        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        _shaderProgram = GLSLProvider.CreateShaderProgram();

        _nativeInput = glControl.EnableNativeInput();
        _nativeInput.KeyDown += OnKeyDownGL;
        _nativeInput.MouseMove += OnMouseMoveGL;
        _nativeInput.MouseWheel += OnMouseWheelGL;
    }

    void OnPreviewKeyDownGL(object sender, PreviewKeyDownEventArgs e)
    {
        e.IsInputKey = true;
    }
    void OnKeyDownGL(KeyboardKeyEventArgs args)
    {
        switch (args.Key)
        {
            case Keys.F11:
                Fullscreen = !Fullscreen;
                break;
            case Keys.Escape:
                _zoom = totalZoom;
                _center = new(centerX, centerY);
                glControl.Invalidate();
                break;
            case Keys.F1:
                infoPanel.Visible = !infoPanel.Visible;
                break;
            case Keys.Up:
                Translate(new Vector2(0, translateStep));
                break;
            case Keys.Down:
                Translate(new Vector2(0, -translateStep));
                break;
            case Keys.Left:
                Translate(new Vector2(translateStep, 0));
                break;
            case Keys.Right:
                Translate(new Vector2(-translateStep, 0));
                break;
            case Keys.PageUp:
                Zoom(-1);
                break;
            case Keys.PageDown:
                Zoom(1);
                break;
        }
    }

    void OnMouseWheelGL(MouseWheelEventArgs args) => Zoom(-args.OffsetY);
    void OnMouseMoveGL(MouseMoveEventArgs args)
    {
        if (!_nativeInput!.IsMouseButtonDown(MouseButton.Left))
        {
            UpdateInfoPanel();
            return;
        }
        Translate(args.Delta);
    }
    void OnResizeGL(object sender, EventArgs e)
    {
        GL.Viewport(0, 0, glControl.Width, glControl.Height);
    }
    void OnPaintGL(object sender, PaintEventArgs e)
    {
        _maxIterations = (int)(100.0f + 50.0f * Math.Log2(1.0 / _zoom));
        _maxIterations = Math.Clamp(_maxIterations, 100, 5000);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        var centerxh = (float)_center.X;
        var centerxl = (float)(_center.X - centerxh);
        var centeryh = (float)_center.Y;
        var centeryl = (float)(_center.Y - centeryh);

        GL.UseProgram(_shaderProgram);
        GL.Uniform2(GL.GetUniformLocation(_shaderProgram, "uResolution"), (float)glControl.ClientSize.Width, (float)glControl.ClientSize.Height);
        GL.Uniform2(GL.GetUniformLocation(_shaderProgram, "uCenterHigh"), centerxh, centeryh);
        GL.Uniform2(GL.GetUniformLocation(_shaderProgram, "uCenterLow"), centerxl, centeryl);
        GL.Uniform1(GL.GetUniformLocation(_shaderProgram, "uZoom"), _zoom);
        GL.Uniform1(GL.GetUniformLocation(_shaderProgram, "uMaxIterations"), _maxIterations);

        GL.BindVertexArray(_vao);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        glControl.SwapBuffers();
        UpdateInfoPanel();
    }

    void Translate(Vector2 delta)
    {
        _center += new Vector2(-1, 1) * _zoom * delta / glControl.ClientSize.Height;
        glControl.Invalidate();
    }
    void Zoom(float factor)
    {
        _zoom *= MathF.Pow(zoomStep, factor);
        _zoom = Math.Clamp(_zoom, 1e-15f, 2f);
        glControl.Invalidate();
    }
    void UpdateInfoPanel()
    {
        if (!infoPanel.Visible) return;
        infoPanel.SuspendLayout();
        labelCenterX.Text = _center.X.ToString();
        labelCenterY.Text = _center.Y.ToString();

        var x = (_nativeInput!.MousePosition.X - 0.5d * glControl.ClientSize.Width) / glControl.ClientSize.Height;
        x = x * _zoom + _center.X;
        var y = ((glControl.ClientSize.Height - _nativeInput?.MousePosition.Y) - 0.5d * glControl.ClientSize.Height) / glControl.ClientSize.Height;
        y = y * _zoom + _center.Y;
        labelMouseX.Text = x.ToString();
        labelMouseY.Text = y.ToString();

        labelZoom.Text = _zoom.ToString();
        labelIterations.Text = _maxIterations.ToString();

        labelGPU.Text = GL.GetString(StringName.Renderer);

        infoPanel.ResumeLayout();
    }
}
