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
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textChange != true)
            {
                UI_TextBox.Clear();
                Text = "Untitled.txt";
            }
            else
            {
                DialogResult result = MessageBox.Show("Would you like to save first?", "MiniEdit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                
                if (result == DialogResult.Yes)
                {

                }
                else if (result == DialogResult.No)
                {
                    UI_TextBox.Clear();
                    Text = "Untitled.txt";
                }
                else if (result == DialogResult.Cancel)
                {

                }
            }
        }
       

        private void UI_TextBox_TextChanged(object sender, EventArgs e)
        {
            textChange = true;
            
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StreamWriter swOutputFile;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    swOutputFile = new StreamWriter(saveFileDialog1.FileName);
                    foreach(string n in UI_TextBox.Lines)
                    {
                        swOutputFile.WriteLine(n);
                    }
                    swOutputFile.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "MiniEdit", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Asterisk);
                }
                Text = saveFileDialog1.FileName;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            int sizeCounter = 0;
            int index = 0;
            string input = null;
            StreamReader srInputFile;
            List<string> middleMan = new List<string>();
            string[] openArray;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Text = openFileDialog1.SafeFileName;
                try
                {
                    srInputFile = new StreamReader(openFileDialog1.SafeFileName);
                    while ((input = srInputFile.ReadLine()) != null)
                    {
                        middleMan.Add(input);
                        sizeCounter++;
                    }
                    srInputFile.Close();
                    openArray = new string[sizeCounter];

                    for (index = 0; index < openArray.Length; index++)
                    {
                        openArray[index] = middleMan[index];
                    }

                    UI_TextBox.Lines = openArray;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ICA4", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
