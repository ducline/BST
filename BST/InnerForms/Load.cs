using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BST.InnerForms
{
    public partial class Load : Form
    {
        string collection;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "vQSNvIfNsufocRgWFE5KvmRAvlWMi9k5i5sZGzwS",
            BasePath = "https://briareus-64aeb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;
        public Load(string search)
        {
            InitializeComponent();

            collection = search;

        }

        List<string> stringList = new List<string>();
        private async void LoadCollection(string collectionName)
        {
            FirebaseResponse response = await client.GetTaskAsync($"collections/{collectionName}/Collection");
            if (response.Body != "null")
            {
                List<Dictionary<string, object>> data = response.ResultAs<List<Dictionary<string, object>>>();

                // Access the values inside the "Collection" location and process them as needed
                int yLocation = 15;
                int position = 1;
                foreach (var entry in data)
                {
                    string key = entry["Key"].ToString();
                    object value = entry["Value"];

                    // Process the key-value pair as per your requirements
                    // ...

                    Label predefname = new Label();
                    panel1.Controls.Add(predefname);
                    panel1.Controls.SetChildIndex(predefname, 2);
                    predefname.ForeColor = Color.White;
                    predefname.BackColor = Color.FromArgb(26, 41, 48);
                    predefname.Size = new Size(panel1.Width - 30, 20);
                    predefname.Location = new Point(10, yLocation);

                    // Remove "(number)" suffix from key
                    string originalKey = RemoveNumberSuffix(key);
                    predefname.Text = position + " | " + originalKey;
                    predefname.Name = position + " | " + originalKey;

                    stringList.Add(originalKey);

                    yLocation += 30;
                    position++;
                }
            }
        }

        private string RemoveNumberSuffix(string key)
        {
            // Check if the key ends with "(number)" pattern
            int suffixIndex = key.LastIndexOf(" (");
            if (suffixIndex != -1 && key.EndsWith(")"))
            {
                return key.Substring(0, suffixIndex);
            }

            return key; // No "(number)" suffix found, return original key
        }


        private void Load_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
            label1.Text = collection;
            LoadCollection(collection);
        }



        public event EventHandler LoopingPlayingChanged;

        protected virtual void OnLoopingPlayingChanged()
        {
            LoopingPlayingChanged?.Invoke(this, EventArgs.Empty);
        }

        bool looping = false;
        bool playing = false;
        public bool Looping { get; private set; }
        public bool Playing { get; private set; }

        TimeSpan runningTime;

        private async Task LoadData(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                FirebaseResponse response = await client.GetTaskAsync("predefinitions/" + search);
                Data obj = response.ResultAs<Data>();

                //MessageBox.Show(search + "\nThumb | " + obj.thumb + "\nIndex | " + obj.index + "\nMiddle | " + obj.middle + "\nRing | " + obj.ring + "\nLittle | " + obj.little);
            }
            await Task.Delay(1000); // Delay for 1 second

        }

        private async Task LoopCollection()
        {
            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "STOP LOOPING"));

            looping = true;
            Looping = true;
            UpdateLoopingPlaying();
            runningTime = TimeSpan.Zero;
            UpdateRunningTime();

            // Start the timer
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) =>
            {
                runningTime = runningTime.Add(TimeSpan.FromSeconds(1));
                UpdateRunningTime();
            };
            timer.Start();

            while (looping)
            {
                int position = 1;
                foreach (var predefname in stringList)
                {
                    if (!looping)
                        break;

                    // PLAY ARDUINO MOTORS
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Control control = panel1.Controls.Find(position + " | " + predefname, true).FirstOrDefault();
                        if (control != null)
                        {
                            ClearHighlights();

                            control.BackColor = Color.Yellow;
                            control.ForeColor = Color.Black;
                        }
                    }));

                    await LoadData(predefname);
                    position++;
                }
            }

            // Stop the timer
            timer.Stop();
            timer.Dispose();

            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "PLAY"));
        }

        private async Task PlayCollection()
        {
            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "STOP PLAYING"));

            playing = true;
            Playing = true;
            UpdateLoopingPlaying();
            runningTime = TimeSpan.Zero;
            UpdateRunningTime();

            // Start the timer
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (sender, e) =>
            {
                runningTime = runningTime.Add(TimeSpan.FromSeconds(1));
                UpdateRunningTime();
            };
            timer.Start();

            while (playing)
            {
                int position = 1;
                foreach (var predefname in stringList)
                {
                    if (!playing)
                        break;

                    // PLAY ARDUINO MOTORS
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Control control = panel1.Controls.Find(position + " | " + predefname, true).FirstOrDefault();
                        if (control != null)
                        {
                            ClearHighlights();
                            control.BackColor = Color.Yellow;
                            control.ForeColor = Color.Black;
                        }
                    }));

                    await LoadData(predefname);
                    position++;
                }
                playing = false;
            }

            // Stop the timer
            timer.Stop();
            timer.Dispose();

            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "PLAY"));
        }




        private void UpdateRunningTime()
        {
            string formattedTime = runningTime.ToString(@"mm\:ss");
            label2.Invoke((MethodInvoker)(() => label2.Text = formattedTime));
        }

        private void ClearHighlights()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control.BackColor == Color.Yellow)
                {
                    control.ForeColor = Color.White;
                    control.BackColor = Color.FromArgb(26, 41, 48);
                }
            }
        }

        private async void SetAngle_Click(object sender, EventArgs e)
        {
            if (SetAngle.Text == "STOP PLAYING")
            {
                playing = false;
                Playing = false;
                UpdateLoopingPlaying();
            }
            if (SetAngle.Text == "STOP LOOPING")
            {
                looping = false;
                Looping = false;
                UpdateLoopingPlaying();
            }

            if (SetAngle.Text != "PLAY")
                return;

            button1.Enabled = false;
            checkBox1.Enabled = false;
            pictureBox1.Visible = true;
            label2.Visible = true;

            if (checkBox1.Checked)
            {
                await LoopCollection();
            }
            else
            {
                await PlayCollection();
            }

            looping = false;
            Looping = false;
            playing = false;
            Playing = false;
            UpdateLoopingPlaying();

            ClearHighlights();

            label2.Visible = false;
            pictureBox1.Visible = false;
            checkBox1.Enabled = true;
            button1.Enabled = true;
        }

        private void UpdateLoopingPlaying()
        {
            OnLoopingPlayingChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string matchedStrings = "";

            foreach (string labelText in stringList)
            {
                matchedStrings += labelText + ";";
            }

            string modifiedString = matchedStrings.TrimEnd(';');

            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                managerForm.OpenSearchableForm(modifiedString, "Collection", label1.Text);
                this.Close();

            }
        }

        private void Load_Leave(object sender, EventArgs e)
        {
            playing = false;
            Playing = false;
            looping = false;
            Looping = false;
            UpdateLoopingPlaying();
        }
    }
}
