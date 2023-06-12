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
        string newsearch = "";
        public Predefine(string search)
        {
            InitializeComponent();

            newsearch = search;


        }
       
        private async void LoadData(string search)
        {
            if (search != null || search != "")
            {
                FirebaseResponse response = await client.GetTaskAsync("predefinitions/" + search);
                Data obj = response.ResultAs<Data>();

                textBox1.Text = search;
                numericUpDown1.Value = obj.thumb;
                numericUpDown2.Value = obj.index;
                numericUpDown3.Value = obj.middle;
                numericUpDown4.Value = obj.ring;
                numericUpDown5.Value = obj.little;

            }
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
        }


        private void Predefine_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
            if (!string.IsNullOrEmpty(newsearch))
            {
                textBox1.Enabled = false;
            }
            LoadData(newsearch);
            CheckForValidation();
        }
        private void OpenPredefinitionManagement()
        {


            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm("", "PredefinitionManagement", "");

                // Close the PredefinitionManagement form
                this.Close();

            }
            
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            OpenPredefinitionManagement();
        }

        private void CheckForValidation()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DisableSaveButton();
            }
            else
            {
                EnableSaveButton();
            }

            CheckIfNameAlreadyExists();
        }

        private void DisableSaveButton()
        {
            // Disabled Background Color
            button2.BackColor = Color.FromArgb(200, 200, 200);

            // Disabled Font Color
            button2.ForeColor = Color.FromArgb(150, 150, 150);

            // Disable Interactions
            button2.Enabled = false;
        }

        private void EnableSaveButton()
        {
            button2.BackColor = Color.FromArgb(40, 60, 70);
            button2.ForeColor = Color.FromArgb(230, 230, 230);

            // Enabled Interactions
            button2.Enabled = true;
        }


        private async void CheckIfNameAlreadyExists()
        {
            if (string.IsNullOrEmpty(newsearch))
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text)) { label2.Text = ""; return; }

                // Check if the predefinition already exists in the database
                FirebaseResponse checkResponse = await client.GetTaskAsync("predefinitions/" + textBox1.Text);
                if (checkResponse.Body != "null")
                {
                    label2.Text = "Name already exists!";
                    DisableSaveButton();
                } else { label2.Text = ""; EnableSaveButton();  }


            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string text = textBox1.Text;
            string validText = RemoveInvalidCharacters(text);

            if (text != validText)
            {
                // Update the text in the TextBox without the invalid characters
                textBox1.Text = validText;

                // Set the cursor position at the end of the TextBox
                textBox1.SelectionStart = validText.Length;
            }

            CheckForValidation();
        }

        private string RemoveInvalidCharacters(string text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if (Char.IsLetterOrDigit(c) || c == '_' || c == '-')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenPredefinitionManagement();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
