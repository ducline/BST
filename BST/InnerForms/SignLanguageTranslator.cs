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


        private void button1_Click(object sender, EventArgs e)
        {
            Manager managerForm = this.Parent.Parent as Manager;

            if (managerForm != null)
            {
                // Call the OpenSearchableForm method of the Manager 
                managerForm.OpenSearchableForm(textBox1.Text, "Load", "STL");

                // Close the PredefinitionManagement form
                this.Close();


            }
}

        private void SignLanguageTranslator_Load(object sender, EventArgs e)
        {
            textBox1.Select();
        }

    }
}
