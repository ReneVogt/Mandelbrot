using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Mandelbrot.Controls
{
    public partial class DlgSelectImageFormat : Form
    {
        readonly Dictionary<string, ImageFormat> formats = new Dictionary<string, ImageFormat>
        {
            ["Bmp"] = ImageFormat.Bmp,
            ["Emf"] = ImageFormat.Emf,
            ["Exif"] = ImageFormat.Exif,
            ["Gif"] = ImageFormat.Gif,
            ["Icon"] = ImageFormat.Icon,
            ["Jpeg"] = ImageFormat.Jpeg,
            ["Png"] = ImageFormat.Png,
            ["Tiff"] = ImageFormat.Tiff,
            ["Wmf"] = ImageFormat.Wmf
        };
        public ImageFormat ImageFormat
        {
            get => formats[cmbFormats.Text];
            set => cmbFormats.SelectedItem = formats.First(kvp => kvp.Value.Equals(value)).Key;
        }
        public string ImageFormatName => cmbFormats.Text;
        public DlgSelectImageFormat()
        {
            InitializeComponent();
            cmbFormats.Items.AddRange(formats.Keys.Cast<object>().ToArray());
            ImageFormat = ImageFormat.Bmp;
        }
    }
}
