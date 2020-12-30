
namespace Mandelbrot
{
    partial class ControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbCalculation = new System.Windows.Forms.GroupBox();
            this.nupIterations = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gbCurrentView = new System.Windows.Forms.GroupBox();
            this.lbCurrentImaginary = new System.Windows.Forms.Label();
            this.lbCurrentReal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbCurrentSelection = new System.Windows.Forms.GroupBox();
            this.lbSelectionImaginary = new System.Windows.Forms.Label();
            this.lbSelectionReal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btStartScreen = new System.Windows.Forms.Button();
            this.btAdjustReal = new System.Windows.Forms.Button();
            this.btAdjustImaginary = new System.Windows.Forms.Button();
            this.gbCalculation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupIterations)).BeginInit();
            this.gbCurrentView.SuspendLayout();
            this.gbCurrentSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCalculation
            // 
            this.gbCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalculation.Controls.Add(this.nupIterations);
            this.gbCalculation.Controls.Add(this.label1);
            this.gbCalculation.Location = new System.Drawing.Point(12, 12);
            this.gbCalculation.Name = "gbCalculation";
            this.gbCalculation.Size = new System.Drawing.Size(331, 76);
            this.gbCalculation.TabIndex = 0;
            this.gbCalculation.TabStop = false;
            this.gbCalculation.Text = "Calculation";
            // 
            // nupIterations
            // 
            this.nupIterations.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nupIterations.Location = new System.Drawing.Point(9, 41);
            this.nupIterations.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nupIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupIterations.Name = "nupIterations";
            this.nupIterations.Size = new System.Drawing.Size(78, 20);
            this.nupIterations.TabIndex = 1;
            this.nupIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maximum number of iterations:";
            // 
            // gbCurrentView
            // 
            this.gbCurrentView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentView.Controls.Add(this.lbCurrentImaginary);
            this.gbCurrentView.Controls.Add(this.lbCurrentReal);
            this.gbCurrentView.Controls.Add(this.label9);
            this.gbCurrentView.Controls.Add(this.label2);
            this.gbCurrentView.Location = new System.Drawing.Point(12, 94);
            this.gbCurrentView.Name = "gbCurrentView";
            this.gbCurrentView.Size = new System.Drawing.Size(322, 63);
            this.gbCurrentView.TabIndex = 10;
            this.gbCurrentView.TabStop = false;
            this.gbCurrentView.Text = "Current scope";
            // 
            // lbCurrentImaginary
            // 
            this.lbCurrentImaginary.AutoSize = true;
            this.lbCurrentImaginary.Location = new System.Drawing.Point(31, 39);
            this.lbCurrentImaginary.Name = "lbCurrentImaginary";
            this.lbCurrentImaginary.Size = new System.Drawing.Size(37, 13);
            this.lbCurrentImaginary.TabIndex = 11;
            this.lbCurrentImaginary.Text = "-1 to 1";
            // 
            // lbCurrentReal
            // 
            this.lbCurrentReal.AutoSize = true;
            this.lbCurrentReal.Location = new System.Drawing.Point(31, 26);
            this.lbCurrentReal.Name = "lbCurrentReal";
            this.lbCurrentReal.Size = new System.Drawing.Size(37, 13);
            this.lbCurrentReal.TabIndex = 10;
            this.lbCurrentReal.Text = "-2 to 1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "I:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "R:";
            // 
            // gbCurrentSelection
            // 
            this.gbCurrentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentSelection.Controls.Add(this.lbSelectionImaginary);
            this.gbCurrentSelection.Controls.Add(this.lbSelectionReal);
            this.gbCurrentSelection.Controls.Add(this.label5);
            this.gbCurrentSelection.Controls.Add(this.label6);
            this.gbCurrentSelection.Location = new System.Drawing.Point(12, 163);
            this.gbCurrentSelection.Name = "gbCurrentSelection";
            this.gbCurrentSelection.Size = new System.Drawing.Size(322, 63);
            this.gbCurrentSelection.TabIndex = 20;
            this.gbCurrentSelection.TabStop = false;
            this.gbCurrentSelection.Text = "Current selection";
            // 
            // lbSelectionImaginary
            // 
            this.lbSelectionImaginary.AutoSize = true;
            this.lbSelectionImaginary.Location = new System.Drawing.Point(31, 39);
            this.lbSelectionImaginary.Name = "lbSelectionImaginary";
            this.lbSelectionImaginary.Size = new System.Drawing.Size(37, 13);
            this.lbSelectionImaginary.TabIndex = 11;
            this.lbSelectionImaginary.Text = "-1 to 1";
            // 
            // lbSelectionReal
            // 
            this.lbSelectionReal.AutoSize = true;
            this.lbSelectionReal.Location = new System.Drawing.Point(31, 26);
            this.lbSelectionReal.Name = "lbSelectionReal";
            this.lbSelectionReal.Size = new System.Drawing.Size(37, 13);
            this.lbSelectionReal.TabIndex = 10;
            this.lbSelectionReal.Text = "-2 to 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "I:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "R:";
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.Location = new System.Drawing.Point(21, 321);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(322, 23);
            this.btRefresh.TabIndex = 60;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btStartScreen
            // 
            this.btStartScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartScreen.Location = new System.Drawing.Point(21, 292);
            this.btStartScreen.Name = "btStartScreen";
            this.btStartScreen.Size = new System.Drawing.Size(322, 23);
            this.btStartScreen.TabIndex = 50;
            this.btStartScreen.Text = "Return to total view";
            this.btStartScreen.UseVisualStyleBackColor = true;
            this.btStartScreen.Click += new System.EventHandler(this.btStartScreen_Click);
            // 
            // btAdjustReal
            // 
            this.btAdjustReal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdjustReal.Location = new System.Drawing.Point(21, 263);
            this.btAdjustReal.Name = "btAdjustReal";
            this.btAdjustReal.Size = new System.Drawing.Size(322, 23);
            this.btAdjustReal.TabIndex = 40;
            this.btAdjustReal.Text = "Adjust real axis";
            this.btAdjustReal.UseVisualStyleBackColor = true;
            this.btAdjustReal.Click += new System.EventHandler(this.btAdjustReal_Click);
            // 
            // btAdjustImaginary
            // 
            this.btAdjustImaginary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdjustImaginary.Location = new System.Drawing.Point(21, 234);
            this.btAdjustImaginary.Name = "btAdjustImaginary";
            this.btAdjustImaginary.Size = new System.Drawing.Size(322, 23);
            this.btAdjustImaginary.TabIndex = 30;
            this.btAdjustImaginary.Text = "Adjust imaginary axis";
            this.btAdjustImaginary.UseVisualStyleBackColor = true;
            this.btAdjustImaginary.Click += new System.EventHandler(this.btAdjustImaginary_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 353);
            this.Controls.Add(this.btAdjustImaginary);
            this.Controls.Add(this.btAdjustReal);
            this.Controls.Add(this.btStartScreen);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.gbCurrentSelection);
            this.Controls.Add(this.gbCurrentView);
            this.Controls.Add(this.gbCalculation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mandelbrot controller";
            this.TopMost = true;
            this.gbCalculation.ResumeLayout(false);
            this.gbCalculation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupIterations)).EndInit();
            this.gbCurrentView.ResumeLayout(false);
            this.gbCurrentView.PerformLayout();
            this.gbCurrentSelection.ResumeLayout(false);
            this.gbCurrentSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCalculation;
        private System.Windows.Forms.NumericUpDown nupIterations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbCurrentView;
        private System.Windows.Forms.Label lbCurrentImaginary;
        private System.Windows.Forms.Label lbCurrentReal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbCurrentSelection;
        private System.Windows.Forms.Label lbSelectionImaginary;
        private System.Windows.Forms.Label lbSelectionReal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btStartScreen;
        private System.Windows.Forms.Button btAdjustReal;
        private System.Windows.Forms.Button btAdjustImaginary;
    }
}