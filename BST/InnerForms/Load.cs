﻿using FireSharp.Config;
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
                    predefname.Text = position + " | " + key;
                    predefname.ForeColor = Color.White;
                    predefname.BackColor = Color.FromArgb(26, 41, 48);
                    predefname.Size = new Size(panel1.Width - 30, 20);
                    predefname.Location = new Point(10, yLocation);

                    yLocation += 30;
                    position++;
                }
            }
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

        private async Task LoopCollection()
        {
            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "STOP LOOPING"));

            looping = true;
            Looping = true;
            UpdateLoopingPlaying();
            runningTime = TimeSpan.Zero;
            UpdateRunningTime();

            while (looping)
            {
                // LOOP ACTIVITIES

                await Task.Delay(1000); // Delay for 1 second
                runningTime = runningTime.Add(TimeSpan.FromSeconds(1));
                UpdateRunningTime();
            }

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

            while (playing)
            {
                // PLAY ACTIVITIES

                await Task.Delay(1000); // Delay for 1 second
                runningTime = runningTime.Add(TimeSpan.FromSeconds(1));
                UpdateRunningTime();
            }

            SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "PLAY"));
        }

        private void UpdateRunningTime()
        {
            string formattedTime = runningTime.ToString(@"mm\:ss");
            label2.Invoke((MethodInvoker)(() => label2.Text = formattedTime));
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

            label2.Visible = false;
            pictureBox1.Visible = false;
            checkBox1.Enabled = true;
        }

        private void UpdateLoopingPlaying()
        {
            OnLoopingPlayingChanged();
        }

    }
}
