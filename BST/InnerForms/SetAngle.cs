using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {
        private SerialPort arduinoPort;

        int count = 0;
        public SetAngle(string search)
        {
            InitializeComponent();
        }
        bool error = false;
        private void InitializeSerialPort()
        {
            try
            {
                arduinoPort = new SerialPort("COM8", 9600); // Replace "COM8" with the appropriate COM port

                // Add a try-catch block to handle the case when the port is not found
                try
                {
                    arduinoPort.Open();
                }
                catch (Exception ex)
                {
                    label1.Text = "Failed to open the Arduino port. Error: " + ex.Message;
                    error = true;
                }
            }
            catch (Exception ex)
            {
                label1.Text = "Failed to initialize the Arduino port. Error: " + ex.Message;
                error = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int angle1 = (int)numericUpDown1.Value;
            int angle2 = (int)numericUpDown2.Value;
            int angle3 = (int)numericUpDown3.Value;
            int angle4 = (int)numericUpDown4.Value;
            int angle5 = (int)numericUpDown5.Value;

            if (arduinoPort != null && arduinoPort.IsOpen)
            {
                string angles = string.Join(",", angle1, angle2, angle3, angle4, angle5);
                arduinoPort.WriteLine(angles);
                MessageBox.Show("SENT VALUES");
                label1.Text = "";
            }
            else
            {
                label1.Text = "Arduino port is not open.";
            }
        }

        private void SetAngle_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            InitializeSerialPort();
            if (error)
            {
                Manager managerForm = this.Parent.Parent as Manager;

            }
        }

        private void SetAngle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (arduinoPort != null && arduinoPort.IsOpen)
            {
                arduinoPort.Close();
            }
        }
    }
}
