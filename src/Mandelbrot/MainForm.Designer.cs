namespace Mandelbrot
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            glControl = new OpenTK.GLControl.GLControl();
            infoPanel = new Panel();
            label10 = new Label();
            labelPerturbation = new Label();
            label8 = new Label();
            label9 = new Label();
            labelWindowW = new Label();
            labelWindowH = new Label();
            label7 = new Label();
            labelGPU = new Label();
            label5 = new Label();
            label6 = new Label();
            labelZoom = new Label();
            labelIterations = new Label();
            label3 = new Label();
            label4 = new Label();
            labelMouseX = new Label();
            labelMouseY = new Label();
            label1 = new Label();
            label2 = new Label();
            labelCenterX = new Label();
            labelCenterY = new Label();
            infoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // glControl
            // 
            glControl.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl.APIVersion = new Version(3, 3, 0, 0);
            glControl.Dock = DockStyle.Fill;
            glControl.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl.IsEventDriven = true;
            glControl.Location = new Point(0, 0);
            glControl.Margin = new Padding(2);
            glControl.Name = "glControl";
            glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl.SharedContext = null;
            glControl.Size = new Size(1024, 768);
            glControl.TabIndex = 0;
            glControl.Load += OnLoadGL;
            glControl.Paint += OnPaintGL;
            glControl.PreviewKeyDown += OnPreviewKeyDownGL;
            glControl.Resize += OnResizeGL;
            // 
            // infoPanel
            // 
            infoPanel.AutoSize = true;
            infoPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            infoPanel.BackColor = Color.Black;
            infoPanel.Controls.Add(label10);
            infoPanel.Controls.Add(labelPerturbation);
            infoPanel.Controls.Add(label8);
            infoPanel.Controls.Add(label9);
            infoPanel.Controls.Add(labelWindowW);
            infoPanel.Controls.Add(labelWindowH);
            infoPanel.Controls.Add(label7);
            infoPanel.Controls.Add(labelGPU);
            infoPanel.Controls.Add(label5);
            infoPanel.Controls.Add(label6);
            infoPanel.Controls.Add(labelZoom);
            infoPanel.Controls.Add(labelIterations);
            infoPanel.Controls.Add(label3);
            infoPanel.Controls.Add(label4);
            infoPanel.Controls.Add(labelMouseX);
            infoPanel.Controls.Add(labelMouseY);
            infoPanel.Controls.Add(label1);
            infoPanel.Controls.Add(label2);
            infoPanel.Controls.Add(labelCenterX);
            infoPanel.Controls.Add(labelCenterY);
            infoPanel.Location = new Point(0, 0);
            infoPanel.Margin = new Padding(7, 6, 0, 0);
            infoPanel.Name = "infoPanel";
            infoPanel.Padding = new Padding(0, 0, 21, 18);
            infoPanel.Size = new Size(187, 211);
            infoPanel.TabIndex = 1;
            infoPanel.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Black;
            label10.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.LimeGreen;
            label10.Location = new Point(21, 152);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(98, 15);
            label10.TabIndex = 21;
            label10.Text = "Perturbation:";
            // 
            // labelPerturbation
            // 
            labelPerturbation.AutoSize = true;
            labelPerturbation.BackColor = Color.Black;
            labelPerturbation.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPerturbation.ForeColor = Color.LimeGreen;
            labelPerturbation.Location = new Point(129, 152);
            labelPerturbation.Margin = new Padding(2, 0, 2, 0);
            labelPerturbation.Name = "labelPerturbation";
            labelPerturbation.Size = new Size(21, 15);
            labelPerturbation.TabIndex = 22;
            labelPerturbation.Text = "No";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Black;
            label8.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.LimeGreen;
            label8.Location = new Point(21, 116);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 17;
            label8.Text = "Window W:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Black;
            label9.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.LimeGreen;
            label9.Location = new Point(21, 130);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(70, 15);
            label9.TabIndex = 18;
            label9.Text = "Window H:";
            // 
            // labelWindowW
            // 
            labelWindowW.AutoSize = true;
            labelWindowW.BackColor = Color.Black;
            labelWindowW.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelWindowW.ForeColor = Color.LimeGreen;
            labelWindowW.Location = new Point(129, 116);
            labelWindowW.Margin = new Padding(2, 0, 2, 0);
            labelWindowW.Name = "labelWindowW";
            labelWindowW.Size = new Size(35, 15);
            labelWindowW.TabIndex = 19;
            labelWindowW.Text = "1024";
            // 
            // labelWindowH
            // 
            labelWindowH.AutoSize = true;
            labelWindowH.BackColor = Color.Black;
            labelWindowH.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelWindowH.ForeColor = Color.LimeGreen;
            labelWindowH.Location = new Point(129, 130);
            labelWindowH.Margin = new Padding(2, 0, 2, 0);
            labelWindowH.Name = "labelWindowH";
            labelWindowH.Size = new Size(28, 15);
            labelWindowH.TabIndex = 20;
            labelWindowH.Text = "768";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Black;
            label7.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.LimeGreen;
            label7.Location = new Point(21, 178);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 15;
            label7.Text = "Renderer:";
            // 
            // labelGPU
            // 
            labelGPU.AutoSize = true;
            labelGPU.BackColor = Color.Black;
            labelGPU.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGPU.ForeColor = Color.LimeGreen;
            labelGPU.Location = new Point(129, 178);
            labelGPU.Margin = new Padding(2, 0, 2, 0);
            labelGPU.Name = "labelGPU";
            labelGPU.Size = new Size(14, 15);
            labelGPU.TabIndex = 16;
            labelGPU.Text = "-";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Black;
            label5.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.LimeGreen;
            label5.Location = new Point(21, 80);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 11;
            label5.Text = "Zoom:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Black;
            label6.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.LimeGreen;
            label6.Location = new Point(21, 94);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 12;
            label6.Text = "Iterations:";
            // 
            // labelZoom
            // 
            labelZoom.AutoSize = true;
            labelZoom.BackColor = Color.Black;
            labelZoom.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelZoom.ForeColor = Color.LimeGreen;
            labelZoom.Location = new Point(129, 80);
            labelZoom.Margin = new Padding(2, 0, 2, 0);
            labelZoom.Name = "labelZoom";
            labelZoom.Size = new Size(35, 15);
            labelZoom.TabIndex = 13;
            labelZoom.Text = "-0.5";
            // 
            // labelIterations
            // 
            labelIterations.AutoSize = true;
            labelIterations.BackColor = Color.Black;
            labelIterations.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelIterations.ForeColor = Color.LimeGreen;
            labelIterations.Location = new Point(129, 94);
            labelIterations.Margin = new Padding(2, 0, 2, 0);
            labelIterations.Name = "labelIterations";
            labelIterations.Size = new Size(14, 15);
            labelIterations.TabIndex = 14;
            labelIterations.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Black;
            label3.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.LimeGreen;
            label3.Location = new Point(21, 45);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 7;
            label3.Text = "Mouse X:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Black;
            label4.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.LimeGreen;
            label4.Location = new Point(21, 59);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "Mouse Y:";
            // 
            // labelMouseX
            // 
            labelMouseX.AutoSize = true;
            labelMouseX.BackColor = Color.Black;
            labelMouseX.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMouseX.ForeColor = Color.LimeGreen;
            labelMouseX.Location = new Point(129, 45);
            labelMouseX.Margin = new Padding(2, 0, 2, 0);
            labelMouseX.Name = "labelMouseX";
            labelMouseX.Size = new Size(35, 15);
            labelMouseX.TabIndex = 9;
            labelMouseX.Text = "-0.5";
            // 
            // labelMouseY
            // 
            labelMouseY.AutoSize = true;
            labelMouseY.BackColor = Color.Black;
            labelMouseY.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMouseY.ForeColor = Color.LimeGreen;
            labelMouseY.Location = new Point(129, 59);
            labelMouseY.Margin = new Padding(2, 0, 2, 0);
            labelMouseY.Name = "labelMouseY";
            labelMouseY.Size = new Size(14, 15);
            labelMouseY.TabIndex = 10;
            labelMouseY.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Black;
            label1.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.LimeGreen;
            label1.Location = new Point(21, 9);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 2;
            label1.Text = "Center X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Black;
            label2.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.LimeGreen;
            label2.Location = new Point(21, 23);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 3;
            label2.Text = "Center Y:";
            // 
            // labelCenterX
            // 
            labelCenterX.AutoSize = true;
            labelCenterX.BackColor = Color.Black;
            labelCenterX.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCenterX.ForeColor = Color.LimeGreen;
            labelCenterX.Location = new Point(129, 9);
            labelCenterX.Margin = new Padding(2, 0, 2, 0);
            labelCenterX.Name = "labelCenterX";
            labelCenterX.Size = new Size(35, 15);
            labelCenterX.TabIndex = 4;
            labelCenterX.Text = "-0.5";
            // 
            // labelCenterY
            // 
            labelCenterY.AutoSize = true;
            labelCenterY.BackColor = Color.Black;
            labelCenterY.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCenterY.ForeColor = Color.LimeGreen;
            labelCenterY.Location = new Point(129, 23);
            labelCenterY.Margin = new Padding(2, 0, 2, 0);
            labelCenterY.Name = "labelCenterY";
            labelCenterY.Size = new Size(14, 15);
            labelCenterY.TabIndex = 6;
            labelCenterY.Text = "0";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 768);
            Controls.Add(infoPanel);
            Controls.Add(glControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl;
        private Panel infoPanel;
        private Label label1;
        private Label label2;
        private Label labelCenterX;
        private Label labelCenterY;
        private Label label7;
        private Label labelGPU;
        private Label label5;
        private Label label6;
        private Label labelZoom;
        private Label labelIterations;
        private Label label3;
        private Label label4;
        private Label labelMouseX;
        private Label labelMouseY;
        private Label label8;
        private Label label9;
        private Label labelWindowW;
        private Label labelWindowH;
        private Label label10;
        private Label labelPerturbation;
    }
}