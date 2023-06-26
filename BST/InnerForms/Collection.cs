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
using static System.Net.Mime.MediaTypeNames;

namespace BST.InnerForms
{
    public partial class Collection : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;
        string newsearch = "";

        private List<string> predefnames;
        private List<Label> positionLabels;
        private List<Label> nameLabel;
        private List<Button> upButtons;
        private List<Button> downButtons;

        private Button addPredefinitionButton;


        public Collection(string matchedStrings)
        {
            InitializeComponent();
            predefnames = matchedStrings.Split(';').ToList();
            positionLabels = new List<Label>();
            nameLabel = new List<Label>();
            upButtons = new List<Button>();
            downButtons = new List<Button>();
        }

        private void Collection_Load(object sender, EventArgs e)
        {
            label3.Text = "";
            addPredefinitionButton = new Button();
            if (textBox1.Text == null || textBox1.Text == "")
            {
                addPredefinitionButton.Enabled = false;
            }
            else addPredefinitionButton.Enabled = true;

            CheckForValidation();
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }

            ReorderList();

        }

        private async void CheckIfNameAlreadyExists()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return;

            FirebaseResponse response = await client.GetTaskAsync("collections/" + textBox1.Text);
            if (response.Body != "null")
            {
                // Name already exists
                if (textBox1.Enabled == false) return;
                label3.Text = "Name already exists!";
                // Perform additional actions if needed
            }
            else label3.Text = "";
        }



        private void EnableButtons()
        {
            
        }

        private void DisableButtons()
        {
            
        }

        private void ReorderList()
        {
            int scrollPosition = panel1.VerticalScroll.Value;

            // Remove all controls from the panel
            panel1.Controls.Clear();

            int yLocation = 10;
            int positionValue = 1;
            foreach (string value in predefnames)
            {
                if(positionValue == 1 && value == "") { yLocation = 10; break; }
                CreatePositionLabel(positionValue, yLocation);
                CreateNameLabel(value, yLocation, positionValue);
                CreateUpButton(positionValue, yLocation);
                CreateDownButton(positionValue, yLocation);

                positionValue++;
                yLocation += 35;
            }

            CreatePositionLabel(positionValue, yLocation);
            // ... Continue with the rest of the code
            panel1.Controls.Add(addPredefinitionButton);
            addPredefinitionButton.Parent = panel1;
            addPredefinitionButton.Text = "Add Predefinition";
            addPredefinitionButton.ForeColor = Color.White;
            addPredefinitionButton.BackColor = Color.FromArgb(40, 60, 70);
            addPredefinitionButton.FlatStyle = FlatStyle.Flat;
            addPredefinitionButton.FlatAppearance.BorderSize = 0;
            addPredefinitionButton.Location = new Point(25, yLocation - 8); // Adjust the location as needed
            addPredefinitionButton.Size = new Size(100, 30); // Adjust the size as needed
            addPredefinitionButton.Click += AddPredefinitionButton_Click;

            panel1.VerticalScroll.Value = scrollPosition;

        }

        bool buttonclicked = false;
        private void AddPredefinitionButton_Click(object sender, EventArgs e)
        {
            if (buttonclicked) return;
            buttonclicked = true;
            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                string matchedStrings = "";
                foreach (Control control in panel1.Controls)
                {
                    if (control is Label)
                    {
                        Label predefname = control as Label;
                        if (predefname.Name == "nameLabel")
                        {
                            matchedStrings += predefname.Text + ";";
                        }

                    }
                }

                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm(matchedStrings, "PredefinitionManagement", textBox1.Text);
                this.Close();
                //% Close the PredefinitionManagement form

                // Open the Predefine form
                //Predefine predefineForm = new Predefine();
                //predefineForm.Show();
            }
        }


        private void CreatePositionLabel(int position, int yLocation)
        {
            Label positionLabel = new Label();
            panel1.Controls.Add(positionLabel);
            positionLabel.ForeColor = Color.White;
            positionLabel.BackColor = Color.FromArgb(26, 41, 48);
            positionLabel.Text = position.ToString();
            positionLabel.AutoSize = true;
            positionLabel.Location = new Point(0, yLocation);
            positionLabels.Add(positionLabel);
        }

        private void CreateNameLabel(string name, int yLocation, int position)
        {
            Label nameLabel = new Label();
            panel1.Controls.Add(nameLabel);
            nameLabel.ForeColor = Color.White;
            nameLabel.BackColor = Color.FromArgb(26, 41, 48);
            nameLabel.Text = name;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(30, yLocation);
            nameLabel.MaximumSize = new Size(100, 30);
            nameLabel.Name = "nameLabel";
            this.nameLabel.Add(nameLabel);

            Button deleteButton = new Button();
            panel1.Controls.Add(deleteButton);
            deleteButton.Text = "X";
            deleteButton.Tag = position;
            deleteButton.Location = new Point(210, yLocation);
            deleteButton.Size = new Size(20, 20); // Set the size of the button
            deleteButton.Click += DeleteButton_Click;
            deleteButton.ForeColor = Color.Red;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int position = (int)button.Tag;

            int index = position - 1;
            if (index >= 0 && index < predefnames.Count)
            {
                predefnames.RemoveAt(index);
                ReorderList();
            }
        }



        private void CreateUpButton(int position, int yLocation)
        {
            Button upButton = new Button();
            panel1.Controls.Add(upButton);
            upButton.Text = "▲";
            upButton.Tag = position;
            upButton.Location = new Point(150, yLocation);
            upButton.Size = new Size(20, 20); // Set the size of the button
            upButton.ForeColor = Color.Yellow;
            upButton.Click += UpButton_Click;
            upButton.FlatStyle = FlatStyle.Flat;
            upButton.FlatAppearance.BorderSize = 0;
            upButtons.Add(upButton);
        }

        private void CreateDownButton(int position, int yLocation)
        {
            Button downButton = new Button();
            panel1.Controls.Add(downButton);
            downButton.Text = "▼";
            downButton.Tag = position;
            downButton.Location = new Point(180, yLocation);
            downButton.Size = new Size(20, 20); // Set the size of the button
            downButton.ForeColor = Color.Yellow;
            downButton.Click += DownButton_Click;
            downButton.FlatStyle = FlatStyle.Flat;
            downButton.FlatAppearance.BorderSize = 0;
            downButtons.Add(downButton);
        }


        private void UpButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int position = (int)button.Tag;

            if (position > 1)
            {
                int index = position - 1;
                SwapPositions(index, index - 1);
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int position = (int)button.Tag;

            if (position < predefnames.Count)
            {
                int index = position - 1;
                SwapPositions(index, index + 1);
            }
        }

        private void SwapPositions(int index1, int index2)
        {
            string temp = predefnames[index1];
            predefnames[index1] = predefnames[index2];
            predefnames[index2] = temp;

            nameLabel[index1].Text = predefnames[index1];
            nameLabel[index2].Text = predefnames[index2];
        }

        private void OpenCollectionManagement()
        {


            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm(textBox1.Text, "CollectionManagement", "");

                // Close the PredefinitionManagement form
                this.Close();

            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string collectionName = textBox1.Text;

            var collectionData = new Dictionary<string, int>();

            for (int i = 0; i < predefnames.Count; i++)
            {
                string name = predefnames[i];
                string newName = name;
                int count = 1;

                while (collectionData.ContainsKey(newName))
                {
                    newName = $"{name} ({count})";
                    count++;
                }

                collectionData.Add(newName, i + 1);
            }

            var datalayer = new CollectionData
            {
                Collection = collectionData
            };

            SetResponse resp = await client.SetTaskAsync("collections/" + collectionName, datalayer);

            OpenCollectionManagement();
        }


        private void CheckForValidation()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Disabled Background Color
                button1.BackColor = Color.FromArgb(200, 200, 200);

                // Disabled Font Color
                button1.ForeColor = Color.FromArgb(150, 150, 150);

                // Disable Interactions
                button1.Enabled = false;
            }
            else
            {
                button1.BackColor = Color.FromArgb(40, 60, 70);
                button1.ForeColor = Color.FromArgb(230, 230, 230);

                // Enabled Interactions
                button1.Enabled = true;
            }

            CheckIfNameAlreadyExists();
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

            if (text == null || text == "") {
                addPredefinitionButton.Enabled = false;
            } else addPredefinitionButton.Enabled = true;

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

        private void button2_Click(object sender, EventArgs e)
        {
            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm(textBox1.Text, "Load", "");

                // Close the PredefinitionManagement form
                this.Close();

            }
        }
    }
}
