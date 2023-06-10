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
using System.Collections.ObjectModel;

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
                    managerForm.OpenSearchableForm(predefname.Text, "Predefine", false);

                    // Close the PredefinitionManagement form
                    this.Close();

                    // Open the Predefine form
                    //Predefine predefineForm = new Predefine();
                    //predefineForm.Show();
                }
            }
        }

        private void UpdateCollectionValidation()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is Label) 
                {
                    Label predefname = control as Label;
                    if (predefname.BackColor == Color.Yellow)
                    {
                        button1.BackColor = Color.FromArgb(40, 60, 70);
                        button1.ForeColor = Color.FromArgb(230, 230, 230);

                        // Enabled Interactions
                        button1.Enabled = true;

                        break;
                    }

                    // Disabled Background Color
                    button1.BackColor = Color.FromArgb(200, 200, 200);

                    // Disabled Font Color
                    button1.ForeColor = Color.FromArgb(150, 150, 150);

                    // Disable Interactions
                    button1.Enabled = false;

                }
                
            }
        }

        private void ClearHighlights(Label predefname)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control == predefname) { continue; }
                if (control is Label label)
                {
                    label.BackColor = Color.FromArgb(26, 41, 48);
                    label.ForeColor = Color.White;
                }
            }

        }

        private void ClearControls()
        {
            for (int i = panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control control = panel1.Controls[i];
                panel1.Controls.Remove(control);
                control.Dispose();
            }
        }


        private async void SearchPredefinition(string search)
        {
            if (active) { return; }
            active = true;
            ClearControls();

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
                        predefname.BackColor = Color.FromArgb(26, 41, 48);
                        predefname.Size = new Size(panel1.Width - 50, 20);
                        predefname.Location = new Point(30, yLocation);
                        predefname.Name = item.Key;

                        Button predefgoto = new Button();
                        panel1.Controls.Add(predefgoto);
                        panel1.Controls.SetChildIndex(predefgoto, 0);
                        predefgoto.Text = "Edit";
                        predefgoto.ForeColor = Color.White;
                        predefgoto.Location = new Point(200, yLocation);
                        predefgoto.Name = item.Key + "_button";

                        // Associate predefname label with predefgoto button using Tag property
                        predefgoto.Tag = predefname;

                        // Event handler for Ctrl + click on the row
                        predefname.MouseDown += (sender, e) =>
                        {
                            UpdateCollectionValidation();
                            if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
                            {
                                // Toggle the visibility of the highlight panel
                                if (predefname.BackColor == Color.Yellow)
                                {
                                    predefname.BackColor = Color.FromArgb(26, 41, 48);
                                    predefname.ForeColor = Color.White;

                                }
                                else
                                {
                                    predefname.BackColor = Color.Yellow;
                                    predefname.ForeColor = Color.Black;
                                }

                            } else if (e.Button == MouseButtons.Left)
                            {
                                ClearHighlights(predefname);
                                if (predefname.BackColor == Color.Yellow)
                                {
                                    predefname.BackColor = Color.FromArgb(26, 41, 48);
                                    predefname.ForeColor = Color.White;
                                }
                                else
                                {
                                    predefname.BackColor = Color.Yellow;
                                    predefname.ForeColor = Color.Black;
                                }
                                
                            }
                            UpdateCollectionValidation();


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
            UpdateCollectionValidation();

            active = false;
        }

        bool active = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //PREDEFINITION SEARCH
            UpdateCollectionValidation();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string matchedStrings = "";
            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    Label predefname = control as Label;
                    if (predefname.BackColor == Color.Yellow)
                    {
                        matchedStrings += predefname.Text + ";";
                    }

                }
            }

            string modifiedString = matchedStrings.Substring(0, matchedStrings.Length - 1);


            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm(modifiedString, "Collection", false);

                this.Close();
                //% Close the PredefinitionManagement form

                // Open the Predefine form
                //Predefine predefineForm = new Predefine();
                //predefineForm.Show();
            }

        }
    }
}
