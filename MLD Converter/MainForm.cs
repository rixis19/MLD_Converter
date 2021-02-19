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
using mldlib;

namespace MLD_Converter
{
    public partial class MainForm : Form
    {
        mldFunctions converter = new mldFunctions();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(fileList.Items.Count == 0))
            {
                switch (MessageBox.Show("Are you sure you want to clear the current list?", "Confirmation", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        fileList.Items.Clear();
                        break;
                    case DialogResult.No:
                        return;
                }
            }

            OpenFileDialog openFileDlg = new OpenFileDialog()
            {
                Filter = "MLD files (*.mld)|*.mld"
            };

            DialogResult result = openFileDlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                fileList.Items.Add(openFileDlg.FileName, false);
            }
        }

        private void openDir_Click(object sender, EventArgs e)
        {
            if (!(fileList.Items.Count == 0))
            {
                switch (MessageBox.Show("Are you sure you want to clear the current list?", "Confirmation", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        fileList.Items.Clear();
                        break;
                    case DialogResult.No:
                        return;
                }
            }

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
            foreach(var item in fileList.CheckedItems)
            {
                Console.WriteLine(item.ToString());
                converter.convertFile(item.ToString());
            }
        }

        private void LoadFile()
        {

        }
    }
}
