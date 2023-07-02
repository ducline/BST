namespace BST
{
    partial class Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SetAngle = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SignLanguageTranslator = new System.Windows.Forms.Button();
            this.CollectionManagement = new System.Windows.Forms.Button();
            this.PredefinitionManagement = new System.Windows.Forms.Button();
            this.Predefine = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(148)))), ((int)(((byte)(141)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 500);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.panel3.Controls.Add(this.pictureBox5);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.pictureBox4);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 25);
            this.panel3.TabIndex = 7;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::BST.Properties.Resources.wifi;
            this.pictureBox5.Location = new System.Drawing.Point(46, 1);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 25);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 12;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(149, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "deviceName";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(130, 1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 25);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(811, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(853, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            this.pictureBox2.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(192, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(700, 400);
            this.panel2.TabIndex = 3;
            // 
            // SetAngle
            // 
            this.SetAngle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.SetAngle.FlatAppearance.BorderSize = 0;
            this.SetAngle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetAngle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SetAngle.Location = new System.Drawing.Point(5, 5);
            this.SetAngle.Name = "SetAngle";
            this.SetAngle.Size = new System.Drawing.Size(140, 40);
            this.SetAngle.TabIndex = 8;
            this.SetAngle.TabStop = false;
            this.SetAngle.Text = "SET ANGLE";
            this.SetAngle.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.SignLanguageTranslator);
            this.panel4.Controls.Add(this.CollectionManagement);
            this.panel4.Controls.Add(this.PredefinitionManagement);
            this.panel4.Controls.Add(this.Predefine);
            this.panel4.Controls.Add(this.SetAngle);
            this.panel4.Location = new System.Drawing.Point(15, 75);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 400);
            this.panel4.TabIndex = 4;
            // 
            // SignLanguageTranslator
            // 
            this.SignLanguageTranslator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.SignLanguageTranslator.FlatAppearance.BorderSize = 0;
            this.SignLanguageTranslator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignLanguageTranslator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignLanguageTranslator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SignLanguageTranslator.Location = new System.Drawing.Point(5, 230);
            this.SignLanguageTranslator.Name = "SignLanguageTranslator";
            this.SignLanguageTranslator.Size = new System.Drawing.Size(140, 40);
            this.SignLanguageTranslator.TabIndex = 12;
            this.SignLanguageTranslator.TabStop = false;
            this.SignLanguageTranslator.Text = "SIGN LANGUAGE TRANSLATOR";
            this.SignLanguageTranslator.UseVisualStyleBackColor = false;
            // 
            // CollectionManagement
            // 
            this.CollectionManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.CollectionManagement.Enabled = false;
            this.CollectionManagement.FlatAppearance.BorderSize = 0;
            this.CollectionManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CollectionManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollectionManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CollectionManagement.Location = new System.Drawing.Point(5, 140);
            this.CollectionManagement.Name = "CollectionManagement";
            this.CollectionManagement.Size = new System.Drawing.Size(140, 40);
            this.CollectionManagement.TabIndex = 11;
            this.CollectionManagement.TabStop = false;
            this.CollectionManagement.Text = "COLLECTION MANAGEMENT";
            this.CollectionManagement.UseVisualStyleBackColor = false;
            // 
            // PredefinitionManagement
            // 
            this.PredefinitionManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.PredefinitionManagement.Enabled = false;
            this.PredefinitionManagement.FlatAppearance.BorderSize = 0;
            this.PredefinitionManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PredefinitionManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PredefinitionManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.PredefinitionManagement.Location = new System.Drawing.Point(5, 95);
            this.PredefinitionManagement.Name = "PredefinitionManagement";
            this.PredefinitionManagement.Size = new System.Drawing.Size(140, 40);
            this.PredefinitionManagement.TabIndex = 10;
            this.PredefinitionManagement.TabStop = false;
            this.PredefinitionManagement.Text = "PREDEFINITION MANAGEMENT";
            this.PredefinitionManagement.UseVisualStyleBackColor = false;
            // 
            // Predefine
            // 
            this.Predefine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.Predefine.Enabled = false;
            this.Predefine.FlatAppearance.BorderSize = 0;
            this.Predefine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Predefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Predefine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Predefine.Location = new System.Drawing.Point(5, 50);
            this.Predefine.Name = "Predefine";
            this.Predefine.Size = new System.Drawing.Size(140, 40);
            this.Predefine.TabIndex = 9;
            this.Predefine.TabStop = false;
            this.Predefine.Text = "PREDEFINE";
            this.Predefine.UseVisualStyleBackColor = false;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(41)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Briareus Support Tool";
            this.Load += new System.EventHandler(this.Manager_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetAngle;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button Predefine;
        private System.Windows.Forms.Button PredefinitionManagement;
        private System.Windows.Forms.Button CollectionManagement;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button SignLanguageTranslator;
    }
}