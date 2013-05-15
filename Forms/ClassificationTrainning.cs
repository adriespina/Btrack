using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Billetrack.Forms
{
    public partial class ClassificationTrainning : UserControl
    {

        string img_origin = null;
        string img_destiny = null;
        CMatching match;
        CClassification classificator;
        public ClassificationTrainning()
        {
            match = new CMatching(8);
            classificator = new CClassification(@"C:\Users\Cesar\Desktop\Billetrack Adrian\Codigo\Billetrack64\Billetrack\Matching.xlsx");
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

                if (dataGridView_ORIGIN.Rows.Count > 0)
                {
                    if (dataGridView_ORIGIN.Rows[0].Cells[0].Value != null)
                    {
                        string img = label_path_origin.Text + "\\" + (string)dataGridView_ORIGIN.Rows[0].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(img))
                        {
                            picture_origin._cPicture.Image = max_image.ToBitmap();
                        }
                    }
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
                if (dataGridView_DESTINY.Rows.Count > 0)
                {
                    if (dataGridView_DESTINY.Rows[0].Cells[0].Value != null)
                    {
                        string img = label_path_destiny.Text + "\\" + (string)dataGridView_DESTINY.Rows[0].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(img))
                        {
                            picture_destiny._cPicture.Image = max_image.ToBitmap();
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView_ORIGIN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_ORIGIN.Rows.Count > 0)
            {
                if (dataGridView_ORIGIN.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    try
                    {
                        img_origin = label_path_origin.Text + "\\" + (string)dataGridView_ORIGIN.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(img_origin))
                        {
                            picture_origin._cPicture.Image = max_image.ToBitmap();
                        }
                    }
                    catch (Exception)
                    {

                        img_origin = null;
                    }
                }
            }
        }

        private void dataGridView_DESTINY_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_DESTINY.Rows.Count > 0)
            {
                if (dataGridView_DESTINY.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    try
                    {
                        img_destiny = label_path_destiny.Text + "\\" + (string)dataGridView_DESTINY.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(img_destiny))
                        {
                            picture_destiny._cPicture.Image = max_image.ToBitmap();
                        }
                    }
                    catch (Exception)
                    {

                        img_destiny = null;
                    }
                }
            }
        }

        private void button_NEWMATCH_Click(object sender, EventArgs e)
        {
            if (img_destiny != null && img_origin != null)
            {
                resultMatching result = match.MatchingOneToOne(img_origin, img_destiny, CSurf.MODE_CESAR);
                classificator.InsertMatch(result);
                if (checkBox_correction.Checked)
                {
                    classificator.DeleteRowsContaining(result.npoints_included_homography.ToString(), "C");
                }
            }
            else MessageBox.Show("No se han seleccionado imagenes para procesar");
        }

    }
}
