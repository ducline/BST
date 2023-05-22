using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace BST.InnerForms
{
    public partial class PredefinitionManagement : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;
        public PredefinitionManagement()
        {
            InitializeComponent();
        }

        private void SearchPredefinition(string search)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //PREDEFINITION SEARCH

            SearchPredefinition(textBox1.Text);
        }

        private void PredefinitionManagement_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                label3.Text = "connected";
            }
        }
    }
}
