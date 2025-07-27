using OpenTK.Mathematics;
using System.Numerics;

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

        var fx = -0.5d * _aspectRatio + column * _tileWidth + 0.5d * _tileWidth;
        var fy = -0.5d + row * _tileHeight + 0.5d * _tileHeight;
        var t = GetTextureIndex(index, _maxIterations);
        _data[t] = (float)fx;
        _data[t+1] = (float)fy;
        var point = new Vector2d(fx, fy);
        point = point * _zoom + _center;
        var c = new Complex(point.X, point.Y);
        Prepare(c, GetTextureIndex(index, 0));        

        void Prepare(Complex c0, int offset)
        {
            var z0 = Complex.Zero;

            _data[offset] = 0;
            _data[offset+1] = 0;
            for (var i = 1; i < _maxIterations; i++)
            {
                var z = z0 * z0 + c0;
                _data[offset + i*2] = (float)z.Real;
                _data[offset + i*2+1] = (float)z.Imaginary;
                z0 = z;
            }
        }

        int GetTextureIndex(int reference, int iteration) => (reference * (_maxIterations+1) + iteration) * 2;
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
