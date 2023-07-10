using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BST.InnerForms
{
    public partial class Load : Form
    {
        string collection;
        private SerialPort USBPortSerial;
        private SerialPort bluetoothPortSerial;
        private bool bluetoothValue = false;

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

                    RetrieveValues(originalKey, predefname);
                    stringList.Add(originalKey);

                    yLocation += 30;
                    position++;
                }
            }
        }

        private async void RetrieveValues(string predefinition, Label label)
        {
            if (!string.IsNullOrEmpty(predefinition))
            {
                FirebaseResponse response = await client.GetTaskAsync("predefinitions/" + predefinition);
                Data obj = response.ResultAs<Data>();

                label.Tag = obj; // Store the 'obj' values in the label's Tag property

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
            alert.Text = "";
            Manager managerForm = this.Parent.Parent as Manager;
            USBPortSerial = managerForm.USBPortSerial;
            bluetoothPortSerial = managerForm.bluetoothPortSerial;
            bluetoothValue = managerForm.bluetooth;

            client = new FireSharp.FirebaseClient(config);
            if (client == null)
            {
                MessageBox.Show("Error in connection");
            }
            if (!string.IsNullOrEmpty(collection)) // FROM COLLECTIONS
            {
                label1.Text = collection;
                LoadCollection(collection);
            }

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

        private async Task LoadData(Label label)
        {
            if (label.Tag is Data obj)
            {
                // Access the values from the 'obj' object
                int angle1 = (int)obj.thumb;
                int angle2 = (int)obj.index;
                int angle3 = (int)obj.middle;
                int angle4 = (int)obj.ring;
                int angle5 = (int)obj.little;

                string data = $"{angle1},{angle2},{angle3},{angle4},{angle5}";

                try
                {
                    if (bluetoothValue) bluetoothPortSerial.WriteLine(data); else USBPortSerial.WriteLine(data);
                    alert.Text = "";
                }
                catch (Exception ex)
                {
                    alert.Text = ex.Message + " Showing demonstration.";
                }
             

                // Use the values as needed
                // ...
            }
            int delayal = (int)(numericUpDown1.Value * 1000);
            await Task.Delay(delayal); // Delay for 2 second

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

                    Label label = new Label();

                    // PLAY ARDUINO MOTORS
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Control control = panel1.Controls.Find(position + " | " + predefname, true).FirstOrDefault();
                        label = control as Label;
                        if (control != null)
                        {
                            ClearHighlights();

                            control.BackColor = Color.Yellow;
                            control.ForeColor = Color.Black;
                        }
                    }));

                    await LoadData(label);
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

                    Label label = new Label();

                    // PLAY ARDUINO MOTORS
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Control control = panel1.Controls.Find(position + " | " + predefname, true).FirstOrDefault();
                        label = control as Label;
                        if (control != null)
                        {
                            ClearHighlights();
                            control.BackColor = Color.Yellow;
                            control.ForeColor = Color.Black;
                        }
                    }));

                    await LoadData(label);
                    position++;
                }
                playing = false;
            }

            // Stop the timer
            timer.Stop();
            timer.Dispose();
            try
            {
                SetAngle.Invoke((MethodInvoker)(() => SetAngle.Text = "PLAY"));

            } catch (Exception ex)
            {
                alert.Text = ex.Message;
            }
        }




        private void UpdateRunningTime()
        {
            string formattedTime = runningTime.ToString(@"mm\:ss");
            try
            {
                label2.Invoke((MethodInvoker)(() => label2.Text = formattedTime));

            } catch (Exception ex) { 
                alert.Text = ex.Message;
            }
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

        private void ResetAngles()
        {
            string data = $"0,0,0,0,0";

            try
            {
                if (bluetoothValue) bluetoothPortSerial.WriteLine(data); else USBPortSerial.WriteLine(data);
                alert.Text = "";
            }
            catch (Exception ex)
            {
                alert.Text = ex.Message + " Showing demonstration.";
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
            numericUpDown1.Enabled = false;
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
            numericUpDown1.Enabled = true;
            button1.Enabled = true;

            ResetAngles();
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

        //STL FUNCTIONS

        private void FindFingerValues(string character, Label label)
        {
            char inputChar = char.ToUpper(character[0]);

            int thumbAngle = 0;
            int indexAngle = 0;
            int middleAngle = 0;
            int ringAngle = 0;
            int littleAngle = 0;

            switch (inputChar)
            {
                case 'A':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'B':
                    thumbAngle = 90;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 0;
                    littleAngle = 0;
                    break;
                case 'C':
                    thumbAngle = 90;
                    indexAngle = 90;
                    middleAngle = 90;
                    ringAngle = 90;
                    littleAngle = 90;
                    break;
                case 'D':
                    thumbAngle = 90;
                    indexAngle = 0;
                    middleAngle = 90;
                    ringAngle = 90;
                    littleAngle = 90;
                    break;
                case 'E':
                    thumbAngle = 180;
                    indexAngle = 120;
                    middleAngle = 120;
                    ringAngle = 120;
                    littleAngle = 120;
                    break;
                case 'F':
                    thumbAngle = 135;
                    indexAngle = 135;
                    middleAngle = 0;
                    ringAngle = 0;
                    littleAngle = 0;
                    break;
                case 'G':
                    thumbAngle = 0;
                    indexAngle = 45;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'H':
                    thumbAngle = 90;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'I':
                    thumbAngle = 135;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'J':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'K':
                    thumbAngle = 45;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'L':
                    thumbAngle = 0;
                    indexAngle = 0;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'M':
                    thumbAngle = 145;
                    indexAngle = 145;
                    middleAngle = 145;
                    ringAngle = 145;
                    littleAngle = 145;
                    break;
                case 'N':
                    thumbAngle = 180;
                    indexAngle = 135;
                    middleAngle = 135;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'O':
                    thumbAngle = 90;
                    indexAngle = 90;
                    middleAngle = 90;
                    ringAngle = 90;
                    littleAngle = 90;
                    break;
                case 'P':
                    thumbAngle = 90;
                    indexAngle = 0;
                    middleAngle = 45;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'Q':
                    thumbAngle = 45;
                    indexAngle = 45;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'R':
                    thumbAngle = 45;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 135;
                    littleAngle = 135;
                    break;
                case 'S':
                    thumbAngle = 180;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'T':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'U':
                    thumbAngle = 180;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 135;
                    littleAngle = 180;
                    break;
                case 'V':
                    thumbAngle = 180;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 45;
                    littleAngle = 45;
                    break;
                case 'W':
                    thumbAngle = 180;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 0;
                    littleAngle = 180;
                    break;
                case 'X':
                    thumbAngle = 135;
                    indexAngle = 45;
                    middleAngle = 135;
                    ringAngle = 135;
                    littleAngle = 135;
                    break;
                case 'Y':
                    thumbAngle = 45;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'Z':
                    thumbAngle = 180;
                    indexAngle = 0;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                default:
                    break;
            }

            // Set the values in the label's Tag property
            label.Tag = new Data
            {
                thumb = thumbAngle,
                index = indexAngle,
                middle = middleAngle,
                ring = ringAngle,
                little = littleAngle
            };

        }

        public void LoadSTL(string incomingSTL)
        {
            if (string.IsNullOrEmpty(incomingSTL))
            {
                string STLText = label1.Text;
                RetrieveSTLValues(STLText);

            }
            else
            {
                RetrieveSTLValues(incomingSTL);
            }
        }

        private void RetrieveSTLValues(string predefinition)
        {
            stringList.Clear();
            int yLocation = 15;
            int position = 1;
            for (int i = 0; i < predefinition.Length; i++)
            {
                string character = Convert.ToString(predefinition[i]);
                // Do something with the character, such as displaying it in the label

                Label letter = new Label();
                panel1.Controls.Add(letter);
                panel1.Controls.SetChildIndex(letter, 2);
                letter.ForeColor = Color.White;
                letter.BackColor = Color.FromArgb(26, 41, 48);
                letter.Size = new Size(panel1.Width - 30, 20);
                letter.Location = new Point(10, yLocation);

                // Remove "(number)" suffix from key
                letter.Text = position + " | " + character;
                letter.Name = position + " | " + character;

                FindFingerValues(character, letter);
                stringList.Add(character);

                yLocation += 30;
                position++;
            }
        }


    }



}
