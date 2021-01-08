using System;
using System.Windows.Forms;
using Mandelbrot.Controls;
using Mandelbrot.Properties;

namespace Mandelbrot
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Settings.Default.Upgraded)
            {
                Settings.Default.Upgrade();
                Settings.Default.Upgraded = true;
                Settings.Default.Save();
            }

            Settings.Default.SavedScopes ??= new SavedScopes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MandelbrotForm());
        }
    }
}
