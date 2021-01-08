using System;
using System.Drawing.Imaging;

#nullable enable

namespace Mandelbrot.Controls
{
    public sealed class SaveImageClickedEventArgs : EventArgs
    {
        public string ImageFormatName { get; }
        public ImageFormat ImageFormat { get; }

        public SaveImageClickedEventArgs(string imageFormatName, ImageFormat imageFormat)
        {
            ImageFormatName = imageFormatName;
            ImageFormat = imageFormat;
        }
    }
}
