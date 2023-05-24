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
        public PredefinitionManagement(string search)
        {
            InitializeComponent();
        }

        private void OpenPredefine(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Text == "Edit")
            {
                Label predefname = button.Tag as Label;

                Manager managerForm = this.Parent.Parent as Manager;

                if (managerForm != null)
                {
                    // Call the OpenSearchableForm method of the Manager 
                    managerForm.OpenSearchableForm(predefname.Text, "Predefine");

                    // Close the PredefinitionManagement form
                    this.Close();

                    // Open the Predefine form
                    //Predefine predefineForm = new Predefine();
                    //predefineForm.Show();
                }
            }
        }

        private async void SearchPredefinition(string search)
        {
            panel1.Controls.Clear();

            FirebaseResponse response = await client.GetTaskAsync("predefinitions");
            if (response.Body != "null")
            {
                Dictionary<string, Data> data = response.ResultAs<Dictionary<string, Data>>();

                // Filter data based on search criteria
                // Filter data based on search criteria (case-insensitive)
                var filteredData = data.Where(item => item.Key.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToDictionary(item => item.Key, item => item.Value);

                if (filteredData.Count > 0)
                {
                    // Loop through the retrieved data
                    int yLocation = 15;
                    foreach (var item in filteredData)
                    {
                        string key = item.Key;
                        Data obj = item.Value;

                        Label predefname = new Label();
                        panel1.Controls.Add(predefname);
                        panel1.Controls.SetChildIndex(predefname, 2);
                        predefname.Text = item.Key;
                        predefname.ForeColor = Color.White;
                        predefname.Location = new Point(30, yLocation);

                        Button predefgoto = new Button();
                        panel1.Controls.Add(predefgoto);
                        predefgoto.Text = "Edit";
                        panel1.Controls.SetChildIndex(predefgoto, 1);
                        predefgoto.ForeColor = Color.White;
                        predefgoto.Location = new Point(200, yLocation);

                        // Associate predefname label with predefgoto button using Tag property
                        predefgoto.Tag = predefname;

                        // Create a hidden panel underneath the row
                        Panel highlightPanel = new Panel();
                        panel1.Controls.Add(highlightPanel);
                        panel1.Controls.SetChildIndex(highlightPanel, 0);
                        highlightPanel.BackColor = Color.Yellow; // Set desired highlight color
                        highlightPanel.Location = new Point(0, yLocation);
                        highlightPanel.Size = new Size(panel1.Width, predefname.Height);
                        highlightPanel.Visible = false;

                        // Event handler for Ctrl + click on the row
                        predefname.MouseDown += (sender, e) =>
                        {
                            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
                            {
                                // Toggle the visibility of the highlight panel
                                highlightPanel.Visible = !highlightPanel.Visible;
                            }
                        };

                        


                        yLocation += 30;
                    }
                }
                else
                {
                    Label predefname = new Label();
                    panel1.Controls.Add(predefname);
                    predefname.Text = "No matching data";
                    predefname.Location = new Point(15, 15);
                    // Handle the case when no data matches the search criteria
                    // ...
                }

                foreach (Control control in panel1.Controls)
                {
                    if (control is Button button)
                    {
                        button.Click += OpenPredefine;
                    }
                }
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //PREDEFINITION SEARCH
            SearchPredefinition(textBox1.Text);
        }

        private void PredefinitionManagement_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
            SearchPredefinition("");
        }
    }
}
