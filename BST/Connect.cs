using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using BST;
using InTheHand.Net.Bluetooth;
using System.Net.Sockets;

namespace BriareusSupportTool
{
    public partial class Connect : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Connect()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private async void CreateConnection()
        {
            string deviceName = textBox1.Text;

            //establishedConnection("testCOM", deviceName); TEST

            if (button1.Text == "CONNECTING") return;
            // Use Invoke to run the code on the UI thread
            this.Invoke((MethodInvoker)delegate
            {
                button1.Text = "CONNECTING";
                button1.BackColor = Color.Yellow;
            });

            // Define the name of the Bluetooth device you want to connect to


            // Run Bluetooth device discovery on a separate thread
            var devicesTask = Task.Run(() =>
            {
                BluetoothClient client = new BluetoothClient();
                IReadOnlyCollection<BluetoothDeviceInfo> testdevices = client.DiscoverDevices();
                return testdevices;
            });

            // Wait for the Bluetooth device discovery to complete
            var devices = await devicesTask;

            // Search for the specified device and get its Bluetooth address
            string deviceAddress = "";
            foreach (BluetoothDeviceInfo device in devices)
            {
                if (device.DeviceName == deviceName)
                {
                    deviceAddress = device.DeviceAddress.ToString();
                    break;
                }
            }

            // If the device was found, get its virtual COM port
            if (deviceAddress != "")
            {
                string comPort = "";
                foreach (var port in SerialPort.GetPortNames())
                {
                    BluetoothDeviceInfo device = new BluetoothDeviceInfo(BluetoothAddress.Parse(deviceAddress));
                    if (device.InstalledServices.Where(service => service.Equals(new Guid("{00001101-0000-1000-8000-00805F9B34FB}"))).Any())
                    {
                        // Use Invoke to update the label on the UI thread
                        comPort = port;

                        // Connect to the Bluetooth device
                        BluetoothClient client = new BluetoothClient();
                        BluetoothEndPoint endPoint = new BluetoothEndPoint(device.DeviceAddress, BluetoothService.SerialPort);
                        await client.ConnectAsync(endPoint);


                        // Close the connection
                        //client.Close();

                        
                        MessageBox.Show(GetConnectedComPort(comPort));

                        establishedConnection(GetConnectedComPort(comPort), deviceName);

                        break;
                    }
                }



            }
            else
            {
                // Use Invoke to update the button and label on the UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    button1.Text = "CONNECT";
                    button1.BackColor = Color.Green;
                    label2.ForeColor = Color.Red;
                    label2.Text = string.Format("Device {0} not found. Make sure it is on discoverable mode", deviceName);
                });


            }
        }

        private void establishedConnection(string comPort, string deviceName)
        {
            this.Invoke((MethodInvoker)delegate
            {
                label2.ForeColor = Color.DarkGreen;
                label2.Text = string.Format("COM port for {0} is {1}", deviceName, comPort);
                button1.Text = "CONNECT";
                button1.BackColor = Color.Green;

                // Create a new Menu
                Manager menu = new Manager(comPort, deviceName);


                // Set the parent form and position the Menu form
                menu.Owner = this;
                menu.StartPosition = FormStartPosition.Manual;

                // Calculate the center point of the parent form
                Point parentCenter = new Point(
                    this.Location.X + this.Width / 2,
                    this.Location.Y + this.Height / 2
                );

                // Center the Menu form relative to the parent form
                menu.Location = new Point(
                    parentCenter.X - menu.Width / 2,
                    parentCenter.Y - menu.Height / 2
                );

                // Check if the form is outside the screen and reposition it inside if necessary
                Screen screen = Screen.FromControl(menu);
                if (!screen.Bounds.Contains(menu.Bounds))
                {
                    int maxWidth = screen.WorkingArea.Width - menu.Width;
                    int maxHeight = screen.WorkingArea.Height - menu.Height;
                    int x = Math.Max(screen.WorkingArea.X, Math.Min(menu.Location.X, maxWidth));
                    int y = Math.Max(screen.WorkingArea.Y, Math.Min(menu.Location.Y, maxHeight));
                    menu.Location = new Point(x, y);
                }

                // Show the Menu form and hide the parent form
                menu.Show();
                this.Hide();



            });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CreateConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Connect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout-darker.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox3.Image = newImage;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox3.Image = newImage;
        }

        private void Connect_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                CreateConnection();
            }
            

        }

        private string GetConnectedComPort(string previousComPort)
        {
            string[] portNamesAfterConnection = SerialPort.GetPortNames();
            foreach (string portName in portNamesAfterConnection)
            {
                if (!previousComPort.Contains(portName))
                {
                    return portName;
                }
            }
            return null;
        }
    }
}
