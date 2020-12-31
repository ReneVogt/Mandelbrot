using System;
using System.Windows.Forms;

namespace Mandelbrot
{
    public sealed class LogarithmicUpDown : NumericUpDown
    {
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            try
            {
                Increment = (int)Math.Pow(10, (int)Math.Log10((double)Value));

            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
    }
}
