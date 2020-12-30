using System;
using System.Windows.Forms;
using Mandelbrot.Properties;

#nullable enable

namespace Mandelbrot
{
    public partial class ControlForm : Form
    {
        public event EventHandler? RefreshClicked;
        public event EventHandler? AdjustImaginaryAxisClicked;
        public event EventHandler? AdjustRealAxisClicked;
        public event EventHandler? ReturnToTotalViewClicked;

        public int MaximumNumberOfIterations
        {
            get => (int)nupIterations.Value;
            set
            {
                try
                {
                    nupIterations.Value = value;
                }
                catch(ArgumentOutOfRangeException){}
            }
        }

        public ControlForm()
        {
            Icon = Resources.Mandelbrot;
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }
        public void SetCurrentScope(double minR, double maxR, double minI, double maxI)
        {
            lbCurrentReal.Text = CreateAxisString(minR, maxR);
            lbCurrentImaginary.Text = CreateAxisString(minI, maxI);
        }
        public void SetCurrentSelection(double minR, double maxR, double minI, double maxI)
        {
            lbSelectionReal.Text = CreateAxisString(minR, maxR);
            lbSelectionImaginary.Text = CreateAxisString(minI, maxI);
        }
        static string CreateAxisString(double min, double max) => $"{min:G17} to {max:G17}";

        void btRefresh_Click(object sender, EventArgs e)
        {
            RefreshClicked?.Invoke(this, e);
        }
        private void btAdjustImaginary_Click(object sender, EventArgs e)
        {
            AdjustImaginaryAxisClicked?.Invoke(this, e);
        }
        private void btAdjustReal_Click(object sender, EventArgs e)
        {
            AdjustRealAxisClicked?.Invoke(this, e);
        }
        private void btStartScreen_Click(object sender, EventArgs e)
        {
            ReturnToTotalViewClicked?.Invoke(this, e);
        }
    }
}
