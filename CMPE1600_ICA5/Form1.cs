using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CMPE1600_ICA5
{
    public partial class Form1 : Form
    {
        bool textChange = false;
        public Form1()
        {
            InitializeComponent();
            Text = "MiniEdit";
        }

        private void UI_NewMenu_Click(object sender, EventArgs e)
        {
            if (textChange != true)
            {
                UI_TextBox.Clear();
                Text = "Untitled.txt";
            }
            else
            {
                MessageBox.Show("Would you like to save first?", "ICA 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);                
            }
        }

        private void UI_TextBox_TextChanged(object sender, EventArgs e)
        {
            textChange = true;
            
        }

        private void UI_SaveAsMenu_Click(object sender, EventArgs e)
        {
            StreamWriter swOutputFile;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
    }
}
