using BriareusSupportTool;
using BST.InnerForms;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BST
{
    public partial class Manager : Form
    {

        public SerialPort arduinoPort;
        private const string USBPortName = "COM8";  // Replace with the appropriate USB port name
        private const string BluetoothPort = "COM5";


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Manager(string comPort, string deviceName)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(@"Images\Ico\BriareusSupportLogo.Ico");
            label1.Text = deviceName;

            foreach (Control control in panel4.Controls)
            {
                if (control is Button button)
                {
                    button.Click += OpenInnerForm;
                }
            }

        }
        bool looping, playing;
        private void LoadForm_LoopingPlayingChanged(object sender, EventArgs e)
        {
            Load loadForm = (Load)sender;

            // Check if looping or playing values have changed
            looping = loadForm.Looping;
            playing = loadForm.Playing;

            if(looping || playing)
            {
                foreach (Control control in panel4.Controls)
                {
                    if (control is Button buttons)
                    {
                        buttons.Enabled = false;
                    }
                }
            } else
            {
                if (!wifi)
                {
                    SetAngle.Enabled = true; SignLanguageTranslator.Enabled = true; return;
                }
                foreach (Control control in panel4.Controls)
                {
                    if (control is Button buttons)
                    {
                        buttons.Enabled = true;
                    }
                }
            }
            // Process the updated looping and playing values as needed
            // ...
        }


        public void OpenSearchableForm(string search, string formName, string secondaryValue)
        {
            arduinoPort.Close();

            this.Text = formName + " | Briareus Support Tool";

            foreach (Control control in panel4.Controls)
            {
                if (control is Button clearbuttons)
                {
                    clearbuttons.BackColor = Color.FromArgb(40, 60, 70);
                    clearbuttons.ForeColor = Color.FromArgb(230, 230, 230);
                }
            }
            foreach (Control control in panel4.Controls)
            {
                if (control is Button button)
                {
                    if (button.Name == formName)
                    {
                        button.BackColor = Color.FromArgb(20, 32, 38);
                        button.ForeColor = Color.White;
                    }
                }
            }

            Type formType = Type.GetType("BST.InnerForms." + formName);

            Form form = (Form)Activator.CreateInstance(formType, search);

            // Set the form's parent to panel2
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel2.Controls.Add(form);
            form.Show();

            if (form.Name == "SetAngle")
            {
                SetAngle setangle = (SetAngle)form;
                if (secondaryValue == "CLOSING")
                {

                    setangle.ResetFingers();
                }
            }

            if (form.Name == "Load")
            {
                Load loadForm = (Load)form;
                loadForm.LoopingPlayingChanged += LoadForm_LoopingPlayingChanged;
                if(secondaryValue == "STL")
                {
                    Label label1 = (Label)form.Controls.Find("label1", true).FirstOrDefault();
                    Button button1= (Button)form.Controls.Find("button1", true).FirstOrDefault();

                    button1.Visible = false;
                    label1.Text = search;
                    loadForm.LoadSTL(search);
                }
            }

            if (form.Name == "PredefinitionManagement")
            {
                TextBox textBox2 = (TextBox)form.Controls.Find("textBox2", true).FirstOrDefault();
                Button button1 = (Button)form.Controls.Find("button1", true).FirstOrDefault();
                if (textBox2 != null && secondaryValue != "")
                {
                    Label label2 = (Label)form.Controls.Find("label2", true).FirstOrDefault();

                    textBox2.Visible = true; label2.Visible = true;

                    if (button1 != null) button1.Text = "ADD TO COLLECTION";
                    textBox2.Text = secondaryValue;
                    textBox2.Enabled = false;
                }
            }


            if (form.Name == "Collection")
            {
                // Access the textbox control and set its text
                TextBox textBox1 = (TextBox)form.Controls.Find("textBox1", true).FirstOrDefault();
                Button button1 = (Button)form.Controls.Find("button1", true).FirstOrDefault();
                if (textBox1 != null && secondaryValue != "")
                {
                    if (button1 != null) button1.Text = "SAVE COLLECTION";
                    textBox1.Text = secondaryValue;
                    textBox1.Enabled = false;
                }
            }

            try
            {
                arduinoPort.Open();
                alert.Text = "";

            }
            catch (Exception ex) 
            {
                alert.Text = ex.Message;
            }

        }

        private void OpenInnerForm(object sender, EventArgs e)
        {
            arduinoPort.Close();

            foreach (Control control in panel4.Controls)
            {
                if (control is Button clearbuttons)
                {
                    clearbuttons.BackColor = Color.FromArgb(40, 60, 70);
                    clearbuttons.ForeColor = Color.FromArgb(230, 230, 230);
                }
            }

            Button button = (Button)sender;

            button.BackColor = Color.FromArgb(20, 32, 38);
            button.ForeColor = Color.White;

            string formName = button.Name;

            if (formName == CheckForOpenForm()) return;

            DisposeOfOpenInnerForm();
            //arduinoPort.Close()

            // Create the form type based on the button name
            this.Text = formName + " | Briareus Support Tool";

            Type formType = Type.GetType("BST.InnerForms." + formName);

            if (formType == null)
            {
                // Form not found, load the Base form instead
                formType = Type.GetType("BST.InnerForms.Base");
            }

            // Create an instance of the form
            // Create an instance of the form and pass a value to its constructor

            Form form = (Form)Activator.CreateInstance(formType, "");



            // Set the form's parent to panel2
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel2.Controls.Add(form);
            form.Show();

            try
            {
                arduinoPort.Open();
                alert.Text = "";
            }
            catch (Exception ex)
            {
                alert.Text = ex.Message;
            }

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            string imagePath = @"Images\undo-darker.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox1.Image = newImage;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            string imagePath = @"Images\undo.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox1.Image = newImage;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout-darker.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox2.Image = newImage;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            string imagePath = @"Images\logout.png"; // Path to the image file

            Image newImage = Image.FromFile(imagePath);
            pictureBox2.Image = newImage;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ResetAngles();

            // Create a new Menu form
            Connect connect = new Connect();

            // Set the parent form and position the Menu form
            connect.Owner = this;
            connect.StartPosition = FormStartPosition.Manual;

            // Calculate the center point of the parent form
            Point parentCenter = new Point(
                this.Location.X + this.Width / 2,
                this.Location.Y + this.Height / 2
            );

            // Center the Menu form relative to the parent form
            connect.Location = new Point(
                parentCenter.X - connect.Width / 2,
                parentCenter.Y - connect.Height / 2
            );

            // Check if the form is outside the screen and reposition it inside if necessary
            Screen screen = Screen.FromControl(connect);
            if (!screen.Bounds.Contains(connect.Bounds))
            {
                int maxWidth = screen.WorkingArea.Width - connect.Width;
                int maxHeight = screen.WorkingArea.Height - connect.Height;
                int x = Math.Max(screen.WorkingArea.X, Math.Min(connect.Location.X, maxWidth));
                int y = Math.Max(screen.WorkingArea.Y, Math.Min(connect.Location.Y, maxHeight));
                connect.Location = new Point(x, y);
            }

            // Show the Menu form and hide the parent form
            connect.Show();
            this.Hide();
        }

        private async void Manager_Load(object sender, EventArgs e)
        {
            alert.Text = "";
            InitializeSerialPort();
            IsInternetAvailable();
            await CheckInternetConnectionAsync(); // Perform the initial check

            // Start the timer to continuously check for internet connection
            timer1.Interval = 2000;
            timer1.Enabled = true;
            timer1.Tick += async (s, args) => await Timer_TickAsync();
        }

        private async Task Timer_TickAsync()
        {
            await CheckInternetConnectionAsync();
        }

        private async Task CheckInternetConnectionAsync()
        {
            bool hasInternetConnection = await Task.Run(() => IsInternetAvailable());
            // Use BeginInvoke to update UI controls from the UI thread 
            BeginInvoke(new Action(() =>
            {
                if (hasInternetConnection)
                {
                    WifiConnection(true);
                }
                else
                {
                    WifiConnection(false);
                }
            }));
        }

        private void DisposeOfOpenInnerForm()
        {
            foreach (Control control in panel2.Controls)
            {
                if (control is Form form)
                {
                    if (form.Visible)
                    {
                        form.Dispose();
                    }
                }
            }
        }

        private string CheckForOpenForm()
        {
            foreach (Control control in panel2.Controls)
            {
                if (control is Form form)
                {
                    if (form.Visible)
                    {
                        return form.Name;
                    }
                }
            }

            return "";
        }

        bool wifi = true;
        private void WifiConnection(bool enable)
        {
            if (enable)
            {
                wifi = true;
                if (!(looping || playing))
                {
                    // Enable the Predefine, PredefinitionManagement, and CollectionManagement buttons
                    Predefine.Enabled = true;
                    PredefinitionManagement.Enabled = true;
                    CollectionManagement.Enabled = true;
                    SetAngle.Enabled = true;
                    SignLanguageTranslator.Enabled = true;
                }

                string imagePath = @"Images\wifi.png"; // Path to the image file

                Image newImage = Image.FromFile(imagePath);
                pictureBox5.Image = newImage;
            } else
            {
                wifi = false;
                // Disable the Predefine, PredefinitionManagement, and CollectionManagement buttons
                Predefine.Enabled = false;
                PredefinitionManagement.Enabled = false;
                CollectionManagement.Enabled = false;

                if (!(CheckForOpenForm() == "SetAngle") && !(CheckForOpenForm() == "Load") && !(CheckForOpenForm() == "SignLanguageTranslator")) panel2.Controls.Clear();


                string imagePath = @"Images\no-wifi.png"; // Path to the image file

                Image newImage = Image.FromFile(imagePath);
                pictureBox5.Image = newImage;
            }
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 2000); // Ping Google's public DNS server (8.8.8.8) with a timeout of 2 seconds
                    return reply?.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool bluetooth;

        public void SetPictureBox4Image(Image image, bool enable)
        {
            pictureBox4.Image = image;
            bluetooth = enable;
        }

        //ARDUINO SERIAL PORT

        private void USBConnection(string portName, int baudRate)
        {
            if (!string.IsNullOrEmpty(portName))
            {
                arduinoPort = new SerialPort(portName, baudRate);

                try
                {
                    arduinoPort.Open();
                }
                catch (UnauthorizedAccessException)
                {
                    // Attempt to grant access to the port
                    GrantPortAccess(portName);
                    try
                    {
                        arduinoPort.Open();
                    }
                    catch (Exception ex)
                    {
                        alert.Text = "Failed to open the Arduino port. Error: " + ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    alert.Text = "Failed to open the Arduino port. Error: " + ex.Message;
                }
            }
            else
            {
                alert.Text = "Port not found.";
            }
        }

        private void BluetoothConnection(string portName, int baudRate)
        {
            if (!string.IsNullOrEmpty(portName))
            {
                serialPort1.PortName = portName;
                serialPort1.BaudRate = baudRate;
                Console.WriteLine("test");
                try
                {
                    if (!serialPort1.IsOpen) serialPort1.Open();
                    serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                }
                catch (Exception ex)
                {
                    alert.Text = "Failed to open the Bluetooth port. Error: " + ex.Message;
                    Console.WriteLine(alert.Text);
                }
            }
            else
            {
                alert.Text = "Bluetooth port not found.";
            }
        }

        int count = 0;
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                SerialPort serialPort = (SerialPort)sender;
                string receivedData = serialPort.ReadLine();
                Console.WriteLine("Data" + count + receivedData);
                count++;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Process received data as needed
            // Example: Display received data in a text box
            //receivedTextBox.Invoke((MethodInvoker)(() => receivedTextBox.Text = receivedData));
        }

        private void InitializeSerialPort()
        {
            string portName;
            if (bluetooth)
            {
                portName = BluetoothPort;
                BluetoothConnection(portName, 115200);
            }
            else
            {
                portName = USBPortName;
                USBConnection(portName, 9600);
            }
        
        }

        private void ResetAngles()
        {
            string data = $"0,0,0,0,0";
            try
            {
                arduinoPort.WriteLine(data);
            }
            catch (Exception ex)
            {
                alert.Text = ex.Message;
            }
        }

        private void Manager_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResetAngles();
        }

        private void GrantPortAccess(string portName)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
                foreach (ManagementObject obj in searcher.Get())
                {
                    object deviceIDObj = obj["DeviceID"];
                    if (deviceIDObj != null && deviceIDObj.ToString() == portName)
                    {
                        object pnpDeviceIDObj = obj["PNPDeviceID"];
                        if (pnpDeviceIDObj != null)
                        {
                            string pnpDeviceID = pnpDeviceIDObj.ToString();
                            string[] split = pnpDeviceID.Split('&');
                            if (split.Length >= 4)
                            {
                                string hardwareID = split[3];
                                ManagementObject portConfig = new ManagementObject(@"root\CIMV2",
                                    "Win32_SerialPortConfiguration.DeviceID='" + hardwareID + "'",
                                    null);
                                object[] args = { 1 };
                                portConfig.InvokeMethod("SetPowerState", args);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to grant access to the port: " + ex.Message);
            }
        }


    }
}
