#version 330 core
out vec4 outputColor;

uniform vec2 _centerHigh;
uniform vec2 _centerLow;
uniform float _zoom;
uniform vec2 _resolution;
uniform int _maxIterations; 

float iterate(vec2 c)
{
    vec2 z = vec2(0.0);
    int i;
    for (i = 0; i < _maxIterations; i++)
    {
        float x = (z.x * z.x - z.y * z.y) + c.x;
        float y = (2.0 * z.x * z.y) + c.y;
        z = vec2(x, y);
        if (dot(z, z) > 4.0) break;
    }

    if (i == _maxIterations)
        return float(i);

    float l = length(z);
    if (l < 1.000001) l = 1.000001;
    return float(i) + 1.0 - log(log(l)) / log(2.0);;
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
    vec2 offset = normalizedPixel * _zoom;
    vec2 point = _centerLow + offset;
    point = _centerHigh + point;

    float i = iterate(point);
    vec3 color = calculateColor(i);
    outputColor = vec4(color, 1.0);
}
