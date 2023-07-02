using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;
using System.Management;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {
        private SerialPort arduinoPort;
        private bool bluetoothValue;
        private const string USBPortName = "COM8";  // Replace with the appropriate USB port name
        private const string BluetoothPortName = "COM4";  // Replace with the appropriate Bluetooth port name

        string modifiedString;
        public SetAngle(string search)
        {
            InitializeComponent();
            modifiedString = search;
        }

        private string FindBluetoothPort()
        {
            return BluetoothPortName; //MAYBE REMOVE LATER
            string bluetoothPort = null;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");
                foreach (ManagementObject obj in searcher.Get())
                {
                    object captionObj = obj["Caption"];
                    if (captionObj != null && captionObj.ToString().Contains("HC-05"))
                    {
                        object portObj = obj["Caption"];
                        if (portObj != null)
                        {
                            string caption = portObj.ToString();
                            int startIndex = caption.LastIndexOf("(COM") + 1;
                            int endIndex = caption.LastIndexOf(")");
                            if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
                            {
                                bluetoothPort = caption.Substring(startIndex, endIndex - startIndex);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the search
                // You can log the exception or display an error message
                Console.WriteLine("Failed to find Bluetooth port: " + ex.Message);
            }

            return bluetoothPort;
        }

        private void InitializeSerialPort()
        {
            string portName = bluetoothValue ? FindBluetoothPort() : USBPortName;
            int baudRate = 9600;

            if (portName != null)
            {
                arduinoPort = new SerialPort(portName, baudRate);

                try
                {
                    arduinoPort.Open();
                }
                catch (Exception ex)
                {
                    label1.Text = "Failed to open the Arduino port. Error: " + ex.Message;
                }
            }
            else
            {
                label1.Text = "Port not found.";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int angle1 = (int)numericUpDown1.Value;
            int angle2 = (int)numericUpDown2.Value;
            int angle3 = (int)numericUpDown3.Value;
            int angle4 = (int)numericUpDown4.Value;
            int angle5 = (int)numericUpDown5.Value;
            bool sendDataViaBluetooth = bluetoothValue;  // Get the boolean value from the bluetoothValue variable

            if (arduinoPort != null && arduinoPort.IsOpen)
            {
                string data = $"{angle1},{angle2},{angle3},{angle4},{angle5},{sendDataViaBluetooth}";
                arduinoPort.WriteLine(data);
                label1.Text = "";
            }
            else
            {
                label1.Text = "Arduino port is not open.";
            }
        }


        private void SetAngle_Load(object sender, EventArgs e)
        {
            if(modifiedString != "")
            {
                string[] values = modifiedString.Split(';');
                int count = Math.Min(values.Length, 5); // Limit the count to 5 in case there are fewer values

                for (int i = 0; i < count; i++)
                {
                    int value;
                    if (int.TryParse(values[i], out value))
                    {
                        NumericUpDown numericUpDown = (NumericUpDown)this.Controls.Find("numericUpDown" + (i + 1), true)[0];
                        numericUpDown.Value = value;
                    }
                }
            }

            label1.Text = "";

            Manager managerForm = this.Parent.Parent as Manager;
            if (managerForm != null)
            {
                bluetoothValue = managerForm.bluetooth;
                // Use the 'bluetoothValue' variable as needed
            }

            InitializeSerialPort();
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
