using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Collections;
using Emgu.CV.UI;
using System.Threading;
using System.IO;
using Billetrack;
using DALSA.SaperaLT.SapClassBasic;
using System.Dynamic;


namespace Billetrack

{
   // delegate void delegateDisplayResults(dynamic result);
    public partial class Form1 : Form
    {
        BilletrackDispatcher dispatch;
        delegateDisplayResults d_DisplayResults;  //puntero a la funcion de pintar
        ArrayList ImagenesAceria;
        ArrayList ImagenesAlam;
        BackgroundWorker backgroundWorker1;
        int imagenes_totales_alambron = 0;
        CGenieCamera camara;       

        public Form1()
        {
            InitializeComponent();
            ImagenesAceria = new ArrayList();
            ImagenesAlam = new ArrayList();


            dataGridView_resultados.ColumnCount = 6;
            dataGridView_resultados.Columns[0].Name = "Imagen original";
            dataGridView_resultados.Columns[1].Name = "Imagen Emparejada";
            dataGridView_resultados.Columns[2].Name = "Porcentaje";
            dataGridView_resultados.Columns[3].Name = "Puntos en comun";
            dataGridView_resultados.Columns[4].Name = "Puntos Dentro";
            dataGridView_resultados.Columns[5].Name = "Tiempo msec";

            InitializeBackgroundWorker();
            dispatch = new BilletrackDispatcher();
            dispatch.NewResultEvent += new SpinPlatform.Dispatcher.ResultEventHandler(dispatch_NewResultEvent);
            d_DisplayResults = new delegateDisplayResults(DisplayResults);

            dynamic ConfigData = new ExpandoObject();
            dispatch.Init(ConfigData);
        }

