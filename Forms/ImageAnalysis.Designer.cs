namespace Billetrack.Forms
{
    partial class ImageAnalysis
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageAnalysis));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_path_image = new System.Windows.Forms.Label();
            this.button_image = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chart_original = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_cropped = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_max_hist_ori = new System.Windows.Forms.Label();
            this.label_pto_sat_ori = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label_max_hist_crop = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_pto_sat_crop = new System.Windows.Forms.Label();
            this.label_avg_ori = new System.Windows.Forms.Label();
            this.label_std_ori = new System.Windows.Forms.Label();
            this.label_sum_ori = new System.Windows.Forms.Label();
            this.label_nonzero_avg_ori = new System.Windows.Forms.Label();
            this.label_percent_nonzero_ori = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label_percent_height_ori = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label_focus_ori = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label_exposure_ori = new System.Windows.Forms.Label();
            this.label_height_ori = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_width_ori = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_avg_crop = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label_std_crop = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label_nonzero_avg_crop = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label_sum_crop = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label_percent_nonzero_crop = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label_percent_height_crop = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label_focus_crop = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label_exposure_crop = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label_width_crop = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label_height_crop = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.hessian_threshold_updown = new System.Windows.Forms.NumericUpDown();
            this.textBox_angle = new System.Windows.Forms.NumericUpDown();
            this.checkBox_interestpoint_ori = new System.Windows.Forms.CheckBox();
            this.checkBox_interestpoint_crop = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label_keypoints_crop = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_keypoints_ori = new System.Windows.Forms.Label();
            this.picture_cropped = new Billetrack.Forms.Picture();
            this.picture_original = new Billetrack.Forms.Picture();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cropped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hessian_threshold_updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox_angle)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(0, 0);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(0, 0);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(0, 0);
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(0, 0);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(0, 0);
            this.imageBox3.TabIndex = 2;
            this.imageBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(94, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Image Path :";
            // 
            // label_path_image
            // 
            this.label_path_image.AutoSize = true;
            this.label_path_image.ForeColor = System.Drawing.Color.White;
            this.label_path_image.Location = new System.Drawing.Point(183, 39);
            this.label_path_image.Name = "label_path_image";
            this.label_path_image.Size = new System.Drawing.Size(10, 13);
            this.label_path_image.TabIndex = 12;
            this.label_path_image.Text = ".";
            // 
            // button_image
            // 
            this.button_image.Image = ((System.Drawing.Image)(resources.GetObject("button_image.Image")));
            this.button_image.Location = new System.Drawing.Point(54, 30);
            this.button_image.Name = "button_image";
            this.button_image.Size = new System.Drawing.Size(30, 30);
            this.button_image.TabIndex = 11;
            this.button_image.UseVisualStyleBackColor = true;
            this.button_image.Click += new System.EventHandler(this.button_image_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chart_original
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.Name = "ChartArea1";
            this.chart_original.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_original.Legends.Add(legend1);
            this.chart_original.Location = new System.Drawing.Point(192, 484);
            this.chart_original.Name = "chart_original";
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            this.chart_original.Series.Add(series1);
            this.chart_original.Size = new System.Drawing.Size(391, 107);
            this.chart_original.TabIndex = 14;
            this.chart_original.Text = "chart_histogram";
            // 
            // chart_cropped
            // 
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.Name = "ChartArea1";
            this.chart_cropped.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chart_cropped.Legends.Add(legend2);
            this.chart_cropped.Location = new System.Drawing.Point(602, 484);
            this.chart_cropped.Name = "chart_cropped";
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.Points.Add(dataPoint2);
            this.chart_cropped.Series.Add(series2);
            this.chart_cropped.Size = new System.Drawing.Size(391, 107);
            this.chart_cropped.TabIndex = 14;
            this.chart_cropped.Text = "chart_histogram";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(460, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Rotation Angle :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Image Avg :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Image Std :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(17, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Image Sum :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(17, 321);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Image NonZero Avg :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(17, 289);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = " % NonZero :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(17, 504);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Max Histogram :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(17, 532);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Saturation Point :";
            // 
            // label_max_hist_ori
            // 
            this.label_max_hist_ori.AutoSize = true;
            this.label_max_hist_ori.ForeColor = System.Drawing.Color.Red;
            this.label_max_hist_ori.Location = new System.Drawing.Point(131, 504);
            this.label_max_hist_ori.Name = "label_max_hist_ori";
            this.label_max_hist_ori.Size = new System.Drawing.Size(13, 13);
            this.label_max_hist_ori.TabIndex = 13;
            this.label_max_hist_ori.Text = "0";
            this.label_max_hist_ori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_pto_sat_ori
            // 
            this.label_pto_sat_ori.AutoSize = true;
            this.label_pto_sat_ori.ForeColor = System.Drawing.Color.Red;
            this.label_pto_sat_ori.Location = new System.Drawing.Point(131, 532);
            this.label_pto_sat_ori.Name = "label_pto_sat_ori";
            this.label_pto_sat_ori.Size = new System.Drawing.Size(13, 13);
            this.label_pto_sat_ori.TabIndex = 13;
            this.label_pto_sat_ori.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(1025, 506);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Max Histogram :";
            // 
            // label_max_hist_crop
            // 
            this.label_max_hist_crop.AutoSize = true;
            this.label_max_hist_crop.ForeColor = System.Drawing.Color.Red;
            this.label_max_hist_crop.Location = new System.Drawing.Point(1139, 506);
            this.label_max_hist_crop.Name = "label_max_hist_crop";
            this.label_max_hist_crop.Size = new System.Drawing.Size(13, 13);
            this.label_max_hist_crop.TabIndex = 13;
            this.label_max_hist_crop.Text = "0";
            this.label_max_hist_crop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(1025, 532);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Saturation Point :";
            // 
            // label_pto_sat_crop
            // 
            this.label_pto_sat_crop.AutoSize = true;
            this.label_pto_sat_crop.ForeColor = System.Drawing.Color.Red;
            this.label_pto_sat_crop.Location = new System.Drawing.Point(1139, 530);
            this.label_pto_sat_crop.Name = "label_pto_sat_crop";
            this.label_pto_sat_crop.Size = new System.Drawing.Size(13, 13);
            this.label_pto_sat_crop.TabIndex = 13;
            this.label_pto_sat_crop.Text = "0";
            // 
            // label_avg_ori
            // 
            this.label_avg_ori.AutoSize = true;
            this.label_avg_ori.ForeColor = System.Drawing.Color.White;
            this.label_avg_ori.Location = new System.Drawing.Point(131, 200);
            this.label_avg_ori.Name = "label_avg_ori";
            this.label_avg_ori.Size = new System.Drawing.Size(13, 13);
            this.label_avg_ori.TabIndex = 13;
            this.label_avg_ori.Text = "0";
            this.label_avg_ori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_std_ori
            // 
            this.label_std_ori.AutoSize = true;
            this.label_std_ori.ForeColor = System.Drawing.Color.White;
            this.label_std_ori.Location = new System.Drawing.Point(131, 230);
            this.label_std_ori.Name = "label_std_ori";
            this.label_std_ori.Size = new System.Drawing.Size(13, 13);
            this.label_std_ori.TabIndex = 13;
            this.label_std_ori.Text = "0";
            // 
            // label_sum_ori
            // 
            this.label_sum_ori.AutoSize = true;
            this.label_sum_ori.ForeColor = System.Drawing.Color.White;
            this.label_sum_ori.Location = new System.Drawing.Point(131, 263);
            this.label_sum_ori.Name = "label_sum_ori";
            this.label_sum_ori.Size = new System.Drawing.Size(13, 13);
            this.label_sum_ori.TabIndex = 13;
            this.label_sum_ori.Text = "0";
            // 
            // label_nonzero_avg_ori
            // 
            this.label_nonzero_avg_ori.AutoSize = true;
            this.label_nonzero_avg_ori.ForeColor = System.Drawing.Color.White;
            this.label_nonzero_avg_ori.Location = new System.Drawing.Point(131, 321);
            this.label_nonzero_avg_ori.Name = "label_nonzero_avg_ori";
            this.label_nonzero_avg_ori.Size = new System.Drawing.Size(13, 13);
            this.label_nonzero_avg_ori.TabIndex = 13;
            this.label_nonzero_avg_ori.Text = "0";
            // 
            // label_percent_nonzero_ori
            // 
            this.label_percent_nonzero_ori.AutoSize = true;
            this.label_percent_nonzero_ori.ForeColor = System.Drawing.Color.White;
            this.label_percent_nonzero_ori.Location = new System.Drawing.Point(131, 289);
            this.label_percent_nonzero_ori.Name = "label_percent_nonzero_ori";
            this.label_percent_nonzero_ori.Size = new System.Drawing.Size(13, 13);
            this.label_percent_nonzero_ori.TabIndex = 13;
            this.label_percent_nonzero_ori.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(17, 557);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = " %HeightHist :";
            // 
            // label_percent_height_ori
            // 
            this.label_percent_height_ori.AutoSize = true;
            this.label_percent_height_ori.ForeColor = System.Drawing.Color.Red;
            this.label_percent_height_ori.Location = new System.Drawing.Point(131, 557);
            this.label_percent_height_ori.Name = "label_percent_height_ori";
            this.label_percent_height_ori.Size = new System.Drawing.Size(13, 13);
            this.label_percent_height_ori.TabIndex = 13;
            this.label_percent_height_ori.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(17, 349);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 13;
            this.label19.Text = "Focus Indicator :";
            // 
            // label_focus_ori
            // 
            this.label_focus_ori.AutoSize = true;
            this.label_focus_ori.ForeColor = System.Drawing.Color.White;
            this.label_focus_ori.Location = new System.Drawing.Point(131, 349);
            this.label_focus_ori.Name = "label_focus_ori";
            this.label_focus_ori.Size = new System.Drawing.Size(13, 13);
            this.label_focus_ori.TabIndex = 13;
            this.label_focus_ori.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(17, 377);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 13);
            this.label21.TabIndex = 13;
            this.label21.Text = "Exposure Time :";
            // 
            // label_exposure_ori
            // 
            this.label_exposure_ori.AutoSize = true;
            this.label_exposure_ori.ForeColor = System.Drawing.Color.White;
            this.label_exposure_ori.Location = new System.Drawing.Point(131, 377);
            this.label_exposure_ori.Name = "label_exposure_ori";
            this.label_exposure_ori.Size = new System.Drawing.Size(13, 13);
            this.label_exposure_ori.TabIndex = 13;
            this.label_exposure_ori.Text = "0";
            // 
            // label_height_ori
            // 
            this.label_height_ori.AutoSize = true;
            this.label_height_ori.ForeColor = System.Drawing.Color.White;
            this.label_height_ori.Location = new System.Drawing.Point(131, 169);
            this.label_height_ori.Name = "label_height_ori";
            this.label_height_ori.Size = new System.Drawing.Size(13, 13);
            this.label_height_ori.TabIndex = 18;
            this.label_height_ori.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(17, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Image Height :";
            // 
            // label_width_ori
            // 
            this.label_width_ori.AutoSize = true;
            this.label_width_ori.ForeColor = System.Drawing.Color.White;
            this.label_width_ori.Location = new System.Drawing.Point(131, 136);
            this.label_width_ori.Name = "label_width_ori";
            this.label_width_ori.Size = new System.Drawing.Size(13, 13);
            this.label_width_ori.TabIndex = 16;
            this.label_width_ori.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(17, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Image Width :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1025, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Image Avg :";
            // 
            // label_avg_crop
            // 
            this.label_avg_crop.AutoSize = true;
            this.label_avg_crop.ForeColor = System.Drawing.Color.White;
            this.label_avg_crop.Location = new System.Drawing.Point(1139, 200);
            this.label_avg_crop.Name = "label_avg_crop";
            this.label_avg_crop.Size = new System.Drawing.Size(13, 13);
            this.label_avg_crop.TabIndex = 13;
            this.label_avg_crop.Text = "0";
            this.label_avg_crop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1025, 230);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Image Std :";
            // 
            // label_std_crop
            // 
            this.label_std_crop.AutoSize = true;
            this.label_std_crop.ForeColor = System.Drawing.Color.White;
            this.label_std_crop.Location = new System.Drawing.Point(1139, 230);
            this.label_std_crop.Name = "label_std_crop";
            this.label_std_crop.Size = new System.Drawing.Size(13, 13);
            this.label_std_crop.TabIndex = 13;
            this.label_std_crop.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(1025, 321);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(109, 13);
            this.label20.TabIndex = 13;
            this.label20.Text = "Image NonZero Avg :";
            // 
            // label_nonzero_avg_crop
            // 
            this.label_nonzero_avg_crop.AutoSize = true;
            this.label_nonzero_avg_crop.ForeColor = System.Drawing.Color.White;
            this.label_nonzero_avg_crop.Location = new System.Drawing.Point(1139, 321);
            this.label_nonzero_avg_crop.Name = "label_nonzero_avg_crop";
            this.label_nonzero_avg_crop.Size = new System.Drawing.Size(13, 13);
            this.label_nonzero_avg_crop.TabIndex = 13;
            this.label_nonzero_avg_crop.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(1025, 263);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 13);
            this.label23.TabIndex = 13;
            this.label23.Text = "Image Sum :";
            // 
            // label_sum_crop
            // 
            this.label_sum_crop.AutoSize = true;
            this.label_sum_crop.ForeColor = System.Drawing.Color.White;
            this.label_sum_crop.Location = new System.Drawing.Point(1139, 263);
            this.label_sum_crop.Name = "label_sum_crop";
            this.label_sum_crop.Size = new System.Drawing.Size(13, 13);
            this.label_sum_crop.TabIndex = 13;
            this.label_sum_crop.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(1025, 289);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 13);
            this.label25.TabIndex = 13;
            this.label25.Text = " % NonZero :";
            // 
            // label_percent_nonzero_crop
            // 
            this.label_percent_nonzero_crop.AutoSize = true;
            this.label_percent_nonzero_crop.ForeColor = System.Drawing.Color.White;
            this.label_percent_nonzero_crop.Location = new System.Drawing.Point(1139, 289);
            this.label_percent_nonzero_crop.Name = "label_percent_nonzero_crop";
            this.label_percent_nonzero_crop.Size = new System.Drawing.Size(13, 13);
            this.label_percent_nonzero_crop.TabIndex = 13;
            this.label_percent_nonzero_crop.Text = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(1028, 557);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 13);
            this.label27.TabIndex = 13;
            this.label27.Text = " %HeightHist :";
            // 
            // label_percent_height_crop
            // 
            this.label_percent_height_crop.AutoSize = true;
            this.label_percent_height_crop.ForeColor = System.Drawing.Color.Red;
            this.label_percent_height_crop.Location = new System.Drawing.Point(1139, 557);
            this.label_percent_height_crop.Name = "label_percent_height_crop";
            this.label_percent_height_crop.Size = new System.Drawing.Size(13, 13);
            this.label_percent_height_crop.TabIndex = 13;
            this.label_percent_height_crop.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(1025, 374);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(86, 13);
            this.label29.TabIndex = 13;
            this.label29.Text = "Focus Indicator :";
            // 
            // label_focus_crop
            // 
            this.label_focus_crop.AutoSize = true;
            this.label_focus_crop.ForeColor = System.Drawing.Color.White;
            this.label_focus_crop.Location = new System.Drawing.Point(1139, 374);
            this.label_focus_crop.Name = "label_focus_crop";
            this.label_focus_crop.Size = new System.Drawing.Size(13, 13);
            this.label_focus_crop.TabIndex = 13;
            this.label_focus_crop.Text = "0";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(1025, 402);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 13);
            this.label31.TabIndex = 13;
            this.label31.Text = "Exposure Time :";
            // 
            // label_exposure_crop
            // 
            this.label_exposure_crop.AutoSize = true;
            this.label_exposure_crop.ForeColor = System.Drawing.Color.White;
            this.label_exposure_crop.Location = new System.Drawing.Point(1139, 402);
            this.label_exposure_crop.Name = "label_exposure_crop";
            this.label_exposure_crop.Size = new System.Drawing.Size(13, 13);
            this.label_exposure_crop.TabIndex = 13;
            this.label_exposure_crop.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(1025, 136);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(73, 13);
            this.label33.TabIndex = 17;
            this.label33.Text = "Image Width :";
            // 
            // label_width_crop
            // 
            this.label_width_crop.AutoSize = true;
            this.label_width_crop.ForeColor = System.Drawing.Color.White;
            this.label_width_crop.Location = new System.Drawing.Point(1139, 136);
            this.label_width_crop.Name = "label_width_crop";
            this.label_width_crop.Size = new System.Drawing.Size(13, 13);
            this.label_width_crop.TabIndex = 16;
            this.label_width_crop.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(1025, 169);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(76, 13);
            this.label35.TabIndex = 19;
            this.label35.Text = "Image Height :";
            // 
            // label_height_crop
            // 
            this.label_height_crop.AutoSize = true;
            this.label_height_crop.ForeColor = System.Drawing.Color.White;
            this.label_height_crop.Location = new System.Drawing.Point(1139, 169);
            this.label_height_crop.Name = "label_height_crop";
            this.label_height_crop.Size = new System.Drawing.Size(13, 13);
            this.label_height_crop.TabIndex = 18;
            this.label_height_crop.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(460, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Hessian Threshold :";
            // 
            // hessian_threshold_updown
            // 
            this.hessian_threshold_updown.Location = new System.Drawing.Point(567, 56);
            this.hessian_threshold_updown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.hessian_threshold_updown.Name = "hessian_threshold_updown";
            this.hessian_threshold_updown.Size = new System.Drawing.Size(92, 20);
            this.hessian_threshold_updown.TabIndex = 20;
            this.hessian_threshold_updown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.hessian_threshold_updown.ValueChanged += new System.EventHandler(this.textBox_angle_ValueChanged);
            // 
            // textBox_angle
            // 
            this.textBox_angle.Location = new System.Drawing.Point(567, 15);
            this.textBox_angle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.textBox_angle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.textBox_angle.Name = "textBox_angle";
            this.textBox_angle.Size = new System.Drawing.Size(92, 20);
            this.textBox_angle.TabIndex = 21;
            this.textBox_angle.Value = new decimal(new int[] {
            53,
            0,
            0,
            -2147483648});
            this.textBox_angle.ValueChanged += new System.EventHandler(this.textBox_angle_ValueChanged);
            // 
            // checkBox_interestpoint_ori
            // 
            this.checkBox_interestpoint_ori.AutoSize = true;
            this.checkBox_interestpoint_ori.ForeColor = System.Drawing.Color.Yellow;
            this.checkBox_interestpoint_ori.Location = new System.Drawing.Point(192, 88);
            this.checkBox_interestpoint_ori.Name = "checkBox_interestpoint_ori";
            this.checkBox_interestpoint_ori.Size = new System.Drawing.Size(116, 17);
            this.checkBox_interestpoint_ori.TabIndex = 22;
            this.checkBox_interestpoint_ori.Text = "Draw Interest Point";
            this.checkBox_interestpoint_ori.UseVisualStyleBackColor = true;
            this.checkBox_interestpoint_ori.CheckedChanged += new System.EventHandler(this.checkBox_interestpoint_ori_CheckedChanged);
            // 
            // checkBox_interestpoint_crop
            // 
            this.checkBox_interestpoint_crop.AutoSize = true;
            this.checkBox_interestpoint_crop.ForeColor = System.Drawing.Color.Yellow;
            this.checkBox_interestpoint_crop.Location = new System.Drawing.Point(602, 88);
            this.checkBox_interestpoint_crop.Name = "checkBox_interestpoint_crop";
            this.checkBox_interestpoint_crop.Size = new System.Drawing.Size(116, 17);
            this.checkBox_interestpoint_crop.TabIndex = 22;
            this.checkBox_interestpoint_crop.Text = "Draw Interest Point";
            this.checkBox_interestpoint_crop.UseVisualStyleBackColor = true;
            this.checkBox_interestpoint_crop.CheckedChanged += new System.EventHandler(this.checkBox_interestpoint_crop_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Yellow;
            this.label18.Location = new System.Drawing.Point(1025, 443);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "Interest Points :";
            // 
            // label_keypoints_crop
            // 
            this.label_keypoints_crop.AutoSize = true;
            this.label_keypoints_crop.ForeColor = System.Drawing.Color.Yellow;
            this.label_keypoints_crop.Location = new System.Drawing.Point(1139, 443);
            this.label_keypoints_crop.Name = "label_keypoints_crop";
            this.label_keypoints_crop.Size = new System.Drawing.Size(13, 13);
            this.label_keypoints_crop.TabIndex = 13;
            this.label_keypoints_crop.Text = "0";
            this.label_keypoints_crop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Yellow;
            this.label22.Location = new System.Drawing.Point(17, 443);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 13);
            this.label22.TabIndex = 13;
            this.label22.Text = "Interest Points :";
            // 
            // label_keypoints_ori
            // 
            this.label_keypoints_ori.AutoSize = true;
            this.label_keypoints_ori.ForeColor = System.Drawing.Color.Yellow;
            this.label_keypoints_ori.Location = new System.Drawing.Point(131, 443);
            this.label_keypoints_ori.Name = "label_keypoints_ori";
            this.label_keypoints_ori.Size = new System.Drawing.Size(13, 13);
            this.label_keypoints_ori.TabIndex = 13;
            this.label_keypoints_ori.Text = "0";
            this.label_keypoints_ori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picture_cropped
            // 
            this.picture_cropped._TitleText = "Image Cropped";
            this.picture_cropped.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.picture_cropped.Location = new System.Drawing.Point(602, 111);
            this.picture_cropped.Name = "picture_cropped";
            this.picture_cropped.Padding = new System.Windows.Forms.Padding(20);
            this.picture_cropped.Size = new System.Drawing.Size(391, 345);
            this.picture_cropped.TabIndex = 0;
            // 
            // picture_original
            // 
            this.picture_original._TitleText = "Image Original";
            this.picture_original.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.picture_original.Location = new System.Drawing.Point(192, 111);
            this.picture_original.Name = "picture_original";
            this.picture_original.Padding = new System.Windows.Forms.Padding(20);
            this.picture_original.Size = new System.Drawing.Size(391, 345);
            this.picture_original.TabIndex = 0;
            // 
            // ImageAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_interestpoint_crop);
            this.Controls.Add(this.checkBox_interestpoint_ori);
            this.Controls.Add(this.textBox_angle);
            this.Controls.Add(this.hessian_threshold_updown);
            this.Controls.Add(this.label_height_crop);
            this.Controls.Add(this.label_height_ori);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_width_crop);
            this.Controls.Add(this.label_width_ori);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chart_cropped);
            this.Controls.Add(this.chart_original);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_exposure_crop);
            this.Controls.Add(this.label_exposure_ori);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label_focus_crop);
            this.Controls.Add(this.label_focus_ori);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label_percent_height_crop);
            this.Controls.Add(this.label_percent_height_ori);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label_percent_nonzero_crop);
            this.Controls.Add(this.label_percent_nonzero_ori);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label_pto_sat_crop);
            this.Controls.Add(this.label_pto_sat_ori);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label_keypoints_ori);
            this.Controls.Add(this.label_keypoints_crop);
            this.Controls.Add(this.label_max_hist_crop);
            this.Controls.Add(this.label_max_hist_ori);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label_sum_crop);
            this.Controls.Add(this.label_sum_ori);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label_nonzero_avg_crop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label_nonzero_avg_ori);
            this.Controls.Add(this.label_std_crop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label_std_ori);
            this.Controls.Add(this.label_avg_crop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_avg_ori);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_path_image);
            this.Controls.Add(this.button_image);
            this.Controls.Add(this.picture_cropped);
            this.Controls.Add(this.picture_original);
            this.ForeColor = System.Drawing.Color.Red;
            this.Name = "ImageAnalysis";
            this.Size = new System.Drawing.Size(1242, 649);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cropped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hessian_threshold_updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox_angle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox imageBox3;
        private Picture picture_original;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_path_image;
        private System.Windows.Forms.Button button_image;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_original;
        private Picture picture_cropped;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cropped;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_max_hist_ori;
        private System.Windows.Forms.Label label_pto_sat_ori;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_max_hist_crop;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_pto_sat_crop;
        private System.Windows.Forms.Label label_avg_ori;
        private System.Windows.Forms.Label label_std_ori;
        private System.Windows.Forms.Label label_sum_ori;
        private System.Windows.Forms.Label label_nonzero_avg_ori;
        private System.Windows.Forms.Label label_percent_nonzero_ori;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_percent_height_ori;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label_focus_ori;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label_exposure_ori;
        private System.Windows.Forms.Label label_height_ori;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_width_ori;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_avg_crop;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label_std_crop;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label_nonzero_avg_crop;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label_sum_crop;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label_percent_nonzero_crop;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label_percent_height_crop;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label_focus_crop;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label_exposure_crop;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label_width_crop;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label_height_crop;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown hessian_threshold_updown;
        private System.Windows.Forms.NumericUpDown textBox_angle;
        private System.Windows.Forms.CheckBox checkBox_interestpoint_ori;
        private System.Windows.Forms.CheckBox checkBox_interestpoint_crop;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label_keypoints_crop;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label_keypoints_ori;
    }
}
