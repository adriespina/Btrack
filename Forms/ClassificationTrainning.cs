using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Billetrack.Forms
{
    public partial class ClassificationTrainning : UserControl
    {
        public ClassificationTrainning()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            folderBrowserDialog1.ShowDialog();
            label_path_origin.Text = folderBrowserDialog1.SelectedPath;
            try
            {
                System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(folderBrowserDialog1.SelectedPath);
                System.IO.FileInfo[] files = rootDir.GetFiles("*.jpg");
                foreach (System.IO.FileInfo file in files)
                {
                    if (!file.Name.Contains("ORIGINAL")) dataGridView_ORIGIN.Rows.Add(file.Name);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            folderBrowserDialog1.ShowDialog();
            label_path_destiny.Text = folderBrowserDialog1.SelectedPath;
            try
            {
                System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(folderBrowserDialog1.SelectedPath);
                System.IO.FileInfo[] files = rootDir.GetFiles("*.jpg");
                foreach (System.IO.FileInfo file in files)
                {
                    if (!file.Name.Contains("ORIGINAL")) dataGridView_DESTINY.Rows.Add(file.Name);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
