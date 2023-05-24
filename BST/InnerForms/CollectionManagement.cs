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
    public partial class CollectionManagement : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;
        public CollectionManagement(string search)
        {
            InitializeComponent();
        }

        private async void SearchCollections(string search)
        {
            panel2.Controls.Clear();
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
                        panel2.Controls.Add(predefname);
                        predefname.Text = item.Key;
                        predefname.ForeColor = Color.White;
                        predefname.Location = new Point(30, yLocation);

                        //Button predefgoto = new Button();
                        //panel2.Controls.Add(predefgoto);
                        //predefgoto.Text = "Edit";
                        //predefgoto.ForeColor = Color.White;
                        //predefgoto.Location = new Point(200, yLocation);




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

                //foreach (Control control in panel1.Controls)
                //{
                //    if (control is Button button)
                //    {
                //        button.Click += OpenPredefine;
                //    }
                //}

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SearchCollections(textBox2.Text);
        }
    }
}
