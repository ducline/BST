using BriareusSupportTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BST
{
    public partial class Manager : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Manager(string comPort, string deviceName)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            label1.Text = deviceName;

            foreach (Control control in panel4.Controls)
            {
                if (control is Button button)
                {
                    button.Click += OpenInnerForm;
                }
            }

        }
        private void OpenInnerForm(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Button button = (Button)sender;
            string formName = button.Name;

            // Create the form type based on the button name
            Type formType = Type.GetType("BST.InnerForms." + formName);

            // Create an instance of the form
            Form form = (Form)Activator.CreateInstance(formType);

            // Set the form's parent to panel2
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel2.Controls.Add(form);
            form.Show();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            string imagePath = @"Images\undo-darker.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox1.Image = newImage;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            string imagePath = @"Images\undo.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox1.Image = newImage;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout-darker.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox2.Image = newImage;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox2.Image = newImage;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create a new Menu form
            Connect connect = new Connect();

            // Set the parent form and position the Menu form
            connect.Owner = this;
            connect.StartPosition = FormStartPosition.Manual;

            // Calculate the center point of the parent form
            Point parentCenter = new Point(
                this.Location.X + this.Width / 2,
                this.Location.Y + this.Height / 2
            );

            // Center the Menu form relative to the parent form
            connect.Location = new Point(
                parentCenter.X - connect.Width / 2,
                parentCenter.Y - connect.Height / 2
            );

            // Check if the form is outside the screen and reposition it inside if necessary
            Screen screen = Screen.FromControl(connect);
            if (!screen.Bounds.Contains(connect.Bounds))
            {
                int maxWidth = screen.WorkingArea.Width - connect.Width;
                int maxHeight = screen.WorkingArea.Height - connect.Height;
                int x = Math.Max(screen.WorkingArea.X, Math.Min(connect.Location.X, maxWidth));
                int y = Math.Max(screen.WorkingArea.Y, Math.Min(connect.Location.Y, maxHeight));
                connect.Location = new Point(x, y);
            }

            // Show the Menu form and hide the parent form
            connect.Show();
            this.Hide();
        }
    }
}
