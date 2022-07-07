using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace austinCrashDiag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MemoryStream userInput = new MemoryStream();

        private void DiscordBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/efE9c9AkWy");
        }

        private void SaveStackBtn_Click(object sender, EventArgs e)
        {
            // Set the properties on SaveFileDialog1 so the user is 
            // prompted to create the file if it doesn't exist 
            // or overwrite the file if it does exist.
            SaveFileDialog1.CreatePrompt = true;
            SaveFileDialog1.OverwritePrompt = true;

            // Set the file name to myText.txt, set the type filter
            // to text files, and set the initial directory to the 
            // MyDocuments folder.
            SaveFileDialog1.FileName = "myText";
            // DefaultExt is only used when "All files" is selected from 
            // the filter box and no extension is specified by the user.
            SaveFileDialog1.DefaultExt = "txt";
            SaveFileDialog1.Filter =
                "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            SaveFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Call ShowDialog and check for a return value of DialogResult.OK,
            // which indicates that the file was saved. 
            DialogResult result = SaveFileDialog1.ShowDialog();
            Stream fileStream;

            if (result == DialogResult.OK)
            {
                // Open the file, copy the contents of memoryStream to fileStream,
                // and close fileStream. Set the memoryStream.Position value to 0 
                // to copy the entire stream. 
                fileStream = SaveFileDialog1.OpenFile();
                userInput.Position = 0;
                userInput.WriteTo(fileStream);
                fileStream.Close();
                SaveStackBtn.Text = "Stack Saved";
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {   
                //Load the temporary files
                string errorText = System.IO.File.ReadAllText(@"logs\latestError.txt");
                string errorStackText = System.IO.File.ReadAllText(@"logs\latestStack.txt");

                if (errorText != "") TextBox1.Text = errorText;
                if (errorStackText != "") RichTextBox1.Text = errorStackText;

                //Delete the files now
                System.IO.File.Delete(@"logs\latestError.txt");
                System.IO.File.Delete(@"logs\latestStack.txt");
            }
            catch
            {
                if (MessageBox.Show("Hi! I'm the crash handler for Austin Engine. When Austin crashes, I'll show a message with the reason why it crashed. Nice to meet you, fellow Austin Engine user.", "Greetings") == DialogResult.OK) Application.Exit();
            }
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RestartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"AustinEngine.exe");
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("Error: Unable to find \"AustinEngine.exe\". You changed the engine's filename, didn't you..","Error");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
