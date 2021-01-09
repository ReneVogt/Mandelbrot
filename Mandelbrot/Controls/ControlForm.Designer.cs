
namespace Mandelbrot.Controls
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
            System.Windows.Forms.ToolStripMenuItem bmpToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem exifToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem gifToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem iconToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem emfToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem jpegToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem pngToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem tiffToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem wmfToolStripMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlForm));
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.gbCurrentView = new System.Windows.Forms.GroupBox();
            this.pgCurrentScope = new System.Windows.Forms.PropertyGrid();
            this.btResetScope = new System.Windows.Forms.Button();
            this.buttonImages = new System.Windows.Forms.ImageList(this.components);
            this.btApplyScope = new System.Windows.Forms.Button();
            this.cbAdjustAxes = new System.Windows.Forms.CheckBox();
            this.btSave = new System.Windows.Forms.Button();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevioius = new System.Windows.Forms.Button();
            this.btStartScreen = new System.Windows.Forms.Button();
            this.gbCurrentSelection = new System.Windows.Forms.GroupBox();
            this.pgCurrentSelction = new System.Windows.Forms.PropertyGrid();
            this.gbCalculationProgress = new System.Windows.Forms.GroupBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbProgress = new System.Windows.Forms.Label();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbCalculationSettings = new System.Windows.Forms.GroupBox();
            this.pgCalculationSettings = new System.Windows.Forms.PropertyGrid();
            this.btResetCalculationSettings = new System.Windows.Forms.Button();
            this.btApplyCalculationSettings = new System.Windows.Forms.Button();
            this.buttonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btExit = new System.Windows.Forms.Button();
            this.imageFormatsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            bmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            iconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            emfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            jpegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            wmfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbCurrentView.SuspendLayout();
            this.gbCurrentSelection.SuspendLayout();
            this.gbCalculationProgress.SuspendLayout();
            this.gbCalculationSettings.SuspendLayout();
            this.imageFormatsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bmpToolStripMenuItem
            // 
            bmpToolStripMenuItem.Name = "bmpToolStripMenuItem";
            bmpToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            bmpToolStripMenuItem.Text = "Bmp";
            bmpToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // exifToolStripMenuItem
            // 
            exifToolStripMenuItem.Name = "exifToolStripMenuItem";
            exifToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            exifToolStripMenuItem.Text = "Exif";
            exifToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // gifToolStripMenuItem
            // 
            gifToolStripMenuItem.Name = "gifToolStripMenuItem";
            gifToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            gifToolStripMenuItem.Text = "Gif";
            gifToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // iconToolStripMenuItem
            // 
            iconToolStripMenuItem.Name = "iconToolStripMenuItem";
            iconToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            iconToolStripMenuItem.Text = "Icon";
            iconToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // emfToolStripMenuItem
            // 
            emfToolStripMenuItem.Name = "emfToolStripMenuItem";
            emfToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            emfToolStripMenuItem.Text = "Emf";
            emfToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // jpegToolStripMenuItem
            // 
            jpegToolStripMenuItem.Name = "jpegToolStripMenuItem";
            jpegToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            jpegToolStripMenuItem.Text = "Jpeg";
            jpegToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // pngToolStripMenuItem
            // 
            pngToolStripMenuItem.Name = "pngToolStripMenuItem";
            pngToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            pngToolStripMenuItem.Text = "Png";
            pngToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // tiffToolStripMenuItem
            // 
            tiffToolStripMenuItem.Name = "tiffToolStripMenuItem";
            tiffToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            tiffToolStripMenuItem.Text = "Tiff";
            tiffToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // wmfToolStripMenuItem
            // 
            wmfToolStripMenuItem.Name = "wmfToolStripMenuItem";
            wmfToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            wmfToolStripMenuItem.Text = "Wmf";
            wmfToolStripMenuItem.Click += new System.EventHandler(this.OnImageFormatClicked);
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(9, 39);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(320, 23);
            this.pbProgress.TabIndex = 30;
            // 
            // gbCurrentView
            // 
            this.gbCurrentView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentView.Controls.Add(this.pgCurrentScope);
            this.gbCurrentView.Controls.Add(this.btResetScope);
            this.gbCurrentView.Controls.Add(this.btApplyScope);
            this.gbCurrentView.Controls.Add(this.cbAdjustAxes);
            this.gbCurrentView.Controls.Add(this.btSave);
            this.gbCurrentView.Controls.Add(this.cbFullscreen);
            this.gbCurrentView.Controls.Add(this.btNext);
            this.gbCurrentView.Controls.Add(this.btPrevioius);
            this.gbCurrentView.Controls.Add(this.btStartScreen);
            this.gbCurrentView.Location = new System.Drawing.Point(12, 195);
            this.gbCurrentView.Name = "gbCurrentView";
            this.gbCurrentView.Size = new System.Drawing.Size(364, 211);
            this.gbCurrentView.TabIndex = 20;
            this.gbCurrentView.TabStop = false;
            this.gbCurrentView.Text = "Current scope";
            // 
            // pgCurrentScope
            // 
            this.pgCurrentScope.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCurrentScope.HelpVisible = false;
            this.pgCurrentScope.Location = new System.Drawing.Point(9, 19);
            this.pgCurrentScope.Name = "pgCurrentScope";
            this.pgCurrentScope.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCurrentScope.Size = new System.Drawing.Size(320, 156);
            this.pgCurrentScope.TabIndex = 0;
            this.pgCurrentScope.ToolbarVisible = false;
            this.pgCurrentScope.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgCurrentScope_PropertyValueChanged);
            // 
            // btResetScope
            // 
            this.btResetScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btResetScope.Enabled = false;
            this.btResetScope.ImageKey = "cancel";
            this.btResetScope.ImageList = this.buttonImages;
            this.btResetScope.Location = new System.Drawing.Point(335, 48);
            this.btResetScope.Name = "btResetScope";
            this.btResetScope.Size = new System.Drawing.Size(24, 24);
            this.btResetScope.TabIndex = 20;
            this.buttonToolTip.SetToolTip(this.btResetScope, "Reset changes");
            this.btResetScope.UseVisualStyleBackColor = true;
            this.btResetScope.Click += new System.EventHandler(this.btResetScope_Click);
            // 
            // buttonImages
            // 
            this.buttonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonImages.ImageStream")));
            this.buttonImages.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonImages.Images.SetKeyName(0, "apply");
            this.buttonImages.Images.SetKeyName(1, "cancel");
            this.buttonImages.Images.SetKeyName(2, "Mandelbrot");
            this.buttonImages.Images.SetKeyName(3, "fullscreen");
            this.buttonImages.Images.SetKeyName(4, "adjustaxes");
            this.buttonImages.Images.SetKeyName(5, "save");
            this.buttonImages.Images.SetKeyName(6, "next");
            this.buttonImages.Images.SetKeyName(7, "previous");
            this.buttonImages.Images.SetKeyName(8, "favorites");
            // 
            // btApplyScope
            // 
            this.btApplyScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyScope.Enabled = false;
            this.btApplyScope.ImageKey = "apply";
            this.btApplyScope.ImageList = this.buttonImages;
            this.btApplyScope.Location = new System.Drawing.Point(335, 18);
            this.btApplyScope.Name = "btApplyScope";
            this.btApplyScope.Size = new System.Drawing.Size(24, 24);
            this.btApplyScope.TabIndex = 10;
            this.buttonToolTip.SetToolTip(this.btApplyScope, "Apply changes");
            this.btApplyScope.UseVisualStyleBackColor = true;
            this.btApplyScope.Click += new System.EventHandler(this.btApplyScope_Click);
            // 
            // cbAdjustAxes
            // 
            this.cbAdjustAxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAdjustAxes.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbAdjustAxes.ImageKey = "adjustaxes";
            this.cbAdjustAxes.ImageList = this.buttonImages;
            this.cbAdjustAxes.Location = new System.Drawing.Point(39, 181);
            this.cbAdjustAxes.Name = "cbAdjustAxes";
            this.cbAdjustAxes.Size = new System.Drawing.Size(24, 24);
            this.cbAdjustAxes.TabIndex = 70;
            this.buttonToolTip.SetToolTip(this.cbAdjustAxes, "Adjust axes");
            this.cbAdjustAxes.UseVisualStyleBackColor = true;
            this.cbAdjustAxes.CheckedChanged += new System.EventHandler(this.cbAdjustAxes_CheckedChanged);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.ImageKey = "save";
            this.btSave.ImageList = this.buttonImages;
            this.btSave.Location = new System.Drawing.Point(69, 181);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(24, 24);
            this.btSave.TabIndex = 100;
            this.buttonToolTip.SetToolTip(this.btSave, "Save image with selected format");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullscreen.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbFullscreen.ImageKey = "fullscreen";
            this.cbFullscreen.ImageList = this.buttonImages;
            this.cbFullscreen.Location = new System.Drawing.Point(9, 181);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(24, 24);
            this.cbFullscreen.TabIndex = 60;
            this.cbFullscreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonToolTip.SetToolTip(this.cbFullscreen, "Toggle fullscreen mode");
            this.cbFullscreen.UseVisualStyleBackColor = true;
            this.cbFullscreen.CheckedChanged += new System.EventHandler(this.cbFullscreen_CheckedChanged);
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.Enabled = false;
            this.btNext.ImageKey = "next";
            this.btNext.ImageList = this.buttonImages;
            this.btNext.Location = new System.Drawing.Point(335, 108);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(24, 24);
            this.btNext.TabIndex = 40;
            this.buttonToolTip.SetToolTip(this.btNext, "Next scope");
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevioius
            // 
            this.btPrevioius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrevioius.Enabled = false;
            this.btPrevioius.ImageKey = "previous";
            this.btPrevioius.ImageList = this.buttonImages;
            this.btPrevioius.Location = new System.Drawing.Point(335, 78);
            this.btPrevioius.Name = "btPrevioius";
            this.btPrevioius.Size = new System.Drawing.Size(24, 24);
            this.btPrevioius.TabIndex = 30;
            this.buttonToolTip.SetToolTip(this.btPrevioius, "Previous scope");
            this.btPrevioius.UseVisualStyleBackColor = true;
            this.btPrevioius.Click += new System.EventHandler(this.btPrevioius_Click);
            // 
            // btStartScreen
            // 
            this.btStartScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartScreen.AutoSize = true;
            this.btStartScreen.ImageKey = "Mandelbrot";
            this.btStartScreen.ImageList = this.buttonImages;
            this.btStartScreen.Location = new System.Drawing.Point(335, 138);
            this.btStartScreen.Name = "btStartScreen";
            this.btStartScreen.Size = new System.Drawing.Size(24, 24);
            this.btStartScreen.TabIndex = 50;
            this.buttonToolTip.SetToolTip(this.btStartScreen, "Total view");
            this.btStartScreen.UseVisualStyleBackColor = true;
            this.btStartScreen.Click += new System.EventHandler(this.btStartScreen_Click);
            // 
            // gbCurrentSelection
            // 
            this.gbCurrentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCurrentSelection.Controls.Add(this.pgCurrentSelction);
            this.gbCurrentSelection.Location = new System.Drawing.Point(12, 412);
            this.gbCurrentSelection.Name = "gbCurrentSelection";
            this.gbCurrentSelection.Size = new System.Drawing.Size(364, 137);
            this.gbCurrentSelection.TabIndex = 40;
            this.gbCurrentSelection.TabStop = false;
            this.gbCurrentSelection.Text = "Current selection";
            // 
            // pgCurrentSelction
            // 
            this.pgCurrentSelction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCurrentSelction.HelpVisible = false;
            this.pgCurrentSelction.Location = new System.Drawing.Point(9, 19);
            this.pgCurrentSelction.Name = "pgCurrentSelction";
            this.pgCurrentSelction.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCurrentSelction.Size = new System.Drawing.Size(320, 112);
            this.pgCurrentSelction.TabIndex = 1;
            this.pgCurrentSelction.ToolbarVisible = false;
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
            this.gbCalculationProgress.Size = new System.Drawing.Size(364, 73);
            this.gbCalculationProgress.TabIndex = 0;
            this.gbCalculationProgress.TabStop = false;
            this.gbCalculationProgress.Text = "Calculation progress";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Enabled = false;
            this.btCancel.ImageKey = "cancel";
            this.btCancel.ImageList = this.buttonImages;
            this.btCancel.Location = new System.Drawing.Point(335, 39);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(24, 24);
            this.btCancel.TabIndex = 35;
            this.buttonToolTip.SetToolTip(this.btCancel, "Cancel");
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lbProgress
            // 
            this.lbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgress.Location = new System.Drawing.Point(264, 23);
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
            this.gbCalculationSettings.Location = new System.Drawing.Point(12, 91);
            this.gbCalculationSettings.Name = "gbCalculationSettings";
            this.gbCalculationSettings.Size = new System.Drawing.Size(364, 98);
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
            this.pgCalculationSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCalculationSettings.Size = new System.Drawing.Size(320, 64);
            this.pgCalculationSettings.TabIndex = 0;
            this.pgCalculationSettings.ToolbarVisible = false;
            this.pgCalculationSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgCalculationSettings_PropertyValueChanged);
            // 
            // btResetCalculationSettings
            // 
            this.btResetCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btResetCalculationSettings.Enabled = false;
            this.btResetCalculationSettings.ImageKey = "cancel";
            this.btResetCalculationSettings.ImageList = this.buttonImages;
            this.btResetCalculationSettings.Location = new System.Drawing.Point(334, 48);
            this.btResetCalculationSettings.Name = "btResetCalculationSettings";
            this.btResetCalculationSettings.Size = new System.Drawing.Size(24, 24);
            this.btResetCalculationSettings.TabIndex = 20;
            this.buttonToolTip.SetToolTip(this.btResetCalculationSettings, "Reset changes");
            this.btResetCalculationSettings.UseVisualStyleBackColor = true;
            this.btResetCalculationSettings.Click += new System.EventHandler(this.btResetCalculationSettings_Click);
            // 
            // btApplyCalculationSettings
            // 
            this.btApplyCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyCalculationSettings.Enabled = false;
            this.btApplyCalculationSettings.ImageKey = "apply";
            this.btApplyCalculationSettings.ImageList = this.buttonImages;
            this.btApplyCalculationSettings.Location = new System.Drawing.Point(334, 18);
            this.btApplyCalculationSettings.Name = "btApplyCalculationSettings";
            this.btApplyCalculationSettings.Size = new System.Drawing.Size(24, 24);
            this.btApplyCalculationSettings.TabIndex = 10;
            this.buttonToolTip.SetToolTip(this.btApplyCalculationSettings, "Apply changes");
            this.btApplyCalculationSettings.UseVisualStyleBackColor = true;
            this.btApplyCalculationSettings.Click += new System.EventHandler(this.btApplyCalculationSettings_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Location = new System.Drawing.Point(12, 556);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(364, 23);
            this.btExit.TabIndex = 50;
            this.btExit.Text = "Exit Mandelbrot viewer";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // imageFormatsMenu
            // 
            this.imageFormatsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            bmpToolStripMenuItem,
            emfToolStripMenuItem,
            exifToolStripMenuItem,
            gifToolStripMenuItem,
            iconToolStripMenuItem,
            jpegToolStripMenuItem,
            pngToolStripMenuItem,
            tiffToolStripMenuItem,
            wmfToolStripMenuItem});
            this.imageFormatsMenu.Name = "imageFormatsMenu";
            this.imageFormatsMenu.Size = new System.Drawing.Size(101, 202);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 591);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.gbCalculationSettings);
            this.Controls.Add(this.gbCalculationProgress);
            this.Controls.Add(this.gbCurrentSelection);
            this.Controls.Add(this.gbCurrentView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(405, 630);
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mandelbrot controller";
            this.gbCurrentView.ResumeLayout(false);
            this.gbCurrentView.PerformLayout();
            this.gbCurrentSelection.ResumeLayout(false);
            this.gbCalculationProgress.ResumeLayout(false);
            this.gbCalculationProgress.PerformLayout();
            this.gbCalculationSettings.ResumeLayout(false);
            this.imageFormatsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbCurrentView;
        private System.Windows.Forms.GroupBox gbCurrentSelection;
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
        private System.Windows.Forms.GroupBox gbCalculationSettings;
        private System.Windows.Forms.Button btResetCalculationSettings;
        private System.Windows.Forms.Button btApplyCalculationSettings;
        private System.Windows.Forms.PropertyGrid pgCalculationSettings;
        private System.Windows.Forms.ImageList buttonImages;
        private System.Windows.Forms.ToolTip buttonToolTip;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.PropertyGrid pgCurrentScope;
        private System.Windows.Forms.Button btResetScope;
        private System.Windows.Forms.Button btApplyScope;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ContextMenuStrip imageFormatsMenu;
        private System.Windows.Forms.PropertyGrid pgCurrentSelction;
    }
}