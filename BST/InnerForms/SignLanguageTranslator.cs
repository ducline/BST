using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class SignLanguageTranslator : Form
    {
        public SignLanguageTranslator(string search)
        {
            InitializeComponent();
        }
        private struct FingerAngles
        {
            public int ThumbAngle;
            public int IndexAngle;
            public int MiddleAngle;
            public int RingAngle;
            public int LittleAngle;
        }

        private FingerAngles GetFingerAnglesFromLabel(Label label)
        {
            if (label.Tag is FingerAngles fingerAngles)
            {
                return fingerAngles;
            }
            else
            {
                // Return default values or handle the case when the Tag is not of the expected type
                return default(FingerAngles);
            }
        }

        private async Task LoadData(Label label)
        {
            FingerAngles angles = GetFingerAnglesFromLabel(label);

            int thumbAngle = angles.ThumbAngle;
            int indexAngle = angles.IndexAngle;
            int middleAngle = angles.MiddleAngle;
            int ringAngle = angles.RingAngle;
            int littleAngle = angles.LittleAngle;

            if (label.Tag is Data obj)
            {
                // Access the values from the 'obj' object
                decimal thumb = obj.thumb;
                decimal index = obj.index;
                decimal middle = obj.middle;
                decimal ring = obj.ring;
                decimal little = obj.little;

                // Use the values as needed
                // ...
            }
            await Task.Delay(1000); // Delay for 1 second

        }

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
                    thumbAngle = 45;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'C':
                    thumbAngle = 90;
                    indexAngle = 90;
                    middleAngle = 90;
                    ringAngle = 90;
                    littleAngle = 90;
                    break;
                case 'D':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'E':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'F':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'G':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'H':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'I':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'J':
                    thumbAngle = 180;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'K':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'L':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'M':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'N':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'O':
                    thumbAngle = 180;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'P':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'Q':
                    thumbAngle = 45;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'R':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 0;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'S':
                    thumbAngle = 0;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 0;
                    littleAngle = 0;
                    break;
                case 'T':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 180;
                    break;
                case 'U':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'V':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'W':
                    thumbAngle = 90;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'X':
                    thumbAngle = 0;
                    indexAngle = 0;
                    middleAngle = 0;
                    ringAngle = 0;
                    littleAngle = 0;
                    break;
                case 'Y':
                    thumbAngle = 45;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                case 'Z':
                    thumbAngle = 0;
                    indexAngle = 180;
                    middleAngle = 180;
                    ringAngle = 180;
                    littleAngle = 0;
                    break;
                default:
                    break;
            }

            // Create a new instance of FingerAngles and assign the angle values
            FingerAngles fingerAngles = new FingerAngles
            {
                ThumbAngle = thumbAngle,
                IndexAngle = indexAngle,
                MiddleAngle = middleAngle,
                RingAngle = ringAngle,
                LittleAngle = littleAngle
            };

            // Set the Tag property of the label to the FingerAngles object
            label.Tag = fingerAngles;
        }


        List<string> stringList = new List<string>();

        private void RetrieveValues(string predefinition, Label label)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the non-letter key press
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                RetrieveValues(textBox1.Text, label1); // Pass the label control as an argument
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        public bool Looping { get; private set; }
        public bool Playing { get; private set; }

        bool looping = false, playing = false;
        TimeSpan runningTime;


        private async void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "STOP PLAYING")
            {
                playing = false;
                Playing = false;
                UpdateLoopingPlaying();
            }
            if (button1.Text == "STOP LOOPING")
            {
                looping = false;
                Looping = false;
                UpdateLoopingPlaying();
            }

            if (button1.Text != "LOAD TRANSLATION")
                return;

            checkBox1.Enabled = false;
            textBox1.Enabled = false;
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
            textBox1.Enabled = true;


        }

        private void UpdateLoopingPlaying()
        {
            OnLoopingPlayingChanged();
        }

        public event EventHandler LoopingPlayingChanged;

        protected virtual void OnLoopingPlayingChanged()
        {
            LoopingPlayingChanged?.Invoke(this, EventArgs.Empty);
        }

        private async Task LoopCollection()
        {
            button1.Invoke((MethodInvoker)(() => button1.Text = "STOP LOOPING"));

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

            button1.Invoke((MethodInvoker)(() => button1.Text = "LOAD TRANSLATION"));
        }

        private async Task PlayCollection()
        {
            button1.Invoke((MethodInvoker)(() => button1.Text = "STOP PLAYING"));

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

            button1.Invoke((MethodInvoker)(() => button1.Text = "LOAD TRANSLATION"));
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

        private void UpdateRunningTime()
        {
            string formattedTime = runningTime.ToString(@"mm\:ss");
            label2.Invoke((MethodInvoker)(() => label2.Text = formattedTime));
        }
    }
}
