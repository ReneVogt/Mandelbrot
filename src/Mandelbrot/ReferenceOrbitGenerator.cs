using OpenTK.Mathematics;
using System.Numerics;
using System.Windows.Forms.VisualStyles;

namespace Mandelbrot;

sealed class ReferenceOrbitGenerator
{
    readonly float _zoom;
    readonly Vector2d _center;
    readonly float _screenWidth, _screenHeight;
    readonly int _referencePointColumns, _referencePointRows;
    readonly int _maxIterations;
    readonly float[] _data;

    readonly double _aspectRatio;
    readonly double _tileWidth;
    readonly double _tileHeight;

    public static int ClimbingSteps { get; set; } = 5;

    ReferenceOrbitGenerator(int referencePointColumns, int referencePointRows, float screenWidth, float screenHeight, float zoom, Vector2d center, int maxIterations, float[] data)
    {
        _zoom = zoom;
        _center = center;
        _screenWidth = screenWidth; 
        _screenHeight = screenHeight;
        _referencePointColumns = referencePointColumns;
        _referencePointRows = referencePointRows;
        _maxIterations = maxIterations;
        _data = data;

        _aspectRatio = (double)_screenWidth / _screenHeight;
        _tileWidth = (1d / _referencePointColumns) * _aspectRatio;
        _tileHeight = 1d / _referencePointRows;
    }

    void GenerateOrbits() => Parallel.For(0, _referencePointRows * _referencePointColumns, GenerateOrbit);
    void GenerateOrbit(int index)
    {
        var row = index / _referencePointColumns;
        var column = index % _referencePointColumns;

        var x = -0.5d * _aspectRatio + column * _tileWidth + 0.5d * _tileWidth;
        var y = -0.5d + row * _tileHeight + 0.5d * _tileHeight;

        var c0 = ComplexFromNormalized(x, y);
        ClimbGradient(ref x, ref y, ref c0);

        var bufferStart = index * (_maxIterations + 1) * 2;
        var bufferEnd = bufferStart + 2 * _maxIterations;
        _data[bufferEnd] = (float)x;
        _data[bufferEnd+1] = (float)y;
        GenerateCompleteOrbit(c0, _data.AsSpan(bufferStart, 2 * _maxIterations));        
    }

    void ClimbGradient(ref double x, ref double y, ref Complex c0)
    {
        if (ClimbingSteps < 1) return;
        var iterations = CalculateEscapeIterations(c0);
        if (iterations >= _maxIterations) return;

        var dw = _tileWidth / 4;
        var dh = _tileHeight / 4;

        var improving = true;

        for(var i=0; i<ClimbingSteps && improving; i++)
        {
            improving = false;
            dw /= 2; dh /= 2;
            var currentX = x;
            var currentY = y;

            if (Test(currentX - dw, currentY - dh, ref x, ref y, ref c0)) return;
            if (Test(currentX - dw, currentY, ref x, ref y, ref c0)) return;
            if (Test(currentX - dw, currentY + dh, ref x, ref y, ref c0)) return;
            if (Test(currentX, currentY - dh, ref x, ref y, ref c0)) return;
            if (Test(currentX, currentY + dh, ref x, ref y, ref c0)) return;
            if (Test(currentX + dw, currentY - dh, ref x, ref y, ref c0)) return;
            if (Test(currentX + dw, currentY, ref x, ref y, ref c0)) return;
            if (Test(currentX + dw, currentY + dh, ref x, ref y, ref c0)) return;

            bool Test(double tx, double ty, ref double x, ref double y, ref Complex c0)
            {
                var c = ComplexFromNormalized(tx, ty);
                var iter = CalculateEscapeIterations(c);
                if (iter > iterations)
                {
                    iterations = iter;
                    x = tx;
                    y = ty;
                    c0 = c;
                    improving = true;
                }

                return iter >= _maxIterations;
            }
        }
    }

    void GenerateCompleteOrbit(Complex c0, Span<float> buffer)
    {
        var z0 = Complex.Zero;

        buffer[0] = 0;
        buffer[1] = 0;
        for (var i = 1; i < _maxIterations; i++)
        {
            var z = z0 * z0 + c0;
            buffer[i*2] = (float)z.Real;
            buffer[i*2+1] = (float)z.Imaginary;
            z0 = z;
        }
    }
    int CalculateEscapeIterations(Complex c0)
    {
        var z0 = Complex.Zero;
        for (var i = 1; i < _maxIterations; i++)
        {
            var z = z0 * z0 + c0;
            if (z.Magnitude > 2) return i;
            z0 = z;
        }

        return _maxIterations;
    }

    Complex ComplexFromNormalized(double x, double y)
    {
        var point = new Vector2d(x, y);
        point = point * _zoom + _center;
        return new Complex(point.X, point.Y);
    }

    public static void GenerateReferenceOrbits(int referencePointColumns, int referencePointRows, float screenWidth, float screenHeight, float zoom, Vector2d center, int maxIterations, float[] data) => 
        new ReferenceOrbitGenerator(
            referencePointColumns: referencePointColumns, 
            referencePointRows: referencePointRows, 
            screenWidth: screenWidth, 
            screenHeight: screenHeight, 
            zoom: zoom, 
            center: center, 
            maxIterations: maxIterations, 
            data: data).GenerateOrbits();
}
