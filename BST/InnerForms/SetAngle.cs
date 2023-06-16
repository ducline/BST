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
            InitializeSerialPort();

        }

        private void InitializeSerialPort()
        {
            arduinoPort = new SerialPort("COM8", 9600); // Replace "COM3" with the appropriate COM port
            arduinoPort.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = (int)numericUpDown1.Value;
            arduinoPort.WriteLine(angle.ToString());

        }

        private void SetAngle_Load(object sender, EventArgs e)
        {
            
        }

        private void SetAngle_FormClosing(object sender, FormClosingEventArgs e)
        {
            arduinoPort.Close();
        }
    }
}
