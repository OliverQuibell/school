namespace Prototype_3
{
    partial class Form1
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
            panel1 = new Panel();
            carbon = new Button();
            hydrogen = new Button();
            bond = new Button();
            oxygen = new Button();
            doublebond = new Button();
            deleteelement = new Button();
            deletebonds = new Button();
            nitrogen = new Button();
            triplebond = new Button();
            model = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Location = new Point(14, 14);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(841, 693);
            panel1.TabIndex = 0;
            // 
            // carbon
            // 
            carbon.Location = new Point(1025, 14);
            carbon.Margin = new Padding(4, 3, 4, 3);
            carbon.Name = "carbon";
            carbon.Size = new Size(160, 69);
            carbon.TabIndex = 1;
            carbon.Text = "Carbon";
            carbon.UseVisualStyleBackColor = true;
            carbon.Click += carbon_Click;
            // 
            // hydrogen
            // 
            hydrogen.Location = new Point(1026, 89);
            hydrogen.Margin = new Padding(4, 3, 4, 3);
            hydrogen.Name = "hydrogen";
            hydrogen.Size = new Size(159, 75);
            hydrogen.TabIndex = 2;
            hydrogen.Text = "Hydrogen";
            hydrogen.UseVisualStyleBackColor = true;
            hydrogen.Click += hydrogen_Click;
            // 
            // bond
            // 
            bond.Location = new Point(858, 14);
            bond.Margin = new Padding(4, 3, 4, 3);
            bond.Name = "bond";
            bond.Size = new Size(160, 73);
            bond.TabIndex = 3;
            bond.Text = "Bond";
            bond.UseVisualStyleBackColor = true;
            bond.Click += bond_Click;
            // 
            // oxygen
            // 
            oxygen.Location = new Point(1026, 170);
            oxygen.Margin = new Padding(4, 3, 4, 3);
            oxygen.Name = "oxygen";
            oxygen.Size = new Size(160, 69);
            oxygen.TabIndex = 0;
            oxygen.Text = "Oxygen";
            oxygen.Click += oxygen_Click;
            // 
            // doublebond
            // 
            doublebond.BackColor = SystemColors.ControlLight;
            doublebond.Location = new Point(857, 92);
            doublebond.Margin = new Padding(4, 3, 4, 3);
            doublebond.Name = "doublebond";
            doublebond.Size = new Size(159, 72);
            doublebond.TabIndex = 4;
            doublebond.Text = "Double Bond";
            doublebond.UseVisualStyleBackColor = false;
            doublebond.Click += doublebond_Click;
            // 
            // deleteelement
            // 
            deleteelement.Location = new Point(858, 244);
            deleteelement.Margin = new Padding(4, 3, 4, 3);
            deleteelement.Name = "deleteelement";
            deleteelement.Size = new Size(160, 69);
            deleteelement.TabIndex = 5;
            deleteelement.Text = "Delete Element";
            deleteelement.UseVisualStyleBackColor = true;
            deleteelement.Click += deleteelement_Click;
            // 
            // deletebonds
            // 
            deletebonds.Location = new Point(857, 319);
            deletebonds.Name = "deletebonds";
            deletebonds.Size = new Size(161, 69);
            deletebonds.TabIndex = 6;
            deletebonds.Text = "Delete Bond";
            deletebonds.UseVisualStyleBackColor = true;
            deletebonds.Click += deletebonds_Click;
            // 
            // nitrogen
            // 
            nitrogen.Location = new Point(1026, 245);
            nitrogen.Name = "nitrogen";
            nitrogen.Size = new Size(160, 69);
            nitrogen.TabIndex = 7;
            nitrogen.Text = "Nitrogen";
            nitrogen.UseVisualStyleBackColor = true;
            nitrogen.Click += nitrogen_Click;
            // 
            // triplebond
            // 
            triplebond.Location = new Point(858, 170);
            triplebond.Name = "triplebond";
            triplebond.Size = new Size(160, 68);
            triplebond.TabIndex = 8;
            triplebond.Text = "Triple Bond";
            triplebond.UseVisualStyleBackColor = true;
            triplebond.Click += triplebond_Click;
            // 
            // model
            // 
            model.Location = new Point(857, 394);
            model.Name = "model";
            model.Size = new Size(161, 69);
            model.TabIndex = 9;
            model.Text = "Model 3D";
            model.UseVisualStyleBackColor = true;
            model.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1263, 808);
            Controls.Add(model);
            Controls.Add(triplebond);
            Controls.Add(nitrogen);
            Controls.Add(deletebonds);
            Controls.Add(deleteelement);
            Controls.Add(doublebond);
            Controls.Add(oxygen);
            Controls.Add(bond);
            Controls.Add(hydrogen);
            Controls.Add(carbon);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button carbon;
        private Button hydrogen;
        private Button bond;
        private Button oxygen;
        private Button doublebond;
        private Button deleteelement;
        private Button deletebonds;
        private Button nitrogen;
        private Button triplebond;
        private Button model;
    }
}