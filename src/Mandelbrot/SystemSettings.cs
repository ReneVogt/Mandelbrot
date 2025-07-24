using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Mandelbrot;

static class SystemSettings
{
    static class Native
    {
        public const int SM_CXDOUBLECLK = 36;
        public const int SM_CYDOUBLECLK = 37;

        [DllImport("user32.dll")]
        public static extern uint GetDoubleClickTime();
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int index);

    }
    public static TimeSpan DoubleClickTime => TimeSpan.FromMilliseconds(Native.GetDoubleClickTime());

    public static bool IsInDoubleClickDistance(this Vector2 currentPoint, Vector2 lastPoint)
    {
        var allowed = new Vector2(Native.GetSystemMetrics(Native.SM_CXDOUBLECLK), Native.GetSystemMetrics(Native.SM_CYDOUBLECLK));
        var distance = currentPoint - lastPoint;
        return Math.Abs(distance.X) <= allowed.X && Math.Abs(distance.Y) <= allowed.Y;
    }
}
