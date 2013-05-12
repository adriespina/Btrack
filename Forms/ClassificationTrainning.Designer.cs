namespace Billetrack.Forms
{
    partial class ClassificationTrainning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassificationTrainning));
            this.picture2 = new Billetrack.Forms.Picture();
            this.picture3 = new Billetrack.Forms.Picture();
            this.button_NEWMATCH = new System.Windows.Forms.Button();
            this.button_ADDFAILURE = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._controlesOriginPath = new System.Windows.Forms.Panel();
            this.label_path_origin = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_path_destiny = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView_ORIGIN = new System.Windows.Forms.DataGridView();
            this.dataGridView_DESTINY = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this._controlesOriginPath.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ORIGIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DESTINY)).BeginInit();
            this.SuspendLayout();
            // 
            // picture2
            // 
            this.picture2._ImageBackground = ((System.Drawing.Image)(resources.GetObject("picture2._ImageBackground")));
            this.picture2._Zoom = false;
            this.picture2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.picture2.DrawROI = false;
            this.picture2.Location = new System.Drawing.Point(619, 39);
            this.picture2.Name = "picture2";
            this.picture2.Padding = new System.Windows.Forms.Padding(3);
            this.picture2.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.picture2.Size = new System.Drawing.Size(391, 345);
            this.picture2.TabIndex = 1;
            // 
            // picture3
            // 
            this.picture3._ImageBackground = ((System.Drawing.Image)(resources.GetObject("picture3._ImageBackground")));
            this.picture3._Zoom = false;
            this.picture3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.picture3.DrawROI = false;
            this.picture3.Location = new System.Drawing.Point(202, 39);
            this.picture3.Name = "picture3";
            this.picture3.Padding = new System.Windows.Forms.Padding(3);
            this.picture3.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.picture3.Size = new System.Drawing.Size(391, 345);
            this.picture3.TabIndex = 2;
            // 
            // button_NEWMATCH
            // 
            this.button_NEWMATCH.Location = new System.Drawing.Point(1082, 52);
            this.button_NEWMATCH.Name = "button_NEWMATCH";
            this.button_NEWMATCH.Size = new System.Drawing.Size(109, 51);
            this.button_NEWMATCH.TabIndex = 3;
            this.button_NEWMATCH.Text = "ADD NEW MATCH";
            this.button_NEWMATCH.UseVisualStyleBackColor = true;
            // 
            // button_ADDFAILURE
            // 
            this.button_ADDFAILURE.Location = new System.Drawing.Point(1082, 122);
            this.button_ADDFAILURE.Name = "button_ADDFAILURE";
            this.button_ADDFAILURE.Size = new System.Drawing.Size(109, 51);
            this.button_ADDFAILURE.TabIndex = 3;
            this.button_ADDFAILURE.Text = "ADD NEW FAILURE";
            this.button_ADDFAILURE.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1082, 214);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(158, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "CORRECT TRAINING FILE";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // _controlesOriginPath
            // 
            this._controlesOriginPath.Controls.Add(this.label_path_origin);
            this._controlesOriginPath.Controls.Add(this.button3);
            this._controlesOriginPath.Location = new System.Drawing.Point(202, 405);
            this._controlesOriginPath.Name = "_controlesOriginPath";
            this._controlesOriginPath.Padding = new System.Windows.Forms.Padding(6);
            this._controlesOriginPath.Size = new System.Drawing.Size(363, 44);
            this._controlesOriginPath.TabIndex = 21;
            // 
            // label_path_origin
            // 
            this.label_path_origin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_path_origin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_path_origin.ForeColor = System.Drawing.Color.DimGray;
            this.label_path_origin.Location = new System.Drawing.Point(39, 6);
            this.label_path_origin.Name = "label_path_origin";
            this.label_path_origin.Size = new System.Drawing.Size(318, 32);
            this.label_path_origin.TabIndex = 19;
            this.label_path_origin.Text = "Origin Path :";
            this.label_path_origin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::Billetrack.Properties.Resources.botonWhite;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(6, 6);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 32);
            this.button3.TabIndex = 18;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_path_destiny);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(619, 405);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(363, 44);
            this.panel1.TabIndex = 21;
            // 
            // label_path_destiny
            // 
            this.label_path_destiny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_path_destiny.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_path_destiny.ForeColor = System.Drawing.Color.DimGray;
            this.label_path_destiny.Location = new System.Drawing.Point(39, 6);
            this.label_path_destiny.Name = "label_path_destiny";
            this.label_path_destiny.Size = new System.Drawing.Size(318, 32);
            this.label_path_destiny.TabIndex = 19;
            this.label_path_destiny.Text = "Origin Path :";
            this.label_path_destiny.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Billetrack.Properties.Resources.botonWhite;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 32);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView_ORIGIN
            // 
            this.dataGridView_ORIGIN.AllowUserToAddRows = false;
            this.dataGridView_ORIGIN.AllowUserToOrderColumns = true;
            this.dataGridView_ORIGIN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ORIGIN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView_ORIGIN.Location = new System.Drawing.Point(202, 472);
            this.dataGridView_ORIGIN.Name = "dataGridView_ORIGIN";
            this.dataGridView_ORIGIN.Size = new System.Drawing.Size(363, 150);
            this.dataGridView_ORIGIN.TabIndex = 22;
            // 
            // dataGridView_DESTINY
            // 
            this.dataGridView_DESTINY.AllowUserToAddRows = false;
            this.dataGridView_DESTINY.AllowUserToOrderColumns = true;
            this.dataGridView_DESTINY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DESTINY.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.dataGridView_DESTINY.Location = new System.Drawing.Point(619, 472);
            this.dataGridView_DESTINY.Name = "dataGridView_DESTINY";
            this.dataGridView_DESTINY.Size = new System.Drawing.Size(363, 150);
            this.dataGridView_DESTINY.TabIndex = 22;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "FILE NAME";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "FILE NAME";
            this.Column2.Name = "Column2";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "C:\\\\Users\\\\Cesar\\\\Desktop\\\\Billetrack Adrian\\\\datos";
            // 
            // ClassificationTrainning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_DESTINY);
            this.Controls.Add(this.dataGridView_ORIGIN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._controlesOriginPath);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_ADDFAILURE);
            this.Controls.Add(this.button_NEWMATCH);
            this.Controls.Add(this.picture3);
            this.Controls.Add(this.picture2);
            this.Name = "ClassificationTrainning";
            this.Size = new System.Drawing.Size(1266, 800);
            this._controlesOriginPath.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ORIGIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DESTINY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Picture picture2;
        private Picture picture3;
        private System.Windows.Forms.Button button_NEWMATCH;
        private System.Windows.Forms.Button button_ADDFAILURE;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel _controlesOriginPath;
        private System.Windows.Forms.Label label_path_origin;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_path_destiny;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView_ORIGIN;
        private System.Windows.Forms.DataGridView dataGridView_DESTINY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
