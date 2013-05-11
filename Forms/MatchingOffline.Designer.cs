namespace Billetrack.Forms
{
    partial class MatchingOffline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchingOffline));
            this.label_path_aceria = new System.Windows.Forms.Label();
            this.label_path_alambron = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView_resultados = new System.Windows.Forms.DataGridView();
            this.button_process = new System.Windows.Forms.Button();
            this.label_estado = new System.Windows.Forms.Label();
            this.label_resultados = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox_Prueba = new Billetrack.Forms.Picture();
            this.pictureBox_Img_Original = new Billetrack.Forms.Picture();
            this.pictureBox_sincortar = new Billetrack.Forms.Picture();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).BeginInit();
            this.SuspendLayout();
            // 
            // label_path_aceria
            // 
            this.label_path_aceria.AutoSize = true;
            this.label_path_aceria.ForeColor = System.Drawing.Color.White;
            this.label_path_aceria.Location = new System.Drawing.Point(157, 32);
            this.label_path_aceria.Name = "label_path_aceria";
            this.label_path_aceria.Size = new System.Drawing.Size(10, 13);
            this.label_path_aceria.TabIndex = 8;
            this.label_path_aceria.Text = ".";
            // 
            // label_path_alambron
            // 
            this.label_path_alambron.AutoSize = true;
            this.label_path_alambron.ForeColor = System.Drawing.Color.White;
            this.label_path_alambron.Location = new System.Drawing.Point(157, 70);
            this.label_path_alambron.Name = "label_path_alambron";
            this.label_path_alambron.Size = new System.Drawing.Size(10, 13);
            this.label_path_alambron.TabIndex = 9;
            this.label_path_alambron.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(68, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Origin Path :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(68, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Destiny Path :";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(28, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 30);
            this.button3.TabIndex = 6;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(28, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView_resultados
            // 
            this.dataGridView_resultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_resultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_resultados.Location = new System.Drawing.Point(40, 414);
            this.dataGridView_resultados.Name = "dataGridView_resultados";
            this.dataGridView_resultados.Size = new System.Drawing.Size(809, 356);
            this.dataGridView_resultados.TabIndex = 12;
            this.dataGridView_resultados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_resultados_CellContentClick);
            this.dataGridView_resultados.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_resultados_RowsAdded);
            // 
            // button_process
            // 
            this.button_process.ForeColor = System.Drawing.Color.White;
            this.button_process.Location = new System.Drawing.Point(496, 27);
            this.button_process.Name = "button_process";
            this.button_process.Size = new System.Drawing.Size(90, 76);
            this.button_process.TabIndex = 13;
            this.button_process.Text = "PROCESS";
            this.button_process.UseVisualStyleBackColor = true;
            this.button_process.Click += new System.EventHandler(this.button_process_Click);
            // 
            // label_estado
            // 
            this.label_estado.AutoSize = true;
            this.label_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_estado.ForeColor = System.Drawing.Color.White;
            this.label_estado.Location = new System.Drawing.Point(610, 44);
            this.label_estado.Name = "label_estado";
            this.label_estado.Size = new System.Drawing.Size(16, 24);
            this.label_estado.TabIndex = 15;
            this.label_estado.Text = ".";
            // 
            // label_resultados
            // 
            this.label_resultados.AutoSize = true;
            this.label_resultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_resultados.ForeColor = System.Drawing.Color.White;
            this.label_resultados.Location = new System.Drawing.Point(610, 12);
            this.label_resultados.Name = "label_resultados";
            this.label_resultados.Size = new System.Drawing.Size(16, 24);
            this.label_resultados.TabIndex = 16;
            this.label_resultados.Text = ".";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(601, 80);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(304, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // pictureBox_Prueba
            // 
            this.pictureBox_Prueba._TitleText = "Imagen Matching";
            this.pictureBox_Prueba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_Prueba.Location = new System.Drawing.Point(644, 123);
            this.pictureBox_Prueba.Name = "pictureBox_Prueba";
            this.pictureBox_Prueba.Padding = new System.Windows.Forms.Padding(20);
            this.pictureBox_Prueba.Size = new System.Drawing.Size(261, 266);
            this.pictureBox_Prueba.TabIndex = 0;
            // 
            // pictureBox_Img_Original
            // 
            this.pictureBox_Img_Original._TitleText = "Imagen Recortada ";
            this.pictureBox_Img_Original.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_Img_Original.Location = new System.Drawing.Point(336, 123);
            this.pictureBox_Img_Original.Name = "pictureBox_Img_Original";
            this.pictureBox_Img_Original.Padding = new System.Windows.Forms.Padding(20);
            this.pictureBox_Img_Original.Size = new System.Drawing.Size(250, 266);
            this.pictureBox_Img_Original.TabIndex = 0;
            // 
            // pictureBox_sincortar
            // 
            this.pictureBox_sincortar._TitleText = "Imagen Original ";
            this.pictureBox_sincortar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_sincortar.Location = new System.Drawing.Point(28, 123);
            this.pictureBox_sincortar.Name = "pictureBox_sincortar";
            this.pictureBox_sincortar.Padding = new System.Windows.Forms.Padding(20);
            this.pictureBox_sincortar.Size = new System.Drawing.Size(250, 266);
            this.pictureBox_sincortar.TabIndex = 0;
            // 
            // MatchingOffline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_estado);
            this.Controls.Add(this.label_resultados);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_process);
            this.Controls.Add(this.dataGridView_resultados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_path_aceria);
            this.Controls.Add(this.label_path_alambron);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox_Prueba);
            this.Controls.Add(this.pictureBox_Img_Original);
            this.Controls.Add(this.pictureBox_sincortar);
            this.Name = "MatchingOffline";
            this.Size = new System.Drawing.Size(948, 789);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Picture pictureBox_sincortar;
        private Picture pictureBox_Img_Original;
        private Picture pictureBox_Prueba;
        private System.Windows.Forms.Label label_path_aceria;
        private System.Windows.Forms.Label label_path_alambron;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_resultados;
        private System.Windows.Forms.Button button_process;
        private System.Windows.Forms.Label label_estado;
        private System.Windows.Forms.Label label_resultados;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
