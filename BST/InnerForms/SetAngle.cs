﻿using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;
using System.Management;

namespace BST.InnerForms
{
    public partial class SetAngle : Form
    {

        private bool bluetoothValue;
        string modifiedString;
        private SerialPort USBPortSerial;
        private SerialPort bluetoothPortSerial;

        public SetAngle(string search)
        {
            InitializeComponent();
            modifiedString = search;
            
            
        }


        public void ResetFingers()
        {
            bool sendDataViaBluetooth = bluetoothValue;  // Get the boolean value from the bluetoothValue variable

            string data = $"0, 0, 0, 0, 0, {sendDataViaBluetooth}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle1 = (int)numericUpDown1.Value;
            int angle2 = (int)numericUpDown2.Value;
            int angle3 = (int)numericUpDown3.Value;
            int angle4 = (int)numericUpDown4.Value;
            int angle5 = (int)numericUpDown5.Value;

            if (USBPortSerial != null && USBPortSerial.IsOpen)
            {
                string data = $"{angle1},{angle2},{angle3},{angle4},{angle5}";
                if(bluetoothValue) bluetoothPortSerial.WriteLine(data); else USBPortSerial.WriteLine(data);
                label1.Text = "";
            }
            else
            {
                label1.Text = "Unable to send data.";
            }

        }


        private void SetAngle_Load(object sender, EventArgs e)
        {
            Manager managerForm = this.Parent.Parent as Manager;
            USBPortSerial = managerForm.USBPortSerial;
            bluetoothPortSerial = managerForm.bluetoothPortSerial;
            bluetoothValue = managerForm.bluetooth;

            if (modifiedString != "")
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


        }

        private void SetAngle_FormClosing(object sender, FormClosingEventArgs e)
        {
            //USBPortSerial.Close();
        }

        private void SetAngle_FormClosed(object sender, FormClosedEventArgs e)
        {


        }


    }
}
