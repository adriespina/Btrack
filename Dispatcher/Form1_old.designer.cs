namespace Billetrack
{
    partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.pictureBox_Img_Original = new System.Windows.Forms.PictureBox();
            this.pictureBox_Prueba = new System.Windows.Forms.PictureBox();
            this.dataGridView_resultados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label_path_alambron = new System.Windows.Forms.Label();
            this.label_path_aceria = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_resultados = new System.Windows.Forms.Label();
            this.label_estado = new System.Windows.Forms.Label();
            this.pictureBox_sincortar = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button_camara = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_hist = new System.Windows.Forms.Button();
            this.button_startDispathcher = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Img_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Prueba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_sincortar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Img_Original
            // 
            this.pictureBox_Img_Original.Location = new System.Drawing.Point(411, 35);
            this.pictureBox_Img_Original.Name = "pictureBox_Img_Original";
            this.pictureBox_Img_Original.Size = new System.Drawing.Size(287, 286);
            this.pictureBox_Img_Original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Img_Original.TabIndex = 0;
            this.pictureBox_Img_Original.TabStop = false;
            // 
            // pictureBox_Prueba
            // 
            this.pictureBox_Prueba.Location = new System.Drawing.Point(773, 35);
            this.pictureBox_Prueba.Name = "pictureBox_Prueba";
            this.pictureBox_Prueba.Size = new System.Drawing.Size(320, 286);
            this.pictureBox_Prueba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Prueba.TabIndex = 0;
            this.pictureBox_Prueba.TabStop = false;
            // 
            // dataGridView_resultados
            // 
            this.dataGridView_resultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_resultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_resultados.Location = new System.Drawing.Point(41, 438);
            this.dataGridView_resultados.Name = "dataGridView_resultados";
            this.dataGridView_resultados.Size = new System.Drawing.Size(1385, 356);
            this.dataGridView_resultados.TabIndex = 1;
            this.dataGridView_resultados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_resultados_CellContentClick);
            this.dataGridView_resultados.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_resultados_RowsAdded);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Imagen Original : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(770, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Imagen Matching";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1181, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "BGW";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1122, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cargar Imagenes Aceria";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1122, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Cargar Imagenes Alambron";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_path_alambron
            // 
            this.label_path_alambron.AutoSize = true;
            this.label_path_alambron.Location = new System.Drawing.Point(1122, 77);
            this.label_path_alambron.Name = "label_path_alambron";
            this.label_path_alambron.Size = new System.Drawing.Size(10, 13);
            this.label_path_alambron.TabIndex = 5;
            this.label_path_alambron.Text = ".";
            // 
            // label_path_aceria
            // 
            this.label_path_aceria.AutoSize = true;
            this.label_path_aceria.Location = new System.Drawing.Point(1122, 167);
            this.label_path_aceria.Name = "label_path_aceria";
            this.label_path_aceria.Size = new System.Drawing.Size(10, 13);
            this.label_path_aceria.TabIndex = 5;
            this.label_path_aceria.Text = ".";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1122, 358);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(304, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // label_resultados
            // 
            this.label_resultados.AutoSize = true;
            this.label_resultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_resultados.Location = new System.Drawing.Point(1133, 273);
            this.label_resultados.Name = "label_resultados";
            this.label_resultados.Size = new System.Drawing.Size(16, 24);
            this.label_resultados.TabIndex = 7;
            this.label_resultados.Text = ".";
            // 
            // label_estado
            // 
            this.label_estado.AutoSize = true;
            this.label_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_estado.Location = new System.Drawing.Point(1133, 321);
            this.label_estado.Name = "label_estado";
            this.label_estado.Size = new System.Drawing.Size(16, 24);
            this.label_estado.TabIndex = 7;
            this.label_estado.Text = ".";
            // 
            // pictureBox_sincortar
            // 
            this.pictureBox_sincortar.Location = new System.Drawing.Point(41, 35);
            this.pictureBox_sincortar.Name = "pictureBox_sincortar";
            this.pictureBox_sincortar.Size = new System.Drawing.Size(287, 286);
            this.pictureBox_sincortar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_sincortar.TabIndex = 0;
            this.pictureBox_sincortar.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Imagen Recortada : ";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1299, 209);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "recortar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button_camara
            // 
            this.button_camara.Location = new System.Drawing.Point(1399, 209);
            this.button_camara.Name = "button_camara";
            this.button_camara.Size = new System.Drawing.Size(75, 23);
            this.button_camara.TabIndex = 9;
            this.button_camara.Text = "Camara";
            this.button_camara.UseVisualStyleBackColor = true;
            this.button_camara.Click += new System.EventHandler(this.button_camara_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chart1
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(44, 340);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(284, 78);
            this.chart1.TabIndex = 10;
            this.chart1.Text = "chart_histogram";
            // 
            // button_hist
            // 
            this.button_hist.Location = new System.Drawing.Point(1399, 167);
            this.button_hist.Name = "button_hist";
            this.button_hist.Size = new System.Drawing.Size(75, 23);
            this.button_hist.TabIndex = 11;
            this.button_hist.Text = "Histograma";
            this.button_hist.UseVisualStyleBackColor = true;
            this.button_hist.Click += new System.EventHandler(this.button_hist_Click);
            // 
            // button_startDispathcher
            // 
            this.button_startDispathcher.Location = new System.Drawing.Point(1181, 167);
            this.button_startDispathcher.Name = "button_startDispathcher";
            this.button_startDispathcher.Size = new System.Drawing.Size(75, 23);
            this.button_startDispathcher.TabIndex = 12;
            this.button_startDispathcher.Text = "Start Dispatcher";
            this.button_startDispathcher.UseVisualStyleBackColor = true;
            this.button_startDispathcher.Click += new System.EventHandler(this.button_startDispathcher_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1564, 830);
            this.Controls.Add(this.button_startDispathcher);
            this.Controls.Add(this.button_hist);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button_camara);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label_estado);
            this.Controls.Add(this.label_resultados);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_path_aceria);
            this.Controls.Add(this.label_path_alambron);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_resultados);
            this.Controls.Add(this.pictureBox_Prueba);
            this.Controls.Add(this.pictureBox_sincortar);
            this.Controls.Add(this.pictureBox_Img_Original);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Img_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Prueba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_sincortar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Img_Original;
        private System.Windows.Forms.PictureBox pictureBox_Prueba;
        private System.Windows.Forms.DataGridView dataGridView_resultados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_path_alambron;
        private System.Windows.Forms.Label label_path_aceria;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_resultados;
        private System.Windows.Forms.Label label_estado;
        private System.Windows.Forms.PictureBox pictureBox_sincortar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button_camara;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button_hist;
        private System.Windows.Forms.Button button_startDispathcher;
    }
}

