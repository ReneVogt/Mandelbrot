
namespace Mandelbrot.Controls
{
    partial class MandelbrotForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progroessTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progroessTimer
            // 
            this.progroessTimer.Enabled = true;
            this.progroessTimer.Interval = 40;
            this.progroessTimer.Tick += new System.EventHandler(this.OnProgressTimer);
            // 
            // MandelbrotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(788, 575);
            this.DoubleBuffered = true;
            this.Name = "MandelbrotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mandelbrot set viewer";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer progroessTimer;
    }
}

