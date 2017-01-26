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
        string saveFileName = null;
        public Form1()
        {
            InitializeComponent();
            Text = "MiniEdit";
        }

        //Clears text box and prompts to save if there have been changes 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textChange != true)
            {
                UI_TextBox.Clear();
                Text = "Untitled.txt";
                textChange = false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Would you like to save first?", "MiniEdit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    SaveAs();
                }
                else if (result == DialogResult.No)
                {
                    UI_TextBox.Clear();
                    Text = "Untitled.txt";
                }

                textChange = false;
            }
           
        }

        //Detects change in textbox and sets global variable to true
        private void UI_TextBox_TextChanged(object sender, EventArgs e)
        {
            textChange = true;
        }

        //Saves the file and makes form title the new file name
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            saveFileName = SaveAs();
            textChange = false;

            string[] pathSplit = saveFileName.Split(new char[] { '\\' });

            Text = pathSplit[pathSplit.Count() - 1];
        }

        //Opens a file and makes that file the name of the form
        //Also checks for edits and prompts to save before opening new file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
                    SaveAs();
                }
                else if (result == DialogResult.No)
                {
                    UI_TextBox.Clear();
                    Text = "Untitled.txt";
                }

                textChange = false;
            }
            LoadFile();
            
            textChange = false;

        }

        //The actual mechanics of saving the file
        //using streamwriter
        private string SaveAs()
        {
            string file = null;
            StreamWriter swOutputFile;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = saveFileDialog1.FileName;
                try
                {
                    swOutputFile = new StreamWriter(saveFileDialog1.FileName);
                    foreach (string n in UI_TextBox.Lines)
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
            return file;
        }

        //The actual mechanics of loading a file using
        //streamreader
        private void LoadFile()
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
                saveFileName = openFileDialog1.FileName;
            }
        }


        //Closes file, and another event will invoke a save dialog if there
        //are changes
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Detects if a save as has been performed. if not, saves like save as
        //if there has already been a save, will save to the current file
        //without prompt
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileName == null)
            {
                saveFileName = SaveAs();
                textChange = false;

                string[] pathSplit = saveFileName.Split(new char[] { '\\' });

                Text = pathSplit[pathSplit.Count() - 1];
            }
            else
            {
                StreamWriter swOutputFile;
                try
                {
                    swOutputFile = new StreamWriter(saveFileName);
                    foreach (string n in UI_TextBox.Lines)
                    {
                        swOutputFile.WriteLine(n);
                    }
                    swOutputFile.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "MiniEdit", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Asterisk);
                }
            }

            textChange = false;
        }

        //When the form closes, checks for unsaved changes using
        //bool flag and prompts user to save
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textChange == true)
            {
                DialogResult result = MessageBox.Show("Would you like to save first?", "MiniEdit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    SaveAs();
                    textChange = false;                    
                }
                else if (result == DialogResult.No)
                {
                    textChange = false;                    
                }

            }
        }
    }
}
