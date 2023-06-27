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
using System.Collections;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BST.InnerForms
{
    public partial class CollectionManagement : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        string collectiontosearch = "";

        public CollectionManagement(string search)
        {
            InitializeComponent();

            collectiontosearch = search;
        }

        private List<string> labelOrder = new List<string>(); // Declare a list to store label texts in order

        private async void LoadPredefinitions(string collectionName)
        {
            panel2.Controls.Clear();
            labelOrder.Clear(); // Clear the label order list

            FirebaseResponse response = await client.GetTaskAsync($"collections/{collectionName}/Collection");
            if (response.Body != "null")
            {
                List<Dictionary<string, object>> data = response.ResultAs<List<Dictionary<string, object>>>();

                // Access the values inside the "Collection" location and process them as needed
                int yLocation = 15;
                foreach (var entry in data)
                {
                    if (entry == null) continue;
                    string key = entry["Key"].ToString();
                    object value = entry["Value"];

                    // Process the key-value pair as per your requirements
                    // ...

                    Label predefname = new Label();
                    panel2.Controls.Add(predefname);
                    panel2.Controls.SetChildIndex(predefname, 2);
                    predefname.Text = key;
                    predefname.ForeColor = Color.White;
                    predefname.BackColor = Color.FromArgb(26, 41, 48);
                    predefname.Size = new Size(panel2.Width - 30, 20);
                    predefname.Location = new Point(10, yLocation);

                    labelOrder.Add(key); // Store the label text in the order they are created

                    yLocation += 30;
                }
            }
        }

        private string SelectedCollection()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    Label predefname = control as Label;
                    if (predefname.BackColor == Color.Yellow)
                    {

                        return predefname.Text;
                    }

                }

            }

            return "";
        }

        private void UpdateCollectionValidation()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    panel2.Controls.Clear();
                    Label predefname = control as Label;
                    if (predefname.BackColor == Color.Yellow)
                    {
                        button1.BackColor = Color.FromArgb(40, 60, 70);
                        button1.ForeColor = Color.FromArgb(230, 230, 230);

                        button3.BackColor = Color.FromArgb(150, 0, 0);
                        button3.ForeColor = Color.FromArgb(230, 230, 230);

                        // Enabled Interactions
                        button1.Enabled = true;
                        button3.Enabled = true;
                        LoadPredefinitions(predefname.Text);

                        break;
                    }

                    // Disabled Background Color
                    button1.BackColor = Color.FromArgb(200, 200, 200);
                    button3.BackColor = Color.FromArgb(200, 200, 200);

                    // Disabled Font Color
                    button1.ForeColor = Color.FromArgb(150, 150, 150);
                    button3.ForeColor = Color.FromArgb(150, 150, 150);



                    // Disable Interactions
                    button1.Enabled = false;
                    button3.Enabled= false;
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

            panel2.Controls.Clear();

        }
        private async void SearchCollections(string search)
        {
            panel1.Controls.Clear();

            FirebaseResponse response = await client.GetTaskAsync("collections");
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
                        predefname.Size = new Size(panel1.Width - 30, 20);
                        predefname.Location = new Point(10, yLocation);

                        predefname.MouseDown += (sender, e) =>
                        {
                            //UpdateCollectionValidation();
                            if (e.Button == MouseButtons.Left)
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

            }

            UpdateCollectionValidation();
        }

        private void OpenCollection(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Text == "Edit")
            {
                Label predefname = button.Tag as Label;

                Manager managerForm = this.Parent.Parent as Manager;

                if (managerForm != null)
                {
                    // Call the OpenSearchableForm method of the Manager 
                    managerForm.OpenSearchableForm(predefname.Text, "Predefine", "");

                    // Close the PredefinitionManagement form
                    this.Close();

                    // Open the Predefine form
                    //Predefine predefineForm = new Predefine();
                    //predefineForm.Show();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SearchCollections(textBox2.Text);
        }

        private void CollectionManagement_Load(object sender, EventArgs e)
        {
            textBox2.Select();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }

            if (collectiontosearch != "")
            {
                textBox2.Text = collectiontosearch;
            }

            SearchCollections(collectiontosearch);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string matchedStrings = "";

            foreach (string labelText in labelOrder)
            {
                matchedStrings += labelText + ";";
            }

            string modifiedString = matchedStrings.TrimEnd(';');

            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                managerForm.OpenSearchableForm(modifiedString, "Collection", SelectedCollection());
                this.Close();

            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            int scrollPosition = panel1.VerticalScroll.Value;
            panel2.Controls.Clear();
            string selectedCollection = SelectedCollection(); // Assuming SelectedCollection() returns the ID or name of the collection to be deleted

            // Get a reference to the collection
            FirebaseResponse collectionResponse = await client.GetTaskAsync($"collections/{selectedCollection}/");
            string collectionJson = collectionResponse.Body;

            // Deserialize the collection data into a dictionary
            IDictionary<string, JToken> collectionData = JsonConvert.DeserializeObject<IDictionary<string, JToken>>(collectionJson);

            // Iterate over each document in the collection and delete them
            foreach (var document in collectionData)
            {
                string documentId = document.Key;
                FirebaseResponse documentResponse = await client.DeleteTaskAsync($"collections/{selectedCollection}/{documentId}");

                // Handle the deletion response if needed
            }

            // Delete the collection itself
            FirebaseResponse deleteCollectionResponse = await client.DeleteTaskAsync($"collections/{selectedCollection}");

            // Handle the deletion response if needed
            SearchCollections(textBox2.Text);

            panel1.VerticalScroll.Value = scrollPosition;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                managerForm.OpenSearchableForm("", "Collection", "");

                this.Close();

            }
        }
    }

}
