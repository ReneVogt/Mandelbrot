#version 330 core
out vec4 FragColor;

uniform float uZoom;
uniform vec2 uResolution;
uniform int uMaxIterations; 
uniform sampler1D uZ0Tex;
uniform int uReferencePointCount;
uniform vec2 uReferencePoints[64];

float mandelbrot(vec2 c, int offset)
{
    vec2 dz = c;
    int i;
    float l = 1.0;
    for (i = 0; i < uMaxIterations; i++)
    {
        vec2 z0 = texelFetch(uZ0Tex, offset+i, 0).xy;
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
   
    if (i == uMaxIterations)
        return float(i);

    if (l < 1.000001) l = 1.000001;
    return float(i) + 1.0 - log(log(l)) / log(2.0);
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
    int referencePoint = 0;
    vec2 min = uv - uReferencePoints[0];
    float minLength = length(min);
    for (int i = 1; i < uReferencePointCount; i++)
    {
        vec2 d = uv - uReferencePoints[i];
        float l = length(d);
        if (l >= minLength) continue;
        minLength = l;
        min = d;
        referencePoint = i;
    }

    uv = min * uZoom;
    vec3 color = colorPalette(mandelbrot(uv, referencePoint * uMaxIterations));
    FragColor = vec4(color, 1.0);
}
