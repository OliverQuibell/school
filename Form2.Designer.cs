namespace Prototype_3
{
    partial class Form2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            cube = new Button();
            sphere = new Button();
            cylinder = new Button();
            label1 = new Label();
            getdata = new Button();
            drawmol = new Button();
            rec = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1564, 1027);
            panel1.TabIndex = 0;
            panel1.Scroll += panel_scroll;
            panel1.MouseDown += panel_mousedown;
            panel1.MouseMove += panel_mousemove;
            panel1.MouseUp += panel_mouseup;
            // 
            // cube
            // 
            cube.Location = new Point(1598, 49);
            cube.Name = "cube";
            cube.Size = new Size(221, 100);
            cube.TabIndex = 1;
            cube.Text = "Cube";
            cube.UseVisualStyleBackColor = true;
            cube.Click += cube_Click;
            // 
            // sphere
            // 
            sphere.Location = new Point(1598, 190);
            sphere.Name = "sphere";
            sphere.Size = new Size(221, 105);
            sphere.TabIndex = 2;
            sphere.Text = "Sphere";
            sphere.UseVisualStyleBackColor = true;
            sphere.Click += sphere_Click;
            // 
            // cylinder
            // 
            cylinder.Location = new Point(1598, 331);
            cylinder.Name = "cylinder";
            cylinder.Size = new Size(221, 101);
            cylinder.TabIndex = 3;
            cylinder.Text = "Cylinder";
            cylinder.UseVisualStyleBackColor = true;
            cylinder.Click += cylinder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1837, 49);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // getdata
            // 
            getdata.Location = new Point(1598, 453);
            getdata.Name = "getdata";
            getdata.Size = new Size(179, 88);
            getdata.TabIndex = 5;
            getdata.Text = "Get Data";
            getdata.UseVisualStyleBackColor = true;
            getdata.Click += getdata_Click;
            // 
            // drawmol
            // 
            drawmol.Location = new Point(1598, 570);
            drawmol.Name = "drawmol";
            drawmol.Size = new Size(172, 81);
            drawmol.TabIndex = 6;
            drawmol.Text = "Draw Molecule";
            drawmol.UseVisualStyleBackColor = true;
            drawmol.Click += drawmol_Click;
            // 
            // rec
            // 
            rec.Location = new Point(1613, 691);
            rec.Name = "rec";
            rec.Size = new Size(141, 76);
            rec.TabIndex = 7;
            rec.Text = "recursion attempt";
            rec.UseVisualStyleBackColor = true;
            rec.Click += rec_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1966, 1051);
            Controls.Add(rec);
            Controls.Add(drawmol);
            Controls.Add(getdata);
            Controls.Add(label1);
            Controls.Add(cylinder);
            Controls.Add(sphere);
            Controls.Add(cube);
            Controls.Add(panel1);
            Name = "Form2";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        private void panel_scroll(object sender, ScrollEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Panel panel1;
        private Button cube;
        private Button sphere;
        private Button cylinder;
        private Label label1;
        private Button getdata;
        private Button drawmol;
        private Button rec;
    }
}