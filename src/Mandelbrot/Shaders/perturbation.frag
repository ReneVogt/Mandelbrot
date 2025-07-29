#version 330 core
out vec4 outputColor;

uniform float _zoom;
uniform vec2 _resolution;
uniform int _maxIterations; 
uniform sampler2D _referenceTexture;
uniform int _referenceCount;
uniform bool _showReferencePoints;

float iterate(vec2 c, int referencePoint)
{
    vec2 dz = c;
    int i;
    float l = 1.0;
    for (i = 0; i < _maxIterations; i++)
    {
        vec2 z0 = texelFetch(_referenceTexture, ivec2(i, referencePoint), 0).xy;
        vec2 Z = z0 + dz;
        if (dot(Z, Z) > 4.0)
        {
            l = length(Z);
            break;
        }
        
        vec2 dz2 = vec2(dz.x * dz.x - dz.y * dz.y, 2.0 * dz.x * dz.y);
        vec2 twoZ0dz = vec2(2.0 * (z0.x * dz.x - z0.y * dz.y), 2.0 * (z0.x * dz.y + z0.y * dz.x));
        dz = twoZ0dz + dz2 + c;
    }
   
    if (i >= _maxIterations)
        return float(i);

    if (l < 1.000001) l = 1.000001;
    return float(i) + 1.0 - log(log(l)) / log(2.0);
}

vec3 calculateColor(float i)
{
    if (i >= _maxIterations) return vec3(0.0, 0.0, 0.0);
    float t = i / float(_maxIterations);
    return vec3(0.5 + 0.5 * cos(6.2831 * (t + vec3(0.25, 0.58, 0.92))));
}

void main()
{
    vec2 normalizedPixel = (gl_FragCoord.xy - 0.5 * _resolution) / _resolution.y;
    int referencePoint = 0;
    vec2 min = normalizedPixel - texelFetch(_referenceTexture, ivec2(_maxIterations, 0), 0).xy;
    float minLength = length(min);
    for (int i = 1; i < _referenceCount; i++)
    {
        vec2 d = normalizedPixel - texelFetch(_referenceTexture, ivec2(_maxIterations, i), 0).xy;
        float l = length(d);
        if (l >= minLength) continue;
        minLength = l;
        min = d;
        referencePoint = i;
    }

    if (_showReferencePoints && minLength < 0.02)
    {
        outputColor = vec4(1.0, 1.0, 1.0, 1.0);
        return;
    }

    vec2 offset = min * _zoom;
    vec3 color = calculateColor(iterate(offset, referencePoint));
    outputColor = vec4(color, 1.0);
}
