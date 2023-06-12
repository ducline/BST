using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {
        private string retrievedVariable;
        int count = 0;
        public SetAngle(string search)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = (int)numericUpDown1.Value;
            string message = Convert.ToString(angle);

        }

        private void SetAngle_Load(object sender, EventArgs e)
        {
            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                retrievedVariable = managerForm.CommunicationPort;
                serialPort1.PortName = retrievedVariable;
                serialPort1.BaudRate = 9600;
                try
                {
                   
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);

            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();

            try
            {
                SerialPort bluetoothdevice = (SerialPort)sender;
                Console.Write("Data" + count);
                count++;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
