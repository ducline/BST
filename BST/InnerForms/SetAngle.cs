using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {
        private SerialPort arduinoSerialPort;

        public SetAngle(string search)
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = (int)numericUpDown1.Value;
            arduinoSerialPort.WriteLine(angle.ToString()); // Send the angle value to Arduino

            
        }

        private void SetAngle_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            arduinoSerialPort.Close(); // Close the serial port connection when the form is closing
        }

        private void SetAngle_Load(object sender, EventArgs e)
        {
            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                string retrievedVariable = managerForm.CommunicationPort;

                arduinoSerialPort = new SerialPort(retrievedVariable, 9600); // Replace "COMX" with your Arduino's COM port
                arduinoSerialPort.Open(); // Open the serial port connection
            }
        }
    }
}
