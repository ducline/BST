namespace BST.InnerForms
{
    partial class Load
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
            this.SetAngle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SetAngle
            // 
            this.SetAngle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.SetAngle.FlatAppearance.BorderSize = 0;
            this.SetAngle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetAngle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SetAngle.Location = new System.Drawing.Point(501, 299);
            this.SetAngle.Name = "SetAngle";
            this.SetAngle.Size = new System.Drawing.Size(140, 40);
            this.SetAngle.TabIndex = 15;
            this.SetAngle.Text = "LOAD";
            this.SetAngle.UseVisualStyleBackColor = false;
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(41)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.SetAngle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Load";
            this.Text = "Load";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetAngle;
    }
}