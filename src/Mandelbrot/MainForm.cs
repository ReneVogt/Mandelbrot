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
    const float totalZoom = 5f;
    const double centerX = -0.5f;
    const double centerY = 0f;
    const float zoomStep = 1.1f;
    const float translateStep = 10f;

    int _vao, _vbo, _shaderProgram;
    Vector2d _center = new(centerX, centerY);
    float _zoom = totalZoom;
    int _maxIterations;

    INativeInput? _nativeInput;

    CancellationTokenSource? _animationCancellation;

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
        _nativeInput.MouseUp += OnMouseUpGL;
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
                ResetView();                
                break;
            case Keys.F1:
                infoPanel.Visible = !infoPanel.Visible;
                break;
            case Keys.Up:
                TransformView(translateY: -translateStep);
                break;
            case Keys.Down:
                TransformView(translateY: translateStep);
                break;
            case Keys.Left:
                TransformView(translateX: translateStep);
                break;
            case Keys.Right:
                TransformView(translateX: -translateStep);
                break;
            case Keys.PageUp:
                TransformView(zoomFactor: -1);
                break;
            case Keys.PageDown:
                TransformView(zoomFactor: 1);
                break;
        }
    }


    DateTime _lastMouseUpTime = DateTime.MinValue;
    Vector2 _lastMouseUpPoint = new (-100, -100);

    void OnMouseUpGL(MouseButtonEventArgs args)
    {
        if (args.Button != MouseButton.Left) return;
        
        if (DateTime.Now - _lastMouseUpTime > SystemSettings.DoubleClickTime || !_nativeInput!.MousePosition.IsInDoubleClickDistance(_lastMouseUpPoint))
        {
            _lastMouseUpPoint = _nativeInput!.MousePosition;
            _lastMouseUpTime = DateTime.Now;
            return;
        }

        _ = ZoomCenterAsync(GetCoordsFromPixel(_nativeInput.MousePosition));
    }
    void OnMouseWheelGL(MouseWheelEventArgs args) => TransformView(zoomFactor: -args.OffsetY);
    void OnMouseMoveGL(MouseMoveEventArgs args)
    {
        if (!_nativeInput!.IsMouseButtonDown(MouseButton.Left))
        {
            UpdateInfoPanel();
            return;
        }
        TransformView(translateX: -args.DeltaX, translateY: args.DeltaY);
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


    void TransformView(float translateX = 0, float translateY = 0, float zoomFactor = 0, float zoomTarget = 0)
    {
        _animationCancellation?.Cancel();
        if (zoomTarget > 0)
            _zoom = zoomTarget;
        else if (zoomFactor != 0)
        {
            _zoom *= MathF.Pow(zoomStep, zoomFactor);
            _zoom = Math.Clamp(_zoom, 1e-15f, totalZoom);
        }

        if (translateX != 0 || translateY != 0)
        {
            var delta = new Vector2(translateX, translateY);
            _center += _zoom * delta / glControl.ClientSize.Height;
        }
        glControl.Invalidate();
    }
    void ResetView()
    {
        _animationCancellation?.Cancel();
        _center = new(centerX, centerY);
        _zoom = totalZoom;
        glControl.Invalidate();
    }
    async Task ZoomCenterAsync(Vector2d targetCenter)
    {
        const int animationSteps = 20;
        const float zoomTarget = 0.2f;

        _animationCancellation?.Cancel();
        _animationCancellation = new();
        var cancellationToken = _animationCancellation.Token;

        var initialZoom = _zoom;
        var distance = targetCenter - _center;
        var step = distance/animationSteps;
        var x = MathF.Pow(zoomTarget, 1f / animationSteps);
        for (var i=0; i<animationSteps; i++)
        {
            if (cancellationToken.IsCancellationRequested) return;
            _center += step;
            _zoom *= x;
            glControl.Invalidate();
            await Task.Delay(5);
        }

        if (cancellationToken.IsCancellationRequested) return;
        _center = targetCenter;
        _zoom = initialZoom * zoomTarget;
        glControl.Invalidate();
    }

    void UpdateInfoPanel()
    {
        if (!infoPanel.Visible) return;
        infoPanel.SuspendLayout();
        labelCenterX.Text = _center.X.ToString();
        labelCenterY.Text = _center.Y.ToString();

        var mouseCoords = GetCoordsFromPixel(_nativeInput!.MousePosition);
        labelMouseX.Text = mouseCoords.X.ToString();
        labelMouseY.Text = mouseCoords.Y.ToString();

        labelZoom.Text = _zoom.ToString();
        labelIterations.Text = _maxIterations.ToString();

        labelGPU.Text = GL.GetString(StringName.Renderer);

        infoPanel.ResumeLayout();
    }

    Vector2d GetCoordsFromPixel(Vector2 pixelPosition)
    {
        var x = (pixelPosition.X - 0.5d * glControl.ClientSize.Width) / glControl.ClientSize.Height;
        x = x * _zoom + _center.X;
        var y = ((glControl.ClientSize.Height - pixelPosition.Y) - 0.5d * glControl.ClientSize.Height) / glControl.ClientSize.Height;
        y = y * _zoom + _center.Y;
        return new Vector2d(x, y);
    }
}
