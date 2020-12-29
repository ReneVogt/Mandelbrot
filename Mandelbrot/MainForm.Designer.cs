
namespace Mandelbrot
{
    partial class MainForm
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
            this.mandelbrotControl1 = new Mandelbrot.MandelbrotControl();
            this.SuspendLayout();
            // 
            // mandelbrotControl1
            // 
            this.mandelbrotControl1.BackColor = System.Drawing.Color.Black;
            this.mandelbrotControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mandelbrotControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mandelbrotControl1.Location = new System.Drawing.Point(0, 0);
            this.mandelbrotControl1.Name = "mandelbrotControl1";
            this.mandelbrotControl1.Size = new System.Drawing.Size(800, 450);
            this.mandelbrotControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mandelbrotControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MandelbrotControl mandelbrotControl1;
    }
}

