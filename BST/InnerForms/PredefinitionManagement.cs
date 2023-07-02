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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Net.NetworkInformation;

namespace BST.InnerForms
{
    public partial class PredefinitionManagement : Form
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        string IncomingString;
        string searchBox;

        IFirebaseClient client;
        public PredefinitionManagement(string search)
        {
            InitializeComponent();
            if (search.Contains(";"))
            {
                IncomingString = search;
                searchBox = "";
            } else
            {
                searchBox = search;
            }
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
                    managerForm.OpenSearchableForm(predefname.Text, "Predefine", "");

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

                        button2.BackColor = Color.FromArgb(150, 0, 0);
                        button2.ForeColor = Color.FromArgb(230, 230, 230);

                        // Enabled Interactions
                        button1.Enabled = true;
                        button2.Enabled = true;

                        break;
                    }

                    // Disabled Background Color
                    button1.BackColor = Color.FromArgb(200, 200, 200);
                    button2.BackColor = Color.FromArgb(200, 200, 200);
                    // Disabled Font Color
                    button1.ForeColor = Color.FromArgb(150, 150, 150);

                    // Disable Interactions
                    button1.Enabled = false;
                    button2.Enabled = false;

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
            textBox1.Text = searchBox;
            SearchPredefinition(searchBox);
            textBox1.Select();
        }

        private void AddToCollection(string stringToAdd)
        {
            string collectionName = textBox2.Text;

            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 

                managerForm.OpenSearchableForm(IncomingString + stringToAdd, "Collection", collectionName);

                this.Close();
                //% Close the PredefinitionManagement form

                // Open the Predefine form
                //Predefine predefineForm = new Predefine();
                //predefineForm.Show();
            }
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

            if (button1.Text != "CREATE NEW COLLECTION")
            {
                AddToCollection(modifiedString); return;
            }

            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                managerForm.OpenSearchableForm(modifiedString, "Collection", "");

                this.Close();

            }

        }
        private bool ShowConfirmationDialog()
        {
            using (var popupForm = new PopUp("This action will delete the predefinition(s) from all of the collections, are you sure you want to do this?"))
            {
                popupForm.ShowDialog();

                if (popupForm.UserClickedContinue)
                {
                    return true;
                    // User clicked Continue
                }
                else
                {
                    return false;
                    // User clicked Cancel
                }
            }
        }

        private async void DeletePredefinitionFromCollection(string predefinitiontodelete, string collectionName)
        {
            FirebaseResponse response = await client.GetTaskAsync($"collections/{collectionName}/Collection");
            if (response.Body != "null")
            {
                List<Dictionary<string, object>> data = response.ResultAs<List<Dictionary<string, object>>>();
                // Access the values inside the "Collection" location and process them as 
                // Display the data in a MessageBox

                foreach (var entry in data)
                {
                    if (entry == null) continue;
                    string key = entry["Key"].ToString();
                    object value = entry["Value"];
                    if (key == predefinitiontodelete)
                    {
                        await client.DeleteTaskAsync($"collections/{collectionName}/Collection/{data.IndexOf(entry)}");
                    }
                }
            }
        }

        private async void DeleteFromCollections(string predefinition)
        {
            FirebaseResponse response = await client.GetTaskAsync("collections");
            if (response.Body != "null")
            {
                Dictionary<string, Data> data = response.ResultAs<Dictionary<string, Data>>();

                // Loop through the retrieved data
                foreach (var item in data)
                {
                    DeletePredefinitionFromCollection(predefinition, item.Key);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            int scrollPosition = panel1.VerticalScroll.Value;

            if (!ShowConfirmationDialog()) return;

            string matchedStrings = "";
            List<Task> deletionTasks = new List<Task>(); // Track the deletion tasks

            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    Label predefname = control as Label;
                    if (predefname.BackColor == Color.Yellow)
                    {
                        string selectedPrefinitions = predefname.Text;

                        DeleteFromCollections(selectedPrefinitions);

                        // Get a reference to the collection
                        FirebaseResponse collectionResponse = await client.GetTaskAsync($"predefinitions/{selectedPrefinitions}/");
                        string collectionJson = collectionResponse.Body;

                        // Deserialize the collection data into a dictionary
                        IDictionary<string, JToken> collectionData = JsonConvert.DeserializeObject<IDictionary<string, JToken>>(collectionJson);

                        // Iterate over each document in the collection and delete them asynchronously
                        foreach (var document in collectionData)
                        {
                            string documentId = document.Key;
                            Task deletionTask = client.DeleteTaskAsync($"predefinitions/{selectedPrefinitions}/{documentId}");
                            deletionTasks.Add(deletionTask); // Add the deletion task to the list
                        }

                        // Delete the collection itself asynchronously
                        Task deleteCollectionTask = client.DeleteTaskAsync($"predefinitions/{selectedPrefinitions}");
                        deletionTasks.Add(deleteCollectionTask); // Add the deletion task to the list
                    }
                }
            }

            // Wait for all deletion tasks to complete
            await Task.WhenAll(deletionTasks);

            SearchPredefinition(textBox1.Text);

            panel1.VerticalScroll.Value = scrollPosition;
        }

    }
}
