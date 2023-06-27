using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class PopUp : Form
    {
        string message;
        public bool UserClickedContinue { get; private set; }

        public PopUp(string text)
        {
            InitializeComponent();
            message = text;

            StartPosition = FormStartPosition.Manual;

        }

        private void PopUp_Load(object sender, EventArgs e)
        {
            CenterToParent(); // Center the form relative to the parent form initially

            label2.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //YES
            UserClickedContinue = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NO
            UserClickedContinue = false;
            Close();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            CenterToParent(); // Recenter the form when the parent form's location changes
        }
    }
}
