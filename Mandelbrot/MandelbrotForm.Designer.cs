
namespace Mandelbrot
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
            this.pbView = new System.Windows.Forms.Panel();
            this.progroessTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pbView
            // 
            this.pbView.BackColor = System.Drawing.Color.Black;
            this.pbView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbView.Location = new System.Drawing.Point(0, 0);
            this.pbView.Name = "pbView";
            this.pbView.Size = new System.Drawing.Size(788, 575);
            this.pbView.TabIndex = 0;
            this.pbView.SizeChanged += new System.EventHandler(this.pbView_SizeChanged);
            this.pbView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbView_Paint);
            this.pbView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseClick);
            this.pbView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseDoubleClick);
            this.pbView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseMove);
            this.pbView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseUp);
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
            this.ClientSize = new System.Drawing.Size(788, 575);
            this.Controls.Add(this.pbView);
            this.DoubleBuffered = true;
            this.Name = "MandelbrotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mandelbrot set viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pbView;
        private System.Windows.Forms.Timer progroessTimer;
    }
}

