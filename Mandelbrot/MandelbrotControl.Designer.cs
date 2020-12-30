
namespace Mandelbrot
{
    partial class MandelbrotControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.calculationProgressBar = new System.Windows.Forms.ProgressBar();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.pnProgress = new System.Windows.Forms.Panel();
            this.pnProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // calculationProgressBar
            // 
            this.calculationProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calculationProgressBar.BackColor = System.Drawing.Color.Red;
            this.calculationProgressBar.ForeColor = System.Drawing.Color.Chartreuse;
            this.calculationProgressBar.Location = new System.Drawing.Point(8, 17);
            this.calculationProgressBar.Name = "calculationProgressBar";
            this.calculationProgressBar.Size = new System.Drawing.Size(731, 25);
            this.calculationProgressBar.TabIndex = 0;
            // 
            // progressTimer
            // 
            this.progressTimer.Enabled = true;
            this.progressTimer.Interval = 40;
            this.progressTimer.Tick += new System.EventHandler(this.OnProgressTimer);
            // 
            // pnProgress
            // 
            this.pnProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnProgress.BackColor = System.Drawing.Color.Black;
            this.pnProgress.Controls.Add(this.calculationProgressBar);
            this.pnProgress.Location = new System.Drawing.Point(3, 449);
            this.pnProgress.Name = "pnProgress";
            this.pnProgress.Size = new System.Drawing.Size(746, 59);
            this.pnProgress.TabIndex = 1;
            this.pnProgress.Visible = false;
            // 
            // MandelbrotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pnProgress);
            this.DoubleBuffered = true;
            this.Name = "MandelbrotControl";
            this.Size = new System.Drawing.Size(752, 518);
            this.pnProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar calculationProgressBar;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.Panel pnProgress;
    }
}
