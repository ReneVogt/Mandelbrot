
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
            this.favoritesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbFavorites = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemManageFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonImages = new System.Windows.Forms.ImageList(this.components);
            this.imageFormatsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pgCurrentScope = new System.Windows.Forms.PropertyGrid();
            this.btResetScope = new System.Windows.Forms.Button();
            this.btApplyScope = new System.Windows.Forms.Button();
            this.cbAdjustAxes = new System.Windows.Forms.CheckBox();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevioius = new System.Windows.Forms.Button();
            this.btStartScreen = new System.Windows.Forms.Button();
            this.pgCurrentSelction = new System.Windows.Forms.PropertyGrid();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbProgress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pgCalculationSettings = new System.Windows.Forms.PropertyGrid();
            this.btResetCalculationSettings = new System.Windows.Forms.Button();
            this.btApplyCalculationSettings = new System.Windows.Forms.Button();
            this.buttonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btExit = new System.Windows.Forms.Button();
            this.exCalculationProgress = new WindowsFormsExpander.Expander();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.exCalculationSettings = new WindowsFormsExpander.Expander();
            this.exCurrentScope = new WindowsFormsExpander.Expander();
            this.exCurrentSelection = new WindowsFormsExpander.Expander();
            this.btFavorites = new Mandelbrot.Controls.DropDownButton();
            this.btSave = new Mandelbrot.Controls.DropDownButton();
            bmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            iconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            emfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            jpegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            wmfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoritesMenu.SuspendLayout();
            this.imageFormatsMenu.SuspendLayout();
            this.exCalculationProgress.SuspendLayout();
            this.exCalculationSettings.SuspendLayout();
            this.exCurrentScope.SuspendLayout();
            this.exCurrentSelection.SuspendLayout();
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
            this.pbProgress.Location = new System.Drawing.Point(19, 49);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(344, 23);
            this.pbProgress.TabIndex = 30;
            // 
            // favoritesMenu
            // 
            this.favoritesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbFavorites,
            this.toolStripMenuItem1,
            this.menuItemAddToFavorites,
            this.menuItemManageFavorites});
            this.favoritesMenu.Name = "favoritesMenu";
            this.favoritesMenu.Size = new System.Drawing.Size(182, 81);
            // 
            // cmbFavorites
            // 
            this.cmbFavorites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFavorites.Name = "cmbFavorites";
            this.cmbFavorites.Size = new System.Drawing.Size(121, 23);
            this.cmbFavorites.Sorted = true;
            this.cmbFavorites.ToolTipText = "Select bookmarked scope";
            this.cmbFavorites.SelectedIndexChanged += new System.EventHandler(this.cmbFavorites_SelectedIndexChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // menuItemAddToFavorites
            // 
            this.menuItemAddToFavorites.Image = global::Mandelbrot.Properties.Resources.add;
            this.menuItemAddToFavorites.Name = "menuItemAddToFavorites";
            this.menuItemAddToFavorites.Size = new System.Drawing.Size(181, 22);
            this.menuItemAddToFavorites.Text = "Add...";
            this.menuItemAddToFavorites.ToolTipText = "Add current scope to bookmarks...";
            this.menuItemAddToFavorites.Click += new System.EventHandler(this.menuItemAddToFavorites_Click);
            // 
            // menuItemManageFavorites
            // 
            this.menuItemManageFavorites.Image = global::Mandelbrot.Properties.Resources.manage;
            this.menuItemManageFavorites.Name = "menuItemManageFavorites";
            this.menuItemManageFavorites.Size = new System.Drawing.Size(181, 22);
            this.menuItemManageFavorites.Text = "Manage...";
            this.menuItemManageFavorites.ToolTipText = "Manage bookmarks...";
            this.menuItemManageFavorites.Click += new System.EventHandler(this.menuItemManageFavorites_Click);
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
            this.buttonImages.Images.SetKeyName(9, "add.png");
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
            // pgCurrentScope
            // 
            this.pgCurrentScope.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCurrentScope.HelpVisible = false;
            this.pgCurrentScope.Location = new System.Drawing.Point(19, 33);
            this.pgCurrentScope.Name = "pgCurrentScope";
            this.pgCurrentScope.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCurrentScope.Size = new System.Drawing.Size(343, 185);
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
            this.btResetScope.Location = new System.Drawing.Point(368, 63);
            this.btResetScope.Name = "btResetScope";
            this.btResetScope.Size = new System.Drawing.Size(24, 24);
            this.btResetScope.TabIndex = 20;
            this.buttonToolTip.SetToolTip(this.btResetScope, "Reset changes");
            this.btResetScope.UseVisualStyleBackColor = true;
            this.btResetScope.Click += new System.EventHandler(this.btResetScope_Click);
            // 
            // btApplyScope
            // 
            this.btApplyScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyScope.Enabled = false;
            this.btApplyScope.ImageKey = "apply";
            this.btApplyScope.ImageList = this.buttonImages;
            this.btApplyScope.Location = new System.Drawing.Point(368, 33);
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
            this.cbAdjustAxes.ContextMenuStrip = this.imageFormatsMenu;
            this.cbAdjustAxes.ImageKey = "adjustaxes";
            this.cbAdjustAxes.ImageList = this.buttonImages;
            this.cbAdjustAxes.Location = new System.Drawing.Point(49, 224);
            this.cbAdjustAxes.Name = "cbAdjustAxes";
            this.cbAdjustAxes.Size = new System.Drawing.Size(24, 24);
            this.cbAdjustAxes.TabIndex = 70;
            this.buttonToolTip.SetToolTip(this.cbAdjustAxes, "Adjust axes");
            this.cbAdjustAxes.UseVisualStyleBackColor = true;
            this.cbAdjustAxes.CheckedChanged += new System.EventHandler(this.cbAdjustAxes_CheckedChanged);
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullscreen.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbFullscreen.ImageKey = "fullscreen";
            this.cbFullscreen.ImageList = this.buttonImages;
            this.cbFullscreen.Location = new System.Drawing.Point(19, 224);
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
            this.btNext.Location = new System.Drawing.Point(368, 123);
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
            this.btPrevioius.Location = new System.Drawing.Point(368, 93);
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
            this.btStartScreen.Location = new System.Drawing.Point(368, 153);
            this.btStartScreen.Name = "btStartScreen";
            this.btStartScreen.Size = new System.Drawing.Size(24, 24);
            this.btStartScreen.TabIndex = 50;
            this.buttonToolTip.SetToolTip(this.btStartScreen, "Total view");
            this.btStartScreen.UseVisualStyleBackColor = true;
            this.btStartScreen.Click += new System.EventHandler(this.btStartScreen_Click);
            // 
            // pgCurrentSelction
            // 
            this.pgCurrentSelction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCurrentSelction.HelpVisible = false;
            this.pgCurrentSelction.Location = new System.Drawing.Point(19, 33);
            this.pgCurrentSelction.Name = "pgCurrentSelction";
            this.pgCurrentSelction.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCurrentSelction.Size = new System.Drawing.Size(343, 112);
            this.pgCurrentSelction.TabIndex = 1;
            this.pgCurrentSelction.ToolbarVisible = false;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Enabled = false;
            this.btCancel.ImageKey = "cancel";
            this.btCancel.ImageList = this.buttonImages;
            this.btCancel.Location = new System.Drawing.Point(368, 49);
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
            this.lbProgress.Location = new System.Drawing.Point(298, 33);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(65, 13);
            this.lbProgress.TabIndex = 33;
            this.lbProgress.Text = "42%";
            this.lbProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Elapsed time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgCalculationSettings
            // 
            this.pgCalculationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgCalculationSettings.HelpVisible = false;
            this.pgCalculationSettings.Location = new System.Drawing.Point(19, 33);
            this.pgCalculationSettings.Name = "pgCalculationSettings";
            this.pgCalculationSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgCalculationSettings.Size = new System.Drawing.Size(343, 80);
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
            this.btResetCalculationSettings.Location = new System.Drawing.Point(367, 63);
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
            this.btApplyCalculationSettings.Location = new System.Drawing.Point(367, 33);
            this.btApplyCalculationSettings.Name = "btApplyCalculationSettings";
            this.btApplyCalculationSettings.Size = new System.Drawing.Size(24, 24);
            this.btApplyCalculationSettings.TabIndex = 10;
            this.buttonToolTip.SetToolTip(this.btApplyCalculationSettings, "Apply changes");
            this.btApplyCalculationSettings.UseVisualStyleBackColor = true;
            this.btApplyCalculationSettings.Click += new System.EventHandler(this.btApplyCalculationSettings_Click);
            // 
            // btExit
            // 
            this.btExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btExit.Location = new System.Drawing.Point(0, 627);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(398, 23);
            this.btExit.TabIndex = 40;
            this.btExit.Text = "Exit Mandelbrot viewer";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // exCalculationProgress
            // 
            this.exCalculationProgress.Controls.Add(this.lbElapsed);
            this.exCalculationProgress.Controls.Add(this.btCancel);
            this.exCalculationProgress.Controls.Add(this.label4);
            this.exCalculationProgress.Controls.Add(this.lbProgress);
            this.exCalculationProgress.Controls.Add(this.pbProgress);
            this.exCalculationProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.exCalculationProgress.ExpandedHeight = 90;
            this.exCalculationProgress.Location = new System.Drawing.Point(0, 0);
            this.exCalculationProgress.Margin = new System.Windows.Forms.Padding(0);
            this.exCalculationProgress.Name = "exCalculationProgress";
            this.exCalculationProgress.Padding = new System.Windows.Forms.Padding(0);
            this.exCalculationProgress.Size = new System.Drawing.Size(398, 90);
            this.exCalculationProgress.TabIndex = 0;
            this.exCalculationProgress.Text = "Calculation progress";
            // 
            // lbElapsed
            // 
            this.lbElapsed.AutoSize = true;
            this.lbElapsed.Location = new System.Drawing.Point(92, 33);
            this.lbElapsed.Name = "lbElapsed";
            this.lbElapsed.Size = new System.Drawing.Size(10, 13);
            this.lbElapsed.TabIndex = 36;
            this.lbElapsed.Text = "-";
            // 
            // exCalculationSettings
            // 
            this.exCalculationSettings.Controls.Add(this.pgCalculationSettings);
            this.exCalculationSettings.Controls.Add(this.btResetCalculationSettings);
            this.exCalculationSettings.Controls.Add(this.btApplyCalculationSettings);
            this.exCalculationSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.exCalculationSettings.ExpandedHeight = 123;
            this.exCalculationSettings.Location = new System.Drawing.Point(0, 90);
            this.exCalculationSettings.Margin = new System.Windows.Forms.Padding(0);
            this.exCalculationSettings.Name = "exCalculationSettings";
            this.exCalculationSettings.Padding = new System.Windows.Forms.Padding(0);
            this.exCalculationSettings.Size = new System.Drawing.Size(398, 123);
            this.exCalculationSettings.TabIndex = 10;
            this.exCalculationSettings.Text = "Calculation settings";
            // 
            // exCurrentScope
            // 
            this.exCurrentScope.Controls.Add(this.btFavorites);
            this.exCurrentScope.Controls.Add(this.pgCurrentScope);
            this.exCurrentScope.Controls.Add(this.btSave);
            this.exCurrentScope.Controls.Add(this.btStartScreen);
            this.exCurrentScope.Controls.Add(this.btPrevioius);
            this.exCurrentScope.Controls.Add(this.btResetScope);
            this.exCurrentScope.Controls.Add(this.btNext);
            this.exCurrentScope.Controls.Add(this.btApplyScope);
            this.exCurrentScope.Controls.Add(this.cbFullscreen);
            this.exCurrentScope.Controls.Add(this.cbAdjustAxes);
            this.exCurrentScope.Dock = System.Windows.Forms.DockStyle.Top;
            this.exCurrentScope.ExpandedHeight = 256;
            this.exCurrentScope.Location = new System.Drawing.Point(0, 213);
            this.exCurrentScope.Margin = new System.Windows.Forms.Padding(0);
            this.exCurrentScope.Name = "exCurrentScope";
            this.exCurrentScope.Padding = new System.Windows.Forms.Padding(0);
            this.exCurrentScope.Size = new System.Drawing.Size(398, 256);
            this.exCurrentScope.TabIndex = 20;
            this.exCurrentScope.Text = "Current scope";
            // 
            // exCurrentSelection
            // 
            this.exCurrentSelection.Controls.Add(this.pgCurrentSelction);
            this.exCurrentSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.exCurrentSelection.ExpandedHeight = 158;
            this.exCurrentSelection.Location = new System.Drawing.Point(0, 469);
            this.exCurrentSelection.Margin = new System.Windows.Forms.Padding(0);
            this.exCurrentSelection.Name = "exCurrentSelection";
            this.exCurrentSelection.Padding = new System.Windows.Forms.Padding(0);
            this.exCurrentSelection.Size = new System.Drawing.Size(398, 158);
            this.exCurrentSelection.TabIndex = 30;
            this.exCurrentSelection.Text = "Current selection";
            // 
            // btFavorites
            // 
            this.btFavorites.DropDownMenu = this.favoritesMenu;
            this.btFavorites.ImageKey = "favorites";
            this.btFavorites.ImageList = this.buttonImages;
            this.btFavorites.Location = new System.Drawing.Point(79, 224);
            this.btFavorites.Name = "btFavorites";
            this.btFavorites.Size = new System.Drawing.Size(24, 24);
            this.btFavorites.TabIndex = 80;
            this.buttonToolTip.SetToolTip(this.btFavorites, "Bookmarks...");
            this.btFavorites.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.DropDownMenu = this.imageFormatsMenu;
            this.btSave.ImageKey = "save";
            this.btSave.ImageList = this.buttonImages;
            this.btSave.Location = new System.Drawing.Point(109, 224);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(24, 24);
            this.btSave.TabIndex = 90;
            this.buttonToolTip.SetToolTip(this.btSave, "Save current image as...");
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(398, 654);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.exCurrentSelection);
            this.Controls.Add(this.exCurrentScope);
            this.Controls.Add(this.exCalculationSettings);
            this.Controls.Add(this.exCalculationProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 39);
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Mandelbrot controller";
            this.favoritesMenu.ResumeLayout(false);
            this.imageFormatsMenu.ResumeLayout(false);
            this.exCalculationProgress.ResumeLayout(false);
            this.exCalculationProgress.PerformLayout();
            this.exCalculationSettings.ResumeLayout(false);
            this.exCurrentScope.ResumeLayout(false);
            this.exCurrentScope.PerformLayout();
            this.exCurrentSelection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btStartScreen;
        private System.Windows.Forms.CheckBox cbFullscreen;
        private System.Windows.Forms.Button btPrevioius;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.CheckBox cbAdjustAxes;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Label label4;
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
        private DropDownButton btFavorites;
        private DropDownButton btSave;
        private System.Windows.Forms.ContextMenuStrip favoritesMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddToFavorites;
        private System.Windows.Forms.ToolStripMenuItem menuItemManageFavorites;
        private System.Windows.Forms.ToolStripComboBox cmbFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private WindowsFormsExpander.Expander exCalculationProgress;
        private WindowsFormsExpander.Expander exCalculationSettings;
        private WindowsFormsExpander.Expander exCurrentScope;
        private WindowsFormsExpander.Expander exCurrentSelection;
        private System.Windows.Forms.Label lbElapsed;
    }
}