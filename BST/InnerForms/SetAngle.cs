using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {
        private string retrievedVariable;
        int count = 0;
        public SetAngle(string search)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = (int)numericUpDown1.Value;
            string message = Convert.ToString(angle);

        }

        private void SetAngle_Load(object sender, EventArgs e)
        {
            
        }


    }
}
