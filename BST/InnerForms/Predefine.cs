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
    public partial class Predefine : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        public Predefine()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e) // SAVE PREDEFINITION
        {
            var datalayer = new Data
            {
                thumb = numericUpDown1.Value,
                index = numericUpDown2.Value,
                middle = numericUpDown3.Value,
                ring = numericUpDown4.Value,
                little = numericUpDown5.Value
            };

            SetResponse resp = await client.SetTaskAsync("predefinitions/" + textBox1.Text, datalayer);
            Data result = resp.ResultAs<Data>();
            MessageBox.Show("Data inserted");

        }

        private void Predefine_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
        }
    }
}
