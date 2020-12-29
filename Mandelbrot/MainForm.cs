using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Mandelbrot
{
    public partial class MainForm : Form
    {
        // ReSharper disable once NotAccessedField.Local
        [SuppressMessage("CodeQuality", "IDE0052:Ungelesene private Member entfernen", Justification = "Necessary for fullscreen mode, should not be GCed.")]
        readonly FullScreen fullScreen;
        public MainForm()
        {
            InitializeComponent();
            fullScreen = new FullScreen(this);
        }
    }
}
