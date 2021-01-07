﻿
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlForm));
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.gbCurrentView = new System.Windows.Forms.GroupBox();
            this.cbAdjustAxes = new System.Windows.Forms.CheckBox();
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
            this.gbCalculationProgress = new System.Windows.Forms.GroupBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbProgress = new System.Windows.Forms.Label();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbCalculationSettings = new System.Windows.Forms.GroupBox();
            this.pgCalculationSettings = new System.Windows.Forms.PropertyGrid();
            this.btResetCalculationSettings = new System.Windows.Forms.Button();
            this.buttonImages = new System.Windows.Forms.ImageList(this.components);
            this.btApplyCalculationSettings = new System.Windows.Forms.Button();
            this.buttonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbBookmarks = new System.Windows.Forms.GroupBox();
            this.btExit = new System.Windows.Forms.Button();
            this.gbCurrentView.SuspendLayout();
            this.gbCurrentSelection.SuspendLayout();
            this.gbCalculationProgress.SuspendLayout();
            this.gbCalculationSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(9, 39);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(427, 23);
            this.pbProgress.TabIndex = 30;
            // 
            // gbCurrentView
            // 
            this.gbCurrentView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentView.Controls.Add(this.cbAdjustAxes);
            this.gbCurrentView.Controls.Add(this.btSave);
            this.gbCurrentView.Controls.Add(this.cbFullscreen);
            this.gbCurrentView.Controls.Add(this.lbCurrentImaginary);
            this.gbCurrentView.Controls.Add(this.btNext);
            this.gbCurrentView.Controls.Add(this.lbCurrentReal);
            this.gbCurrentView.Controls.Add(this.btPrevioius);
            this.gbCurrentView.Controls.Add(this.label9);
            this.gbCurrentView.Controls.Add(this.label2);
            this.gbCurrentView.Controls.Add(this.btStartScreen);
            this.gbCurrentView.Location = new System.Drawing.Point(12, 241);
            this.gbCurrentView.Name = "gbCurrentView";
            this.gbCurrentView.Size = new System.Drawing.Size(442, 184);
            this.gbCurrentView.TabIndex = 20;
            this.gbCurrentView.TabStop = false;
            this.gbCurrentView.Text = "Current scope";
            // 
            // cbAdjustAxes
            // 
            this.cbAdjustAxes.Location = new System.Drawing.Point(9, 93);
            this.cbAdjustAxes.Name = "cbAdjustAxes";
            this.cbAdjustAxes.Size = new System.Drawing.Size(102, 24);
            this.cbAdjustAxes.TabIndex = 0;
            this.cbAdjustAxes.Text = "Adjust axes";
            this.cbAdjustAxes.UseVisualStyleBackColor = true;
            this.cbAdjustAxes.CheckedChanged += new System.EventHandler(this.cbAdjustAxes_CheckedChanged);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(90, 152);
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
            this.btNext.Location = new System.Drawing.Point(90, 123);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 20;
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
            this.btPrevioius.Location = new System.Drawing.Point(9, 123);
            this.btPrevioius.Name = "btPrevioius";
            this.btPrevioius.Size = new System.Drawing.Size(75, 23);
            this.btPrevioius.TabIndex = 10;
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
            this.btStartScreen.Location = new System.Drawing.Point(9, 152);
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
            this.gbCurrentSelection.Location = new System.Drawing.Point(12, 537);
            this.gbCurrentSelection.Name = "gbCurrentSelection";
            this.gbCurrentSelection.Size = new System.Drawing.Size(442, 63);
            this.gbCurrentSelection.TabIndex = 40;
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
            this.cbFullscreen.Location = new System.Drawing.Point(191, 151);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(208, 24);
            this.cbFullscreen.TabIndex = 70;
            this.cbFullscreen.Text = "Fullscreen";
            this.cbFullscreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbFullscreen.UseVisualStyleBackColor = true;
            this.cbFullscreen.CheckedChanged += new System.EventHandler(this.cbFullscreen_CheckedChanged);
            // 
            // gbCalculationProgress
            // 
            this.gbCalculationProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalculationProgress.Controls.Add(this.btCancel);
            this.gbCalculationProgress.Controls.Add(this.lbProgress);
            this.gbCalculationProgress.Controls.Add(this.lbElapsed);
            this.gbCalculationProgress.Controls.Add(this.label4);
            this.gbCalculationProgress.Controls.Add(this.pbProgress);
            this.gbCalculationProgress.Location = new System.Drawing.Point(12, 12);
            this.gbCalculationProgress.Name = "gbCalculationProgress";
            this.gbCalculationProgress.Size = new System.Drawing.Size(442, 100);
            this.gbCalculationProgress.TabIndex = 0;
            this.gbCalculationProgress.TabStop = false;
            this.gbCalculationProgress.Text = "Calculation progress";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Enabled = false;
            this.btCancel.Location = new System.Drawing.Point(9, 69);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(427, 23);
            this.btCancel.TabIndex = 34;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lbProgress
            // 
            this.lbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgress.Location = new System.Drawing.Point(371, 23);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(65, 13);
            this.lbProgress.TabIndex = 33;
            this.lbProgress.Text = "42%";
            this.lbProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbElapsed
            // 
            this.lbElapsed.AutoSize = true;
            this.lbElapsed.Location = new System.Drawing.Point(73, 23);
            this.lbElapsed.Name = "lbElapsed";
            this.lbElapsed.Size = new System.Drawing.Size(10, 13);
            this.lbElapsed.TabIndex = 32;
            this.lbElapsed.Text = "-";
            this.lbElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Elapsed time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbCalculationSettings
            // 
            this.gbCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalculationSettings.Controls.Add(this.pgCalculationSettings);
            this.gbCalculationSettings.Controls.Add(this.btResetCalculationSettings);
            this.gbCalculationSettings.Controls.Add(this.btApplyCalculationSettings);
            this.gbCalculationSettings.Location = new System.Drawing.Point(12, 118);
            this.gbCalculationSettings.Name = "gbCalculationSettings";
            this.gbCalculationSettings.Size = new System.Drawing.Size(442, 117);
            this.gbCalculationSettings.TabIndex = 10;
            this.gbCalculationSettings.TabStop = false;
            this.gbCalculationSettings.Text = "Calculation settings";
            // 
            // pgCalculationSettings
            // 
            this.pgCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCalculationSettings.HelpVisible = false;
            this.pgCalculationSettings.Location = new System.Drawing.Point(9, 18);
            this.pgCalculationSettings.Name = "pgCalculationSettings";
            this.pgCalculationSettings.Size = new System.Drawing.Size(427, 64);
            this.pgCalculationSettings.TabIndex = 0;
            this.pgCalculationSettings.ToolbarVisible = false;
            this.pgCalculationSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgCalculationSettings_PropertyValueChanged);
            // 
            // btResetCalculationSettings
            // 
            this.btResetCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btResetCalculationSettings.Enabled = false;
            this.btResetCalculationSettings.ImageKey = "cancel";
            this.btResetCalculationSettings.ImageList = this.buttonImages;
            this.btResetCalculationSettings.Location = new System.Drawing.Point(413, 88);
            this.btResetCalculationSettings.Name = "btResetCalculationSettings";
            this.btResetCalculationSettings.Size = new System.Drawing.Size(23, 23);
            this.btResetCalculationSettings.TabIndex = 20;
            this.buttonToolTip.SetToolTip(this.btResetCalculationSettings, "Reset changes");
            this.btResetCalculationSettings.UseVisualStyleBackColor = true;
            this.btResetCalculationSettings.Click += new System.EventHandler(this.btResetCalculationSettings_Click);
            // 
            // buttonImages
            // 
            this.buttonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonImages.ImageStream")));
            this.buttonImages.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonImages.Images.SetKeyName(0, "apply");
            this.buttonImages.Images.SetKeyName(1, "cancel");
            // 
            // btApplyCalculationSettings
            // 
            this.btApplyCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyCalculationSettings.Enabled = false;
            this.btApplyCalculationSettings.ImageKey = "apply";
            this.btApplyCalculationSettings.ImageList = this.buttonImages;
            this.btApplyCalculationSettings.Location = new System.Drawing.Point(387, 88);
            this.btApplyCalculationSettings.Name = "btApplyCalculationSettings";
            this.btApplyCalculationSettings.Size = new System.Drawing.Size(23, 23);
            this.btApplyCalculationSettings.TabIndex = 10;
            this.buttonToolTip.SetToolTip(this.btApplyCalculationSettings, "Apply changes");
            this.btApplyCalculationSettings.UseVisualStyleBackColor = true;
            this.btApplyCalculationSettings.Click += new System.EventHandler(this.btApplyCalculationSettings_Click);
            // 
            // gbBookmarks
            // 
            this.gbBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBookmarks.Location = new System.Drawing.Point(12, 431);
            this.gbBookmarks.Name = "gbBookmarks";
            this.gbBookmarks.Size = new System.Drawing.Size(442, 100);
            this.gbBookmarks.TabIndex = 30;
            this.gbBookmarks.TabStop = false;
            this.gbBookmarks.Text = "Bookmarked scopes";
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Location = new System.Drawing.Point(12, 606);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(442, 23);
            this.btExit.TabIndex = 50;
            this.btExit.Text = "Exit Mandelbrot viewer";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(467, 641);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.gbBookmarks);
            this.Controls.Add(this.gbCalculationSettings);
            this.Controls.Add(this.gbCalculationProgress);
            this.Controls.Add(this.gbCurrentSelection);
            this.Controls.Add(this.gbCurrentView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mandelbrot controller";
            this.gbCurrentView.ResumeLayout(false);
            this.gbCurrentView.PerformLayout();
            this.gbCurrentSelection.ResumeLayout(false);
            this.gbCurrentSelection.PerformLayout();
            this.gbCalculationProgress.ResumeLayout(false);
            this.gbCalculationProgress.PerformLayout();
            this.gbCalculationSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button btStartScreen;
        private System.Windows.Forms.CheckBox cbFullscreen;
        private System.Windows.Forms.Button btPrevioius;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.CheckBox cbAdjustAxes;
        private System.Windows.Forms.GroupBox gbCalculationProgress;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Label lbElapsed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox gbCalculationSettings;
        private System.Windows.Forms.Button btResetCalculationSettings;
        private System.Windows.Forms.Button btApplyCalculationSettings;
        private System.Windows.Forms.PropertyGrid pgCalculationSettings;
        private System.Windows.Forms.ImageList buttonImages;
        private System.Windows.Forms.ToolTip buttonToolTip;
        private System.Windows.Forms.GroupBox gbBookmarks;
        private System.Windows.Forms.Button btExit;
    }
}