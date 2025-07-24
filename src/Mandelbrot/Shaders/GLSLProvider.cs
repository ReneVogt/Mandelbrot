using OpenTK.Graphics.OpenGL;
using System.Reflection;

namespace Mandelbrot.Shaders;

static class GLSLProvider
{
    public static int CreateMandelbrotShader() => CreateShaderProgram(GLSLProvider.GetVertexShaderCode(), GLSLProvider.GetMandelbrotShaderCode());
    public static int CreatePerturbationShader() => CreateShaderProgram(GLSLProvider.GetVertexShaderCode(), GLSLProvider.GetPerturbationShaderCode());
    static int CreateShaderProgram(string vertexShaderCode, string fragmentShaderCode)
    {
        int vertex = CompileShader(ShaderType.VertexShader, vertexShaderCode);
        int fragment = CompileShader(ShaderType.FragmentShader, fragmentShaderCode);

        int program = GL.CreateProgram();
        GL.AttachShader(program, vertex);
        GL.AttachShader(program, fragment);
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            GL.GetProgramInfoLog(program, out var log);
            MessageBox.Show(log, "Shader program link error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);

        return program;

    }
    static int CompileShader(ShaderType type, string code)
    {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, code);
        GL.CompileShader(shader);
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(shader, out var log);
            MessageBox.Show(log, $"{type} compilation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
        return shader;
    }

    static string GetVertexShaderCode() => ReadEmbeddedResource("Mandelbrot.Shaders.fullscreen.vert");
    static string GetMandelbrotShaderCode() => ReadEmbeddedResource("Mandelbrot.Shaders.mandelbrot.frag");
    static string GetPerturbationShaderCode() => ReadEmbeddedResource("Mandelbrot.Shaders.perturbation.frag");
    static string ReadEmbeddedResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
