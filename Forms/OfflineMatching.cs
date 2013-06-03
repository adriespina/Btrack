using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Billetrack.Forms
{
    public partial class OfflineMatching : UserControl
    {
        #region variables
        ArrayList ImagenesAceria;
        ArrayList ImagenesAlam;
        BackgroundWorker backgroundWorker1;
        int imagenes_totales_alambron = 0;
        string pathacer, pathalam;
        Bitmap _cambioImagen = null;
        public bool _running = false;
        public CClassification classificator;
        #endregion

        public OfflineMatching()
        {
            InitializeComponent();
            this.panel3.Controls.Remove(this.splitContainer1);
            this.panel3.Controls.Add(this.spinLabel1);
            ImagenesAceria = new ArrayList();
            ImagenesAlam = new ArrayList();

            dataGridView_resultados.ColumnCount = 6;
            dataGridView_resultados.Columns[0].Name = "Imagen original";
            dataGridView_resultados.Columns[1].Name = "Imagen Emparejada";
            dataGridView_resultados.Columns[2].Name = "Puntos homografia";
            dataGridView_resultados.Columns[3].Name = "Puntos Dentro";
            dataGridView_resultados.Columns[4].Name = "Factor";
            dataGridView_resultados.Columns[5].Name = "Tiempo msec";

            InitializeBackgroundWorker();

            try
            {
                classificator = new CClassification(@"C:\Users\Cesar\Desktop\Billetrack Adrian\Codigo\Billetrack64\Billetrack\Matching.xlsx");
               // classificator = new CClassification("Matching.xlsx");

            }
            catch (Exception e)
            {
                MessageBox.Show("error creando clasificador" + e.Message);
            }
        }

        #region WORKERS
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.UserState != null)
                {
                    dataGridView_resultados.Rows.Add((string[])e.UserState);
                }
                label_estado.Text = "PROCESANDO IMAGEN Nº : " + (e.ProgressPercentage + 1).ToString() + "  DE : " + imagenes_totales_alambron.ToString();

                int valor = (int)Math.Round(((double)e.ProgressPercentage / imagenes_totales_alambron) * 100);
                if (valor >= 0 && valor <= 100)
                {
                    progressBar1.Value = valor;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                double percentage = (((double)((int)e.Result)) / (double)imagenes_totales_alambron) * 100.0;
                label_resultados.Text = "EXITO DEL " + percentage.ToString("F2") + " %. Identificadas : " + ((int)e.Result).ToString() + " de :" + imagenes_totales_alambron.ToString();
                label_estado.Text = "Procesadas todas las imagenes";
                progressBar1.Value = 100;
                _running = false;
                _play.BackgroundImage = global::Billetrack.Properties.Resources.play;
                button1.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerInfo wi = (WorkerInfo)e.Argument;
                ArrayList Dudosos = new ArrayList();
                ImagenesAlam = wi._ImagenesAlambron;
                string[] ImagenesAceriavector = wi._ImagenesAceria;
                Stopwatch watch;
                long matchTime;
                int position_max = -1, it = 0;
                CMatching match = new CMatching(8);
                resultMatching[] resultados = new resultMatching[ImagenesAceriavector.Length];
                CMetodosAuxiliares aux = new CMetodosAuxiliares();
                int matched = 0, matched_ok = 0;
                Rectangle rect = new Rectangle();

                foreach (string img in ImagenesAlam)
                {
                    if (this.backgroundWorker1.CancellationPending)
                        break;
                    position_max = -1;
                    watch = Stopwatch.StartNew();
                    //string path_recortada = img;
                    string path_recortada = img.Substring(0, img.Length - 13) + ".jpg";



                    using (Image<Gray, Byte> original = new Image<Gray, byte>(img))
                    {
                        using (Image<Gray, Byte> cortada = aux.RotateAndCropImageForBilletrack(original, -54, ref rect))
                        {

                            cortada.Save(path_recortada);

                        }
                    }

                    //position_max = match.MatchingOneToVarius(path_recortada, ImagenesAceriavector, out resultados);


                    position_max = match.MatchingOneToVarius_fast(path_recortada, ImagenesAceriavector, out resultados);
                    //classificator.InsertSetMatches(resultados, position_max);
                   
                    watch.Stop();

                    matchTime = watch.ElapsedMilliseconds;
                    if (position_max == 9999)
                    {
                        Dudosos.Add(img);
                    }
                    else
                    {

                        if (position_max >= 0)
                        {
                            //string[] row = { path_recortada.Substring(path_recortada.LastIndexOf("\\") + 1), ((string)ImagenesAceriavector[position_max]).Substring(path_recortada.LastIndexOf("\\") + 1),"0", "0","0", matchTime.ToString() };
                            string[] row = { path_recortada.Substring(path_recortada.LastIndexOf("\\") + 1), ((string)ImagenesAceriavector[position_max]).Substring(path_recortada.LastIndexOf("\\") + 1), resultados[position_max].npoints_included_homography.ToString(), resultados[position_max].inside_KeyPoints.ToString(), resultados[position_max].points_factor2.ToString(), matchTime.ToString() };
                            ImagenesAceriavector[position_max] = "matched"; //para que no vuelva a procesarla
                            matched++;
                            matched_ok++;

                            backgroundWorker1.ReportProgress(matched, row);

                        }
                        else
                        {
                            int pos_max = 0;
                            double factor_max = 0;
                            for (int i = 0; i < resultados.Length; i++)
                            {
                                if (resultados[i].points_factor2>factor_max)
                                {
                                    pos_max = i;
                                    factor_max = resultados[i].points_factor2;
                                }
                            }
                            string[] row = { path_recortada.Substring(path_recortada.LastIndexOf("\\") + 1), "MATCH NOT FOUND", resultados[pos_max].npoints_included_homography.ToString(), resultados[pos_max].inside_KeyPoints.ToString(), resultados[pos_max].points_factor2.ToString(), matchTime.ToString() };
                            matched++;
                            backgroundWorker1.ReportProgress(matched, row);
                        }

                    }
                }

                //analizo lo que fueron dudosos

                if (Dudosos.Count > 0)
                {
                    foreach (string img in Dudosos)
                    {
                        if (this.backgroundWorker1.CancellationPending)
                            break;
                        position_max = -1;

                        watch = Stopwatch.StartNew();

                        position_max = match.MatchingOneToVarius(img,ImagenesAceriavector, out resultados);

                        watch.Stop();

                        matchTime = watch.ElapsedMilliseconds;

                        if (position_max >= 0 && position_max < 8888)
                        {
                            string[] row = { img, (string)ImagenesAlam[position_max], resultados[position_max].quality.ToString(), resultados[position_max].common_KeyPoints.ToString(), resultados[position_max].inside_KeyPoints.ToString(), matchTime.ToString() };
                            ImagenesAceriavector[position_max] = "matched"; //para que no vuelva a procesarla
                            matched++;
                            matched_ok++;
                            backgroundWorker1.ReportProgress(matched, row);

                        }
                        else
                        {
                            string[] row = { img, "MATCH NOT FOUND", "0", "0", "0", matchTime.ToString() };
                            matched++;
                            backgroundWorker1.ReportProgress(matched, row);
                        }
                    }
                }
                e.Result = matched_ok;
                ImagenesAceria.Clear();
                ImagenesAlam.Clear();
                Dudosos.Clear();
                if (resultados!=null)
                {
                    foreach (resultMatching rst in resultados)
                    {
                        if (rst!=null)
                        {
                           rst.Dispose(); 
                        } 
                    } 
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region eventos raton
        private void _splitterarrow_Click(object sender, EventArgs e)
        {
            SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(200, this, "", global::Billetrack.Properties.Resources.bg22);
            b.Show();

            if (this.Controls.Contains(_controles))
            {
                _splitterarrow.Image = global::Billetrack.Properties.Resources.splitterhide1;
                this.Controls.RemoveByKey("_controles");
            }
            else
            {
                _splitterarrow.Image = global::Billetrack.Properties.Resources.splittershow1;
                this.Controls.Add(_controles);
            }
            this.Refresh();
            b.CloseLoading();
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "C:\\Users\\Cesar\\Desktop\\Billetrack Adrian\\datos"; 
            folderBrowserDialog1.ShowDialog();
            label_path_alambron.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "C:\\Users\\Cesar\\Desktop\\Billetrack Adrian\\datos";
            folderBrowserDialog1.ShowDialog();
            label_path_aceria.Text = folderBrowserDialog1.SelectedPath;
        }

        public void play_Click(object sender, EventArgs e)
        {
            if (!_running)
            {
                progressBar1.Visible = true;
                _running = true;
                label_resultados.Text = "";
                button1.Enabled = false;
                button3.Enabled = false;
                _play.BackgroundImage = global::Billetrack.Properties.Resources.stop;
                ImagenesAceria = new ArrayList();
                ImagenesAlam = new ArrayList();

                dataGridView_resultados.Rows.Clear();

               // pathacer = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_acer\";
                pathacer = label_path_aceria.Text;
                label_path_aceria.Text = pathacer;
                //  pathalam = @"C:\Users\Cesar\Desktop\billetrack_halcon\imagenes\220356_alam";
              //  pathalam = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_alam\";
                pathalam = label_path_alambron.Text;
                label_path_alambron.Text = pathalam;

                
                    pathacer = label_path_aceria.Text;
                    pathalam = label_path_alambron.Text;

                    if (!(System.IO.Directory.Exists(pathacer) && System.IO.Directory.Exists(pathalam)))   return;

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pathacer);
                System.IO.FileInfo[] files = null;
                files = di.GetFiles("*.jpg");
                foreach (System.IO.FileInfo fi in files)
                {

                    if (!fi.FullName.Contains("ORIGINAL")) ImagenesAceria.Add(fi.FullName);

                }
                di = new System.IO.DirectoryInfo(pathalam);
                files = null;
                files = di.GetFiles("*.jpg");
                foreach (System.IO.FileInfo fi in files)
                {

                    if (fi.FullName.Contains("ORIGINAL")) ImagenesAlam.Add(fi.FullName);

                }

                string[] ImagenesAceriavector = new string[ImagenesAceria.Count];
                int y = 0;
                foreach (string img2 in ImagenesAceria)
                {
                    ImagenesAceriavector[y] = img2;
                    y++;
                }
                imagenes_totales_alambron = ImagenesAlam.Count;
                label_estado.Text = "PROCESANDO IMAGEN Nº : " + 1 + "  DE : " + imagenes_totales_alambron.ToString();

                backgroundWorker1.RunWorkerAsync(new WorkerInfo(ImagenesAlam, ImagenesAceriavector));
            }
            else
            {
                //PARAR!!
                backgroundWorker1.CancelAsync();
                _running = false;
                _play.BackgroundImage = global::Billetrack.Properties.Resources.play;
            }
        }

        private void dataGridView_resultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView_resultados.Rows.Count > 0)
                {
                    if ((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString() == "MATCH NOT FOUND")
                    {
                        _cambioImagen = new Bitmap(this.splitContainer1.Panel1.Width, this.splitContainer1.Panel1.Height);
                        this.splitContainer1.Panel1.DrawToBitmap(_cambioImagen, this.splitContainer1.Panel1.ClientRectangle);
                        // Show the image of the panel in a pictureBox

                        SpinPlatform.Controls.spinLoadingScreenForm c = _cambioImagen == null ? new SpinPlatform.Controls.spinLoadingScreenForm(30, this.splitContainer1.Panel1, "", null) : new SpinPlatform.Controls.spinLoadingScreenForm(200, this.splitContainer1.Panel1 , "", _cambioImagen, ImageLayout.Stretch);
                        c.BackColor = Color.FromArgb(80, 80, 80);
                        c.Show();

                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(200, 200))
                        {
                            pictureBox_Prueba._cPicture.Image = max_image.ToBitmap();
                        }
                        string cortada = pathalam + "\\" + (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original._cPicture.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar._cPicture.Image = original.ToBitmap();
                        }

                        c.CloseLoading();
                    }
                    else
                    {
                        _cambioImagen = new Bitmap(this.splitContainer1.Panel1.Width, this.splitContainer1.Panel1.Height);
                        this.splitContainer1.Panel1.DrawToBitmap(_cambioImagen, this.splitContainer1.Panel1.ClientRectangle);
                        // Show the image of the panel in a pictureBox

                        SpinPlatform.Controls.spinLoadingScreenForm c = _cambioImagen == null ? new SpinPlatform.Controls.spinLoadingScreenForm(30, this.splitContainer1.Panel1, "", null) : new SpinPlatform.Controls.spinLoadingScreenForm(200, this.splitContainer1.Panel1, "", _cambioImagen, ImageLayout.Stretch);
                        c.BackColor = Color.FromArgb(80, 80, 80);
                        c.Show();

                        string matched = pathacer +"\\"+ (string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(matched))
                        {
                            pictureBox_Prueba._cPicture.Image = max_image.ToBitmap();
                        }
                        string cortada = pathalam + "\\" + (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original._cPicture.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar._cPicture.Image = original.ToBitmap();
                        }

                        c.CloseLoading();

                        
                    }

                }
            }
            catch (Exception)
            {


            }
        }

        private void dataGridView_resultados_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (dataGridView_resultados.Rows.Count > 0)
                {

                    using (Image<Gray, Byte> max_image = new Image<Gray, byte>(200, 200))
                    {
                        pictureBox_Prueba._cPicture.Image = max_image.ToBitmap();
                    }
                    if ((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString() == "MATCH NOT FOUND")
                    {
                        if (panel3.Controls.Contains(spinLabel1))
                        {
                            SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel3, "", global::Billetrack.Properties.Resources.bg22);
                            a.Show();
                            panel3.Controls.Clear();
                            panel3.Controls.Add(splitContainer1);
                            panel3.Refresh();
                            a.CloseLoading();
                        }
                        SpinPlatform.Controls.spinLoadingScreenForm c = null;
                        string cortada = pathalam + "\\" + (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original._cPicture.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar._cPicture.Image = original.ToBitmap();
                        }
                    }
                    else
                    {
                        if (panel3.Controls.Contains(spinLabel1))
                        {
                            SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel3, "", global::Billetrack.Properties.Resources.bg22);
                            a.Show();
                            panel3.Controls.Clear();
                            panel3.Controls.Add(splitContainer1);
                            panel3.Refresh();
                            a.CloseLoading();
                        }
                        SpinPlatform.Controls.spinLoadingScreenForm c = null;

                        string matched = pathacer +"\\"+ (string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString();
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>(matched))
                        {
                            pictureBox_Prueba._cPicture.Image = max_image.ToBitmap();
                        }
                        string cortada = pathalam + "\\" + (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original._cPicture.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar._cPicture.Image = original.ToBitmap();
                        }
                    }
 

                }
            }
            catch (Exception)
            {


            }
        }

        private void _play_MouseEnter(object sender, EventArgs e)
        {
            if(_running)
                _play.BackgroundImage = global::Billetrack.Properties.Resources.stopover;
            else
                _play.BackgroundImage = global::Billetrack.Properties.Resources.playover;
        }

        private void _play_MouseLeave(object sender, EventArgs e)
        {
            if (_running)
                _play.BackgroundImage = global::Billetrack.Properties.Resources.stop;
            else
                _play.BackgroundImage = global::Billetrack.Properties.Resources.play;
        }
    }

    public class WorkerInfo
    {
        public string[] _ImagenesAceria;
        public ArrayList _ImagenesAlambron;

        public WorkerInfo(ArrayList ImagenesAlambron, string[] ImagenesAceria)
        {
            _ImagenesAlambron = ImagenesAlambron;
            _ImagenesAceria = ImagenesAceria;
        }
    }
}
