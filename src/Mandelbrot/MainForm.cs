using Mandelbrot.Shaders;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Diagnostics;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using MouseButton = OpenTK.Windowing.GraphicsLibraryFramework.MouseButton;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace Mandelbrot;

public partial class MainForm : FullscreenableForm
{
    const float totalZoom = 5f;
    const double initialCenterX = -0.5f;
    const double initialCenterY = 0f;
    const float zoomStep = 1.1f;
    const float translateStep = 10f;
    const int iterationLimit = 8000;
    
    const float perturbationThreshold = 1e-7f;
    const int referencePointRows = 2;
    const int referencePointColumns = 2;    

    readonly float[] _z0Data;

    InfoForm? _infoForm;
    string _renderer = "-";

    int _vao, _vbo, _mandelbrotShader, _perturbationShader, _z0Texture;
    Vector2d _center = new(initialCenterX, initialCenterY);
    float _zoom = totalZoom;

    int _maxIterations;

    INativeInput? _nativeInput;

    CancellationTokenSource? _animationCancellation;

    bool UsePerturbation => perturbationThreshold * glControl.ClientSize.Height > _zoom;

    public MainForm()
    {
        _z0Data = new float[(iterationLimit + 1) * referencePointColumns * referencePointRows * 2];
        InitializeComponent();
    }

    void OnLoadGL(object sender, EventArgs e)
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

        _mandelbrotShader = GLSLProvider.CreateMandelbrotShader();
        _perturbationShader = GLSLProvider.CreatePerturbationShader();

        _z0Texture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _z0Texture);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        _nativeInput = glControl.EnableNativeInput();
        _nativeInput.KeyDown += OnKeyDownGL;
        _nativeInput.MouseMove += OnMouseMoveGL;
        _nativeInput.MouseWheel += OnMouseWheelGL;
        _nativeInput.MouseUp += OnMouseUpGL;

        _renderer = GL.GetString(StringName.Renderer);
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
                if (_infoForm?.IsDisposed != false)
                {
                    _infoForm = new();
                    _infoForm.Renderer = _renderer;
                    _infoForm.Show(this);
                }
                else
                {
                    _infoForm.Close();
                    _infoForm = null;
                }
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
            case Keys.F12:
                KeyDrivenTestMethod();
                break;
        }
    }

    DateTime _lastMouseUpTime = DateTime.MinValue;
    Vector2 _lastMouseUpPoint = new(-100, -100);

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
        _maxIterations = Math.Clamp(_maxIterations, 100, iterationLimit);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        if (UsePerturbation)
            PreparePerturbationRendering();
        else
            PrepareMandelbrotRendering();

        GL.BindVertexArray(_vao);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        glControl.SwapBuffers();

        UpdateInfoPanel();
    }
    void PrepareMandelbrotRendering()
    {
        var centerxh = (float)_center.X;
        var centerxl = (float)(_center.X - centerxh);
        var centeryh = (float)_center.Y;
        var centeryl = (float)(_center.Y - centeryh);

        GL.UseProgram(_mandelbrotShader);
        GL.Uniform2(GL.GetUniformLocation(_mandelbrotShader, "_resolution"), (float)glControl.ClientSize.Width, (float)glControl.ClientSize.Height);
        GL.Uniform2(GL.GetUniformLocation(_mandelbrotShader, "_centerHigh"), centerxh, centeryh);
        GL.Uniform2(GL.GetUniformLocation(_mandelbrotShader, "_centerLow"), centerxl, centeryl);
        GL.Uniform1(GL.GetUniformLocation(_mandelbrotShader, "_zoom"), _zoom);
        GL.Uniform1(GL.GetUniformLocation(_mandelbrotShader, "_maxIterations"), _maxIterations);
    }
    void PreparePerturbationRendering()
    {
        var referenceCount = referencePointRows * referencePointColumns;

        ReferenceOrbitGenerator.GenerateReferenceOrbits(
            referencePointColumns: referencePointColumns, 
            referencePointRows: referencePointRows,
            screenWidth: glControl.ClientSize.Width, 
            screenHeight: glControl.ClientSize.Height, 
            zoom: _zoom, 
            center: _center, 
            maxIterations: _maxIterations, 
            data: _z0Data);
        GL.BindTexture(TextureTarget.Texture2D, _z0Texture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rg32f, _maxIterations + 1, referenceCount, 0, PixelFormat.Rg, PixelType.Float, _z0Data);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);

        GL.UseProgram(_perturbationShader);
        GL.Uniform2(GL.GetUniformLocation(_perturbationShader, "_resolution"), (float)glControl.ClientSize.Width, (float)glControl.ClientSize.Height);
        GL.Uniform1(GL.GetUniformLocation(_perturbationShader, "_zoom"), _zoom);
        GL.Uniform1(GL.GetUniformLocation(_perturbationShader, "_maxIterations"), _maxIterations);
        GL.Uniform1(GL.GetUniformLocation(_perturbationShader, "_referenceTexture"), 0);
        GL.Uniform1(GL.GetUniformLocation(_perturbationShader, "_referenceCount"), referenceCount);
    }

    void TransformView(float translateX = 0, float translateY = 0, float zoomFactor = 0, float zoomTarget = 0)
    {
        _animationCancellation?.Cancel();
        if (zoomTarget > 0)
            _zoom = zoomTarget;
        else if (zoomFactor != 0)
            _zoom *= MathF.Pow(zoomStep, zoomFactor);

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
        _center = new(initialCenterX, initialCenterY);
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
        for (var i = 0; i<animationSteps; i++)
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
        if (_infoForm?.Visible != true) return;
        _infoForm.SuspendLayout();
        _infoForm.CenterX = _center.X.ToString();
        _infoForm.CenterY = _center.Y.ToString();

        var mouseCoords = GetCoordsFromPixel(_nativeInput!.MousePosition);
        _infoForm.MouseX = mouseCoords.X.ToString();
        _infoForm.MouseY = mouseCoords.Y.ToString();

        _infoForm.Zoom = _zoom.ToString();
        _infoForm.Iterations = _maxIterations.ToString();

        _infoForm.WindowW = glControl.ClientSize.Width.ToString();
        _infoForm.WindowH = glControl.ClientSize.Height.ToString();

        _infoForm.Perturbation = UsePerturbation ? "yes" : "no";

        _infoForm.ResumeLayout();
    }

    Vector2d GetCoordsFromPixel(Vector2 pixelPosition)
    {
        var x = (pixelPosition.X - 0.5d * glControl.ClientSize.Width) / glControl.ClientSize.Height;
        x = x * _zoom + _center.X;
        var y = ((glControl.ClientSize.Height - pixelPosition.Y) - 0.5d * glControl.ClientSize.Height) / glControl.ClientSize.Height;
        y = y * _zoom + _center.Y;
        return new Vector2d(x, y);
    }

    [Conditional("DEBUG")]
    void KeyDrivenTestMethod()
    {
        _center = new Vector2d(-0.5659488886088083, 0.6390228890750222);
        _zoom = 6.400001E-05f;
        glControl.Invalidate();
    }

}