        void dispatch_NewResultEvent(object sender, SpinPlatform.Data.DataEventArgs res)
        {

            try
            {
                this.BeginInvoke(d_DisplayResults, res.DataArgs);
            }
            catch (Exception)
            {
            }
        }
        void DisplayResults(dynamic datos)
        {
            if (datos.TRIErrors == "")
            {
                foreach (string data in datos.TRIReturnedData)
                {
                    switch (data)
                    {
                        case "CurrentImage":

                            CurrentImage img = (CurrentImage)datos.CurrentImage;
                            pictureBox_sincortar.Image = img.Image.ToBitmap();
                            img.Dispose();
                            break;

                        case "MatchInfo":

                            Match matched = (Match)datos.MatchedInfo;
                            pictureBox_Img_Original.Image = matched.Image_Cropped.ToBitmap();
                            if (matched.Image_Matched != null) pictureBox_Prueba.Image = matched.Image_Matched.ToBitmap();
                            else
                            {
                                using (Image<Gray,byte> imgen=new Image<Gray,byte>(200,200))
                                {
                                     pictureBox_Prueba.Image = imgen.ToBitmap();
                                }
                            }
                            matched.Dispose();
                            break;
                    }
                }
            }
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;            
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

                    position_max = match.MatchingOneToVarius(path_recortada, ref ImagenesAceriavector, out resultados);

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
                            string[] row = { path_recortada, (string)ImagenesAceriavector[position_max], resultados[position_max].quality.ToString(), resultados[position_max].common_KeyPoints.ToString(), resultados[position_max].inside_KeyPoints.ToString(), matchTime.ToString() };
                            ImagenesAceriavector[position_max] = "matched"; //para que no vuelva a procesarla
                            matched++;
                            matched_ok++;

                            backgroundWorker1.ReportProgress(matched, row);

                        }
                        else
                        {
                            string[] row = { path_recortada, "MATCH NOT FOUND", "0", "0", "0", matchTime.ToString() };
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
                        position_max = -1;

                        watch = Stopwatch.StartNew();

                        position_max = match.MatchingOneToVarius(img, ref ImagenesAceriavector, out resultados);

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

            }
            catch (Exception)
            {

                throw;
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ImagenesAceria = new ArrayList();
            ImagenesAlam = new ArrayList();

            dataGridView_resultados.Rows.Clear();
            string pathacer, pathalam;
            pathacer = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_acer\";
            //  pathalam = @"C:\Users\Cesar\Desktop\billetrack_halcon\imagenes\220356_alam";
            pathalam = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_alam\";

            //if (label_path_aceria.Text != "." && label_path_alambron.Text != ".")
            //{
            //    pathacer = label_path_aceria.Text;
            //    pathalam = label_path_alambron.Text;
            //}
            //else return;


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

        private void dataGridView_resultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView_resultados.Rows.Count > 0)
                {
                    using (Image<Gray, Byte> max_image = new Image<Gray, byte>(200, 200))
                    {
                        pictureBox_Prueba.Image = max_image.ToBitmap();
                    }
                    if ((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString() == "MATCH NOT FOUND")
                    {
                        string cortada = (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar.Image = original.ToBitmap();
                        }
                    }
                    else
                    {
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString()))
                        {
                            pictureBox_Prueba.Image = max_image.ToBitmap();
                        }
                        string cortada = (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar.Image = original.ToBitmap();
                        }
                    }

                }
            }
            catch (Exception)
            {


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "C:\\Users\\Cesar\\Desktop";
            folderBrowserDialog1.ShowDialog();
            label_path_alambron.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "C:\\Users\\Cesar\\Desktop";
            folderBrowserDialog1.ShowDialog();
            label_path_aceria.Text = folderBrowserDialog1.SelectedPath;
        }

        private void dataGridView_resultados_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            try
            {
                if (dataGridView_resultados.Rows.Count > 0)
                {
                    using (Image<Gray, Byte> max_image = new Image<Gray, byte>(200, 200))
                    {
                        pictureBox_Prueba.Image = max_image.ToBitmap();
                    }
                    if ((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString() == "MATCH NOT FOUND")
                    {
                        string cortada = (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar.Image = original.ToBitmap();
                        }
                    }
                    else
                    {
                        using (Image<Gray, Byte> max_image = new Image<Gray, byte>((string)dataGridView_resultados.Rows[e.RowIndex].Cells[1].Value.ToString()))
                        {
                            pictureBox_Prueba.Image = max_image.ToBitmap();
                        }
                        string cortada = (string)dataGridView_resultados.Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (Image<Gray, Byte> cropped = new Image<Gray, byte>(cortada))
                        {
                            pictureBox_Img_Original.Image = cropped.ToBitmap();
                        }
                        string orig = cortada.Substring(0, cortada.Length - 4) + "_ORIGINAL.jpg";
                        using (Image<Gray, Byte> original = new Image<Gray, byte>(orig))
                        {
                            pictureBox_sincortar.Image = original.ToBitmap();
                        }
                    }

                }
            }
            catch (Exception)
            {


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\Cesar\Desktop\billetrack_halcon\imagenes\220356_alam\2012-02-29 01-33-21 220356_09_06_ORIGINAL.jpg";
            CMetodosAuxiliares aux = new CMetodosAuxiliares();


            using (Image<Gray, Byte> original = new Image<Gray, byte>(path))
            {
                Rectangle rect = new Rectangle();

                using (Image<Gray, Byte> cortada = aux.RotateAndCropImageForBilletrack(original, -54, ref rect))
                {

                    pictureBox_sincortar.Image = original.ToBitmap();
                    pictureBox_Img_Original.Image = cortada.ToBitmap();

                }


            }
        }

        private void button_camara_Click(object sender, EventArgs e)
        {

            try
            {
                camara = new CGenieCamera();
                if (camara.GrabAsync() == 1) timer1.Enabled = true;
                else MessageBox.Show("La Camara esta Desconectada");
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message);
            }
          
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (camara != null) camara.Close();
            dispatch.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Image<Gray, byte> Image = camara.GetImage())
            {
                pictureBox_sincortar.Image = Image.ToBitmap();
            }
        }

        private void button_hist_Click(object sender, EventArgs e)
        {
            int AnchoHist, MaxValue, PosPtoSaturation, posmax;
            int[] bin = new int[5];
            CMetodosAuxiliares aux = new CMetodosAuxiliares();
            using (Image<Gray, byte> Image = new Image<Gray, byte>(@"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\221937_alam\2012-09-05 19-00-07 221937_06_06_ORIGINAL.jpg"))
            {
                DenseHistogram Histogram = aux.GetMaxHistogram(Image, false, out AnchoHist, out MaxValue, out posmax, ref bin, out PosPtoSaturation);
                pictureBox_sincortar.Image = Image.ToBitmap();


                chart1.Series[0].Points.Clear();
                foreach (float val in Histogram.MatND.ManagedArray)
                {
                    chart1.Series[0].Points.AddY(val);
                }
               

                Histogram.Dispose();
                aux.StampTimeAndSaveImg(Image,true);

                imgStats stats = aux.CalculateStatsImage(Image, 0);
            }
        }

        private void button_startDispathcher_Click(object sender, EventArgs e)
        {
            dispatch.Start();
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
