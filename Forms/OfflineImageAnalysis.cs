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
using System.Threading;
using System.Dynamic;
using SpinPlatform.Errors;

namespace Billetrack.Forms
{
    public partial class OfflineImageAnalysis : UserControl
    {
        int AnchoHist, MaxValue, PosPtoSaturation, posmax;
        int AnchoHist2, MaxValue2, PosPtoSaturation2, posmax2;
        int[] bin = new int[5];
        int[] bin2 = new int[5];
        CGenieCamera Camera;
        CMetodosAuxiliares aux = new CMetodosAuxiliares();
        Image<Gray, byte> _CurrentImagen;
        BackgroundWorker backgroundWorker1;
        bool _running = false;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), DefaultValue("Image")]
        [Bindable(true)]
        [Description("Image to be processed")]
        [Category("Spin")]
        public Image<Gray, byte> Image
        {

            get { return _CurrentImagen; }
            set
            {
                try
                {
                    if (_CurrentImagen != null) _CurrentImagen.Dispose();
                    _CurrentImagen = ((Image<Gray, byte>)value).Copy();
                    AnalizarImagen();
                }
                catch { }
            }
        }
        public OfflineImageAnalysis()
        {
            InitializeComponent();
            //_CurrentImagen = new Image<Gray, byte>(200, 200);
            InitializeBackgroundWorker();
        }

        public void CloseCamera()
        {
            if(_running)
            {
                _running=false;
                if(Camera!=null)Camera.Close();
            }
        }

        public void OpenCamera()
        {
            if (checkBox_Camera.Checked)
            {
                try
                {
                    Camera = new CGenieCamera(1, 0, true);
                    Camera.FrameRate = 15000;
                    Camera.GrabAsync();
                }
                catch (Exception)
                {

                    checkBox_File.Checked = true;
                    checkBox_Camera.Checked = false;
                    _running = false;
                    if (Camera != null) Camera.Close();
                    return;
                }
                label_framerate.Text = Camera.FrameRate.ToString();
                exposure_updown.Value = Camera.ExposureTime;
                _running = true;
                backgroundWorker1.RunWorkerAsync();
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
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_running)
                {
                    backgroundWorker1.RunWorkerAsync();

                    //actualizo los labels
                    label_keypoints_ori.Text = ((dynamic)e.Result).label_keypoints_ori;
                    label_width_ori.Text = ((dynamic)e.Result).label_width_ori;
                    label_height_ori.Text = ((dynamic)e.Result).label_height_ori;
                    label_avg_ori.Text = ((dynamic)e.Result).label_avg_ori;
                    label_std_ori.Text = ((dynamic)e.Result).label_std_ori;
                    label_sum_ori.Text = ((dynamic)e.Result).label_sum_ori;
                    label_percent_height_ori.Text = ((dynamic)e.Result).label_percent_height_ori;
                    label_percent_nonzero_ori.Text = ((dynamic)e.Result).label_percent_nonzero_ori;
                    label_focus_ori.Text = ((dynamic)e.Result).label_focus_ori;
                    label_exposure_ori.Text = ((dynamic)e.Result).label_exposure_ori;
                    label_nonzero_avg_ori.Text = ((dynamic)e.Result).label_nonzero_avg_ori;

                    label_max_hist_ori.Text = ((dynamic)e.Result).label_max_hist_ori;
                    label_pto_sat_ori.Text = ((dynamic)e.Result).label_pto_sat_ori;
                    if (((IDictionary<string, object>)e.Result).ContainsKey("ExposureTime"))
                        exposure_updown.Value = ((dynamic)e.Result).ExposureTime;
                    //actualizo los labels
                    label_keypoints_crop.Text = ((dynamic)e.Result).label_keypoints_crop;

                    label_width_crop.Text = ((dynamic)e.Result).label_width_crop;
                    label_height_crop.Text = ((dynamic)e.Result).label_height_crop;
                    label_avg_crop.Text = ((dynamic)e.Result).label_avg_crop;
                    label_std_crop.Text = ((dynamic)e.Result).label_std_crop;
                    label_sum_crop.Text = ((dynamic)e.Result).label_sum_crop;
                    label_percent_height_crop.Text = ((dynamic)e.Result).label_percent_height_crop;
                    label_percent_nonzero_crop.Text = ((dynamic)e.Result).label_percent_nonzero_crop;
                    label_focus_crop.Text = ((dynamic)e.Result).label_focus_crop;
                    label_exposure_crop.Text = ((dynamic)e.Result).label_exposure_crop;
                    label_nonzero_avg_crop.Text = ((dynamic)e.Result).label_nonzero_avg_crop;
                    label_max_hist_crop.Text = ((dynamic)e.Result).label_max_hist_crop;
                    label_pto_sat_crop.Text = ((dynamic)e.Result).label_pto_sat_crop;

                    //pinto el histograma y la imagen

                    picture_original._cPicture.Image = ((dynamic)e.Result).OriginalImage;
                    chart_original.Series[0].Points.Clear();
                    foreach (float val in ((dynamic)e.Result).Histograma)
                    {
                        chart_original.Series[0].Points.AddY(val);
                    }
                    picture_cropped._cPicture.Image = ((dynamic)e.Result).CroppedImage;
                    chart_cropped.Series[0].Points.Clear();
                    foreach (float val in ((dynamic)e.Result).HistogramaCropped)
                    {
                        chart_cropped.Series[0].Points.AddY(val);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (Image<Gray, Byte> camera_image = Camera.GetImage())
            {
                dynamic resultado = new ExpandoObject();
                try
                {
                    if (camera_image != null)
                    {
                        try
                        {
                            //calculo el histograma de la imagen original
                            DenseHistogram Histogram = aux.GetMaxHistogram(camera_image, false, out AnchoHist, out MaxValue, out posmax, ref bin, out PosPtoSaturation);
                            //claculo las estadisticos de la imagen original
                            imgStats stats = aux.CalculateStatsImage(camera_image, 0);
                            //calculo los ptos de interes de de la imagen original
                            double val = 10;
                            try{val = (double)hessian_threshold_updown.Value;}catch{}
                            CSurf surf_image = new CSurf(camera_image,val);



                            //Si esta marcado calculo el autoexposure de la camara 
                            if (checkBox_autoexposure.Checked && checkBox_Camera.Checked)
                            {
                                if (picture_original.ROI == null || picture_original.ROI.Width == 0 || picture_original.ROI.Height == 0)
                                    picture_original.ROI = new Rectangle(10, 10, _CurrentImagen.Width - 30, _CurrentImagen.Height - 30);

                                double media = aux.GetMeanOnWindow(camera_image, picture_original.ROI.Y, picture_original.ROI.X, picture_original.ROI.Height, picture_original.ROI.Width);
                                Camera.ExposureTime = aux.CalculateExposureTimeSimple((int)media, (int)reference_updown.Value, Camera.ExposureTime);
                                resultado.ExposureTime = Camera.ExposureTime;
                            }

                            //si esta marcado el checkbox pinto los ptos de interes
                            if (checkBox_interestpoint_ori.Checked)
                            {
                                Image<Rgb, byte> image_to_process = camera_image.Convert<Rgb, byte>();
                                PointF p;
                                Point init, fin;
                                for (int i1 = 0; i1 < surf_image.m_pKeyPoints.Size; i1++)
                                {

                                    MKeyPoint k = surf_image.m_pKeyPoints[i1];
                                    p = k.Point;
                                    init = new Point((int)p.X - 4, (int)p.Y - 4);
                                    fin = new Point((int)p.X + 4, (int)p.Y + 4);
                                    image_to_process.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);
                                    init = new Point((int)p.X - 4, (int)p.Y + 4);
                                    fin = new Point((int)p.X + 4, (int)p.Y - 4);
                                    image_to_process.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);

                                }
                                resultado.OriginalImage = image_to_process.ToBitmap();
                                image_to_process.Dispose();
                            }
                            else
                            {
                                resultado.OriginalImage = camera_image.ToBitmap();
                            }


                            //actualizo los labels
                            resultado.label_keypoints_ori = surf_image.m_pKeyPoints.Size.ToString();
                            resultado.label_width_ori = stats.Width.ToString();
                            resultado.label_height_ori = stats.Height.ToString();
                            resultado.label_avg_ori = stats.Media.ToString("F2");
                            resultado.label_std_ori = stats.Std.ToString("F2");
                            resultado.label_sum_ori = stats.Suma.ToString();
                            resultado.label_percent_height_ori = stats.percHeightHistNonZero.ToString("F2");
                            resultado.label_percent_nonzero_ori = stats.percNonZero.ToString("F2");
                            resultado.label_focus_ori = stats.Param_Focus_normalizefreq.ToString("F2");
                            resultado.label_exposure_ori = stats.ExposureTime.ToString();
                            resultado.label_nonzero_avg_ori = stats.MediaNonZero.ToString("F2");

                            resultado.label_max_hist_ori = posmax.ToString();
                            resultado.label_pto_sat_ori = PosPtoSaturation.ToString();

                            //pinto el histograma de de la imagen original
                            float[] Histograma = new float[Histogram.MatND.ManagedArray.Length];
                            Histogram.MatND.ManagedArray.CopyTo(Histograma,0);
                            resultado.Histograma = Histograma;

                            Rectangle rect = new Rectangle();

                            //recorto la imagen 
                            int rotate = 0;
                            try {rotate = int.Parse(textBox_angle.Text); }
                            catch { }
                            using (Image<Gray, Byte> cortada = aux.RotateAndCropImageForBilletrack(camera_image, rotate, ref rect))
                            {


                                //calculo el histograma de la imagen recortada
                                DenseHistogram Histogram2 = aux.GetMaxHistogram(cortada, false, out AnchoHist2, out MaxValue2, out posmax2, ref bin2, out PosPtoSaturation2);
                                // calculo los estadisticos de la imagen recortada
                                imgStats stats2 = aux.CalculateStatsImage(cortada, 0);
                                // calculo los ptos de interes de la imagen recortada
                                val = 10;
                                try { val = (double)hessian_threshold_updown.Value; }
                                catch { }
                                CSurf surf_image2 = new CSurf(cortada, val);
                                //si esta marcado el checkbox pinto los ptos de interes
                                if (checkBox_interestpoint_crop.Checked)
                                {
                                    Image<Rgb, byte> image_to_process2 = cortada.Convert<Rgb, byte>();
                                    PointF p;
                                    Point init, fin;
                                    for (int i1 = 0; i1 < surf_image2.m_pKeyPoints.Size; i1++)
                                    {

                                        MKeyPoint k = surf_image2.m_pKeyPoints[i1];
                                        p = k.Point;
                                        init = new Point((int)p.X - 2, (int)p.Y - 2);
                                        fin = new Point((int)p.X + 2, (int)p.Y + 2);
                                        image_to_process2.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);
                                        init = new Point((int)p.X - 2, (int)p.Y + 2);
                                        fin = new Point((int)p.X + 2, (int)p.Y - 2);
                                        image_to_process2.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);

                                    }
                                    resultado.CroppedImage = image_to_process2.ToBitmap();
                                    image_to_process2.Dispose();
                                }
                                else
                                {
                                    resultado.CroppedImage = cortada.ToBitmap();
                                }
                                //actualizo los labels
                                resultado.label_keypoints_crop = surf_image2.m_pKeyPoints.Size.ToString();
                                resultado.label_width_crop = stats2.Width.ToString();
                                resultado.label_height_crop = stats2.Height.ToString();
                                resultado.label_avg_crop = stats2.Media.ToString("F2");
                                resultado.label_std_crop = stats2.Std.ToString("F2");
                                resultado.label_sum_crop = stats2.Suma.ToString();
                                resultado.label_percent_height_crop = stats2.percHeightHistNonZero.ToString("F2");
                                resultado.label_percent_nonzero_crop = stats2.percNonZero.ToString("F2");
                                resultado.label_focus_crop = stats2.Param_Focus_normalizefreq.ToString("F2");
                                resultado.label_exposure_crop = stats2.ExposureTime.ToString();
                                resultado.label_nonzero_avg_crop = stats2.MediaNonZero.ToString("F2");
                                resultado.label_max_hist_crop = posmax2.ToString();
                                resultado.label_pto_sat_crop = PosPtoSaturation2.ToString();


                                //pinto el histograma de la imagen recortada
                                //pinto el histograma de de la imagen original
                                float[] HistogramaCropped = new float[Histogram2.MatND.ManagedArray.Length];
                                Histogram2.MatND.ManagedArray.CopyTo(HistogramaCropped, 0);
                                resultado.HistogramaCropped = HistogramaCropped;

                                //libero los recursos
                                Histogram2.Dispose();
                                surf_image.Clean();
                                surf_image2.Clean();
                            }


                            Histogram.Dispose();
                        }
                        catch (Exception ex)
                        {
                            throw new SpinException("Error worker offline image analysis: " + ex.Message);
                        }

                    }

                    e.Result = resultado;

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        #endregion

        private void AnalizarImagen()
        {
            if (_CurrentImagen != null)
            {
                try
                {
                    //calculo el histograma de la imagen original
                    DenseHistogram Histogram = aux.GetMaxHistogram(_CurrentImagen, false, out AnchoHist, out MaxValue, out posmax, ref bin, out PosPtoSaturation);
                    //claculo las estadisticos de la imagen original
                    imgStats stats = aux.CalculateStatsImage(_CurrentImagen, 0);
                    //calculo los ptos de interes de de la imagen original
                    CSurf surf_image = new CSurf(_CurrentImagen, (double)hessian_threshold_updown.Value);



                    //Si esta marcado calculo el autoexposure de la camara 
                    if (checkBox_autoexposure.Checked && checkBox_Camera.Checked)
                    {
                        if (picture_original.ROI == null || picture_original.ROI.Width == 0 || picture_original.ROI.Height == 0)
                            picture_original.ROI = new Rectangle(10, 10, _CurrentImagen.Width - 30, _CurrentImagen.Height - 30);

                        double media = aux.GetMeanOnWindow(_CurrentImagen, picture_original.ROI.Y, picture_original.ROI.X, picture_original.ROI.Height, picture_original.ROI.Width);
                        Camera.ExposureTime = aux.CalculateExposureTimeSimple((int)media, (int)reference_updown.Value, Camera.ExposureTime);
                        exposure_updown.Value = Camera.ExposureTime;
                    }

                    //si esta marcado el checkbox pinto los ptos de interes
                    if (checkBox_interestpoint_ori.Checked)
                    {
                        Image<Rgb, byte> image_to_process = _CurrentImagen.Convert<Rgb, byte>();
                        PointF p;
                        Point init, fin;
                        for (int i1 = 0; i1 < surf_image.m_pKeyPoints.Size; i1++)
                        {

                            MKeyPoint k = surf_image.m_pKeyPoints[i1];
                            p = k.Point;
                            init = new Point((int)p.X - 4, (int)p.Y - 4);
                            fin = new Point((int)p.X + 4, (int)p.Y + 4);
                            image_to_process.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);
                            init = new Point((int)p.X - 4, (int)p.Y + 4);
                            fin = new Point((int)p.X + 4, (int)p.Y - 4);
                            image_to_process.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);

                        }
                        picture_original._cPicture.Image = image_to_process.ToBitmap();
                        image_to_process.Dispose();
                    }
                    else
                    {
                        picture_original._cPicture.Image = _CurrentImagen.ToBitmap();
                    }


                    //actualizo los labels
                    label_keypoints_ori.Text = surf_image.m_pKeyPoints.Size.ToString();

                    label_width_ori.Text = stats.Width.ToString();
                    label_height_ori.Text = stats.Height.ToString();
                    label_avg_ori.Text = stats.Media.ToString("F2");
                    label_std_ori.Text = stats.Std.ToString("F2");
                    label_sum_ori.Text = stats.Suma.ToString();
                    label_percent_height_ori.Text = stats.percHeightHistNonZero.ToString("F2");
                    label_percent_nonzero_ori.Text = stats.percNonZero.ToString("F2");
                    label_focus_ori.Text = stats.Param_Focus_normalizefreq.ToString("F2");
                    label_exposure_ori.Text = stats.ExposureTime.ToString();
                    label_nonzero_avg_ori.Text = stats.MediaNonZero.ToString("F2");

                    label_max_hist_ori.Text = posmax.ToString();
                    label_pto_sat_ori.Text = PosPtoSaturation.ToString();

                    //pinto el histograma de de la imagen original
                    chart_original.Series[0].Points.Clear();
                    foreach (float val in Histogram.MatND.ManagedArray)
                    {
                        chart_original.Series[0].Points.AddY(val);
                    }

                    Rectangle rect = new Rectangle();

                    //recorto la imagen 

                    using (Image<Gray, Byte> cortada = aux.RotateAndCropImageForBilletrack(_CurrentImagen, int.Parse(textBox_angle.Text), ref rect))
                    {


                        //calculo el histograma de la imagen recortada
                        DenseHistogram Histogram2 = aux.GetMaxHistogram(cortada, false, out AnchoHist2, out MaxValue2, out posmax2, ref bin2, out PosPtoSaturation2);
                        // calculo los estadisticos de la imagen recortada
                        imgStats stats2 = aux.CalculateStatsImage(cortada, 0);
                        // calculo los ptos de interes de la imagen recortada
                        CSurf surf_image2 = new CSurf(cortada, (double)hessian_threshold_updown.Value);
                        //si esta marcado el checkbox pinto los ptos de interes
                        if (checkBox_interestpoint_crop.Checked)
                        {
                            Image<Rgb, byte> image_to_process2 = cortada.Convert<Rgb, byte>();
                            PointF p;
                            Point init, fin;
                            for (int i1 = 0; i1 < surf_image2.m_pKeyPoints.Size; i1++)
                            {

                                MKeyPoint k = surf_image2.m_pKeyPoints[i1];
                                p = k.Point;
                                init = new Point((int)p.X - 2, (int)p.Y - 2);
                                fin = new Point((int)p.X + 2, (int)p.Y + 2);
                                image_to_process2.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);
                                init = new Point((int)p.X - 2, (int)p.Y + 2);
                                fin = new Point((int)p.X + 2, (int)p.Y - 2);
                                image_to_process2.Draw(new LineSegment2D(init, fin), new Rgb(Color.Yellow), 2);

                            }
                            picture_cropped._cPicture.Image = image_to_process2.ToBitmap();
                            image_to_process2.Dispose();
                        }
                        else
                        {
                            picture_cropped._cPicture.Image = cortada.ToBitmap();
                        }
                        //actualizo los labels
                        label_keypoints_crop.Text = surf_image2.m_pKeyPoints.Size.ToString();

                        label_width_crop.Text = stats2.Width.ToString();
                        label_height_crop.Text = stats2.Height.ToString();
                        label_avg_crop.Text = stats2.Media.ToString("F2");
                        label_std_crop.Text = stats2.Std.ToString("F2");
                        label_sum_crop.Text = stats2.Suma.ToString();
                        label_percent_height_crop.Text = stats2.percHeightHistNonZero.ToString("F2");
                        label_percent_nonzero_crop.Text = stats2.percNonZero.ToString("F2");
                        label_focus_crop.Text = stats2.Param_Focus_normalizefreq.ToString("F2");
                        label_exposure_crop.Text = stats2.ExposureTime.ToString();
                        label_nonzero_avg_crop.Text = stats2.MediaNonZero.ToString("F2");
                        label_max_hist_crop.Text = posmax2.ToString();
                        label_pto_sat_crop.Text = PosPtoSaturation2.ToString();


                        //pinto el histograma de la imagen recortada
                        chart_cropped.Series[0].Points.Clear();
                        foreach (float val in Histogram2.MatND.ManagedArray)
                        {
                            chart_cropped.Series[0].Points.AddY(val);
                        }
                        //libero los recursos
                        Histogram2.Dispose();
                        surf_image.Clean();
                        surf_image2.Clean();

                    }


                    Histogram.Dispose();
                }
                catch (Exception e)
                {

                    label_path_image.Text = "Error : " + e.Message;
                }

            }


        }

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


        private void textBox_angle_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_File.Checked && _CurrentImagen != null)
            {
                AnalizarImagen();
            }
        }

        private void checkBox_interestpoint_crop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_File.Checked && _CurrentImagen != null)
            {
                AnalizarImagen();
            }
        }

        private void checkBox_interestpoint_ori_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_File.Checked && _CurrentImagen != null)
            {
                AnalizarImagen();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Camera.Checked)
            {
                checkBox_File.Checked = false;

                SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(100, groupBox3, "", global::Billetrack.Properties.Resources.fondoplayer);
                b.Show();
                this.groupBox3.Controls.Remove(this._panelParamFile);
                this.groupBox3.Controls.Add(this._panelParamCamera);
                this.groupBox3.Refresh();
                b.CloseLoading();

                try
                {
                    Camera = new CGenieCamera(1, 0, true);
                    Camera.FrameRate = 15000;
                    Camera.GrabAsync();
                }
                catch (Exception)
                {

                    checkBox_File.Checked = true;
                    checkBox_Camera.Checked = false;
                    _running = false;
                    if (Camera != null) Camera.Close();
                    return;
                }
                label_framerate.Text = Camera.FrameRate.ToString();
                exposure_updown.Value = Camera.ExposureTime;
                _running = true;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                checkBox_File.Checked = true;
                SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(100, groupBox3, "", global::Billetrack.Properties.Resources.fondoplayer);
                b.Show();
                this.groupBox3.Controls.Remove(this._panelParamCamera);
                this.groupBox3.Controls.Add(this._panelParamFile);
                label_path_image.Text = ".";
                this.groupBox3.Refresh();
                b.CloseLoading();
                _running = false;
                if (Camera != null) Camera.Close();
                if (_CurrentImagen == null)
                {
                    Bitmap _cambioImagen = new Bitmap(panel3.Width, panel3.Height);
                    panel3.DrawToBitmap(_cambioImagen, panel3.ClientRectangle);
                    SpinPlatform.Controls.spinLoadingScreenForm bc = new SpinPlatform.Controls.spinLoadingScreenForm(100, panel3, "", _cambioImagen, ImageLayout.Stretch);
                    bc.Show();
                    chart_original.Series[0].Points.Clear();
                    chart_cropped.Series[0].Points.Clear();
                    picture_cropped._cPicture.Image = null;
                    picture_original._cPicture.Image = null;
                    picture_original._cPicture.Refresh();
                    picture_cropped._cPicture.Refresh();
                    bc.CloseLoading();
                }
                else
                {
                    Bitmap _cambioImagen = new Bitmap(panel3.Width, panel3.Height);
                    panel3.DrawToBitmap(_cambioImagen, panel3.ClientRectangle);
                    SpinPlatform.Controls.spinLoadingScreenForm bc = new SpinPlatform.Controls.spinLoadingScreenForm(100, panel3, "", _cambioImagen, ImageLayout.Stretch);
                    bc.Show();
                    AnalizarImagen();
                    bc.CloseLoading();
                }
            }
        }

        private void exposure_updown_ValueChanged(object sender, EventArgs e)
        {
            Camera.ExposureTime = (int)exposure_updown.Value;
            label_framerate.Text = Camera.FrameRate.ToString();

        }

        private void checkBox_autoexposure_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autoexposure.Checked)
            {
                exposure_updown.Enabled = false;
            }
            else
            {
                exposure_updown.Enabled = true;
            }
        }

        private void checkBox_ROI_CheckedChanged(object sender, EventArgs e)
        {
            picture_original.DrawROI = checkBox_ROI.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(200, _panelLeft, "", global::Billetrack.Properties.Resources.bg22);
            b.Show();

            if (_panelLeft.Controls.Contains(_menuLeft))
            {
                pictureBox1.Image = global::Billetrack.Properties.Resources.splitterhide;
                _panelLeft.Controls.RemoveByKey("_menuLeft");
            }
            else
            {
                pictureBox1.Image = global::Billetrack.Properties.Resources.splittershow;
                _panelLeft.Controls.Add(_menuLeft);
            }
            this.Refresh();
            b.CloseLoading();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(200, _panelRight, "", global::Billetrack.Properties.Resources.bg22);
            b.Show();

            if (_panelRight.Controls.Contains(_menuRight))
            {
                pictureBox2.Image = global::Billetrack.Properties.Resources.splitterhide1;
                _panelRight.Controls.RemoveByKey("_menuRight");
            }
            else
            {
                pictureBox2.Image = global::Billetrack.Properties.Resources.splittershow1;
                _panelRight.Controls.Add(_menuRight);
            }
            this.Refresh();
            b.CloseLoading();
        }

        private void checkBox_File_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_File.Checked)
            {
                checkBox_Camera.Checked = false;
            }
            else
            {
                checkBox_Camera.Checked = true;
            }
        }

        private void button_image_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                label_path_image.Text = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1);
                if (label_path_image.Text.Substring(label_path_image.Text.LastIndexOf('.')).Contains("jp") ||
                    label_path_image.Text.Substring(label_path_image.Text.LastIndexOf('.')).Contains("bmp") ||
                    label_path_image.Text.Substring(label_path_image.Text.LastIndexOf('.')).Contains("png"))
                {
                    using (Image<Gray, byte> imagen = new Image<Gray, byte>(openFileDialog1.FileName))
                    {
                        Bitmap _cambioImagen = new Bitmap(panel3.Width, panel3.Height);
                        panel3.DrawToBitmap(_cambioImagen, panel3.ClientRectangle);
                        SpinPlatform.Controls.spinLoadingScreenForm bc = new SpinPlatform.Controls.spinLoadingScreenForm(100, panel3, "", _cambioImagen, ImageLayout.Stretch);
                        bc.Show();

                        Image = imagen;

                        bc.CloseLoading();

                    }
                }
                else
                {
                    MessageBox.Show("No se ha identificado el formato de imagen seleccionado. Solo se aceptan .jpeg, .jpg, .bmp o .png");
                }

            }
        }

        private void textBox_angle_ValueChanged_1(object sender, EventArgs e)
        {
            if (checkBox_File.Checked && _CurrentImagen != null)
            {
                AnalizarImagen();
            }
        }

        private void hessian_threshold_updown_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_File.Checked && _CurrentImagen != null)
            {
                AnalizarImagen();
            }
        }
    }
}
