using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpClam;

namespace CSharpSample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog1.FileName;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() == "")
            {
                MessageBox.Show(this, "Path is empty!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); //MsgBox("Path is empty!", vbExclamation, "Warning!");
            }
            else if (!System.IO.File.Exists(TextBox1.Text))
            {
                MessageBox.Show(this, "File not found!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); //MsgBox("File not found!", vbExclamation, "Warning!");
            }

            //This database path is relative. Change this line according to 
            //your ClamAV database directory.
            string database_path = Environment.CurrentDirectory + "\\database";

            ManagedClam clam = new ManagedClam();
            uint loaded_signature = 0;
            
            {
                ListBox1.Items.Clear();
                //Must set this first!
                clam.DatabasePath = database_path;
                clam.LoadClamAV(DatabaseOptions.CL_DB_STDOPT, ManagedClam.CL_INIT_STDOPT, ref loaded_signature);

                ListBox1.Items.Add("Initializing ClamAV Engine...");
                ListBox1.Items.Add(string.Format("Loaded signature: {0}", loaded_signature));
                ListBox1.Items.Add("");
                ListBox1.Items.Add("Start scanning...");

                ScanResult result = clam.ScanFile(TextBox1.Text, ScanOptions.CL_SCAN_STDOPT);
                ListBox1.Items.Add(string.Format("Elapsed time (in ms): {0}", result.ElapsedTime));
                ListBox1.Items.Add(string.Format("Fullpath: {0}", result.FullPath));
                ListBox1.Items.Add(string.Format("Scanned data (in MB): {0}", result.ScannedData));
                ListBox1.Items.Add(string.Format("Scan time: {0}", result.ScanTime.ToString()));
                ListBox1.Items.Add(string.Format("Virus name: {0}", result.VirusName));

                ListBox1.Items.Add("Dispose all allocated memory...");
                clam.FreeMemory();

                ListBox1.Items.Add("");
                ListBox1.Items.Add("Scan has been completed.");
            }

            //Optional
            clam = null;
        }
    }
}
