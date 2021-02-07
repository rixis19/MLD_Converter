using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MLD_Converter
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog()
            {
                Filter = "MLD files (*.mld)|*.mld"
            };

            DialogResult result = openFileDlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                fileList.Items.Add(Path.GetFileName(openFileDlg.FileName), false);
            }
        }

        private void openDir_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog openFolderDlg = new FolderBrowserDialog();

            DialogResult result = openFolderDlg.ShowDialog(this);
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFolderDlg.SelectedPath))
            {
               string[] files = Directory.GetFiles(openFolderDlg.SelectedPath, "*.mld");

                foreach (var item in files)
                {
                    fileList.Items.Add(Path.GetFileName(item), false);
                }
            }
        }

        private void batchConv_Click(object sender, EventArgs e)
        {

        }

        private void selectConv_Click(object sender, EventArgs e)
        {

        }

        private void LoadFile()
        {

        }
    }
}
