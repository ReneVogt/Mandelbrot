
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
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.nupIterations = new Mandelbrot.LogarithmicUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btRecalculate = new System.Windows.Forms.Button();
            this.gbCurrentView = new System.Windows.Forms.GroupBox();
            this.btAdjust = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.lbCurrentImaginary = new System.Windows.Forms.Label();
            this.btNext = new System.Windows.Forms.Button();
            this.lbCurrentReal = new System.Windows.Forms.Label();
            this.btPrevioius = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btStartScreen = new System.Windows.Forms.Button();
            this.gbCurrentSelection = new System.Windows.Forms.GroupBox();
            this.lbSelectionImaginary = new System.Windows.Forms.Label();
            this.lbSelectionReal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbColorizer = new System.Windows.Forms.ComboBox();
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
            this.gbCalculation.Controls.Add(this.cmbColorizer);
            this.gbCalculation.Controls.Add(this.label3);
            this.gbCalculation.Controls.Add(this.pbProgress);
            this.gbCalculation.Controls.Add(this.nupIterations);
            this.gbCalculation.Controls.Add(this.label1);
            this.gbCalculation.Controls.Add(this.btRecalculate);
            this.gbCalculation.Location = new System.Drawing.Point(12, 12);
            this.gbCalculation.Name = "gbCalculation";
            this.gbCalculation.Size = new System.Drawing.Size(331, 133);
            this.gbCalculation.TabIndex = 0;
            this.gbCalculation.TabStop = false;
            this.gbCalculation.Text = "Calculation";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(9, 104);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(315, 23);
            this.pbProgress.TabIndex = 30;
            // 
            // nupIterations
            // 
            this.nupIterations.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nupIterations.Location = new System.Drawing.Point(161, 22);
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
            this.nupIterations.Size = new System.Drawing.Size(163, 20);
            this.nupIterations.TabIndex = 0;
            this.nupIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nupIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maximum number of iterations:";
            // 
            // btRecalculate
            // 
            this.btRecalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRecalculate.Location = new System.Drawing.Point(9, 75);
            this.btRecalculate.Name = "btRecalculate";
            this.btRecalculate.Size = new System.Drawing.Size(315, 23);
            this.btRecalculate.TabIndex = 20;
            this.btRecalculate.Text = "Recalculate";
            this.btRecalculate.UseVisualStyleBackColor = true;
            this.btRecalculate.Click += new System.EventHandler(this.btRecalculate_Click);
            // 
            // gbCurrentView
            // 
            this.gbCurrentView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentView.Controls.Add(this.btAdjust);
            this.gbCurrentView.Controls.Add(this.btSave);
            this.gbCurrentView.Controls.Add(this.lbCurrentImaginary);
            this.gbCurrentView.Controls.Add(this.btNext);
            this.gbCurrentView.Controls.Add(this.lbCurrentReal);
            this.gbCurrentView.Controls.Add(this.btPrevioius);
            this.gbCurrentView.Controls.Add(this.label9);
            this.gbCurrentView.Controls.Add(this.label2);
            this.gbCurrentView.Controls.Add(this.btStartScreen);
            this.gbCurrentView.Location = new System.Drawing.Point(12, 151);
            this.gbCurrentView.Name = "gbCurrentView";
            this.gbCurrentView.Size = new System.Drawing.Size(331, 146);
            this.gbCurrentView.TabIndex = 10;
            this.gbCurrentView.TabStop = false;
            this.gbCurrentView.Text = "Current scope";
            // 
            // btAdjust
            // 
            this.btAdjust.Location = new System.Drawing.Point(9, 89);
            this.btAdjust.Name = "btAdjust";
            this.btAdjust.Size = new System.Drawing.Size(75, 23);
            this.btAdjust.TabIndex = 20;
            this.btAdjust.Text = "Adjust axes";
            this.btAdjust.UseVisualStyleBackColor = true;
            this.btAdjust.Click += new System.EventHandler(this.btAdjust_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(9, 117);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 40;
            this.btSave.Text = "Save...";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
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
            // btNext
            // 
            this.btNext.Enabled = false;
            this.btNext.Location = new System.Drawing.Point(90, 60);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 10;
            this.btNext.Text = ">>";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
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
            // btPrevioius
            // 
            this.btPrevioius.Enabled = false;
            this.btPrevioius.Location = new System.Drawing.Point(9, 60);
            this.btPrevioius.Name = "btPrevioius";
            this.btPrevioius.Size = new System.Drawing.Size(75, 23);
            this.btPrevioius.TabIndex = 0;
            this.btPrevioius.Text = "<<";
            this.btPrevioius.UseVisualStyleBackColor = true;
            this.btPrevioius.Click += new System.EventHandler(this.btPrevioius_Click);
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
            // btStartScreen
            // 
            this.btStartScreen.AutoSize = true;
            this.btStartScreen.Location = new System.Drawing.Point(90, 89);
            this.btStartScreen.Name = "btStartScreen";
            this.btStartScreen.Size = new System.Drawing.Size(75, 23);
            this.btStartScreen.TabIndex = 30;
            this.btStartScreen.Text = "Total";
            this.btStartScreen.UseVisualStyleBackColor = true;
            this.btStartScreen.Click += new System.EventHandler(this.btStartScreen_Click);
            // 
            // gbCurrentSelection
            // 
            this.gbCurrentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentSelection.Controls.Add(this.lbSelectionImaginary);
            this.gbCurrentSelection.Controls.Add(this.lbSelectionReal);
            this.gbCurrentSelection.Controls.Add(this.label5);
            this.gbCurrentSelection.Controls.Add(this.label6);
            this.gbCurrentSelection.Location = new System.Drawing.Point(12, 303);
            this.gbCurrentSelection.Name = "gbCurrentSelection";
            this.gbCurrentSelection.Size = new System.Drawing.Size(331, 63);
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
            // cbFullscreen
            // 
            this.cbFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFullscreen.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbFullscreen.Location = new System.Drawing.Point(12, 372);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(331, 24);
            this.cbFullscreen.TabIndex = 70;
            this.cbFullscreen.Text = "Fullscreen";
            this.cbFullscreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbFullscreen.UseVisualStyleBackColor = true;
            this.cbFullscreen.CheckedChanged += new System.EventHandler(this.cbFullscreen_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Colorization:";
            // 
            // cmbColorizer
            // 
            this.cmbColorizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorizer.FormattingEnabled = true;
            this.cmbColorizer.Items.AddRange(new object[] {
            "Black & White",
            "Based on iteration ratio",
            "Based on iteration round trip"});
            this.cmbColorizer.Location = new System.Drawing.Point(161, 47);
            this.cmbColorizer.Name = "cmbColorizer";
            this.cmbColorizer.Size = new System.Drawing.Size(163, 21);
            this.cmbColorizer.TabIndex = 10;
            // 
            // ControlForm
            // 
            this.AcceptButton = this.btRecalculate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 408);
            this.Controls.Add(this.cbFullscreen);
            this.Controls.Add(this.gbCurrentSelection);
            this.Controls.Add(this.gbCurrentView);
            this.Controls.Add(this.gbCalculation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mandelbrot controller";
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
        LogarithmicUpDown nupIterations;
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
        private System.Windows.Forms.Button btRecalculate;
        private System.Windows.Forms.Button btStartScreen;
        private System.Windows.Forms.CheckBox cbFullscreen;
        private System.Windows.Forms.Button btPrevioius;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Button btAdjust;
        private System.Windows.Forms.ComboBox cmbColorizer;
        private System.Windows.Forms.Label label3;
    }
}