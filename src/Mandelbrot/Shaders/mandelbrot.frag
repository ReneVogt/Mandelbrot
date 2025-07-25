#version 330 core
out vec4 FragColor;

uniform vec2 uCenterHigh;
uniform vec2 uCenterLow;
uniform float uZoom;
uniform vec2 uResolution;
uniform int uMaxIterations; 

float mandelbrot(vec2 c)
{
    vec2 z = vec2(0.0);
    int i;
    for (i = 0; i < uMaxIterations; i++)
    {
        float x = (z.x * z.x - z.y * z.y) + c.x;
        float y = (2.0 * z.x * z.y) + c.y;
        z = vec2(x, y);
        if (dot(z, z) > 4.0) break;
    }

    if (i == uMaxIterations)
        return float(i);

    float l = length(z);
    if (l < 1.000001) l = 1.000001;
    return float(i) + 1.0 - log(log(l)) / log(2.0);;
}

vec3 colorPalette(float i)
{
    if (i >= uMaxIterations) return vec3(0.0, 0.0, 0.0);
    float t = i / float(uMaxIterations);
    return vec3(0.5 + 0.5 * cos(6.2831 * (t + vec3(0.25, 0.58, 0.92))));
}

void main()
{
    vec2 uv = (gl_FragCoord.xy - 0.5 * uResolution) / uResolution.y;
    uv = uv * uZoom;
    uv = uCenterLow + uv;
    uv = uCenterHigh + uv;

    float i = mandelbrot(uv);
    vec3 color = colorPalette(i);
    FragColor = vec4(color, 1.0);
}
