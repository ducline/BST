using FireSharp.Config;
using FireSharp.Interfaces;
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
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
            int yLocation = 0;
            int positionValue = 1;
            foreach (string value in predefnames)
            {
                CreatePositionLabel(positionValue, yLocation);
                CreateNameLabel(value, yLocation);
                CreateUpButton(positionValue, yLocation);
                CreateDownButton(positionValue, yLocation);

                positionValue++;
                yLocation += 30;
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

        private void CreateNameLabel(string name, int yLocation)
        {
            Label nameLabel = new Label();
            panel1.Controls.Add(nameLabel);
            nameLabel.ForeColor = Color.White;
            nameLabel.BackColor = Color.FromArgb(26, 41, 48);
            nameLabel.Text = name;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(30, yLocation);
            this.nameLabel.Add(nameLabel);
        }

        private void CreateUpButton(int position, int yLocation)
        {
            Button upButton = new Button();
            panel1.Controls.Add(upButton);
            upButton.Text = "▲";
            upButton.Tag = position;
            upButton.Location = new Point(150, yLocation);
            upButton.Size = new Size(20, 20); // Set the size of the button
            upButton.Click += UpButton_Click;
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
            downButton.Click += DownButton_Click;
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

        private async void button1_Click(object sender, EventArgs e)
        {
            string collectionName = textBox1.Text;

            var collectionData = new Dictionary<string, int>();

            for (int i = 0; i < predefnames.Count; i++)
            {
                string name = predefnames[i];

                collectionData.Add(name, i + 1);
            }

            var datalayer = new CollectionData
            {
                Collection = collectionData
            };

            SetResponse resp = await client.SetTaskAsync("collections/" + collectionName, datalayer);

            MessageBox.Show("Data inserted");
        }


    }
}
