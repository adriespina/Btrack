namespace Billetrack.Forms
{
    partial class OfflineMatching
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflineMatching));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_Prueba = new Billetrack.Forms.Picture();
            this.pictureBox_Img_Original = new Billetrack.Forms.Picture();
            this.pictureBox_sincortar = new Billetrack.Forms.Picture();
            this.dataGridView_resultados = new System.Windows.Forms.DataGridView();
            this.spinLabel1 = new SpinPlatform.Controls.SpinLabel();
            this._splitter = new System.Windows.Forms.Panel();
            this._splitterarrow = new System.Windows.Forms.PictureBox();
            this._controles = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._play = new System.Windows.Forms.Panel();
            this._controlesinfo = new System.Windows.Forms.Panel();
            this.label_estado = new System.Windows.Forms.Label();
            this.label_resultados = new System.Windows.Forms.Label();
            this._controlesOriginPath = new System.Windows.Forms.Panel();
            this.label_path_alambron = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_path_aceria = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).BeginInit();
            this._splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitterarrow)).BeginInit();
            this._controles.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this._controlesinfo.SuspendLayout();
            this._controlesOriginPath.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(851, 585);
            this.panel3.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_resultados);
            this.splitContainer1.Size = new System.Drawing.Size(851, 585);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox_Prueba, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox_Img_Original, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox_sincortar, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(851, 400);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureBox_Prueba
            // 
            this.pictureBox_Prueba._ImageBackground = ((System.Drawing.Image)(resources.GetObject("pictureBox_Prueba._ImageBackground")));
            this.pictureBox_Prueba._TitleText = "Matching image";
            this.pictureBox_Prueba._Zoom = true;
            this.pictureBox_Prueba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_Prueba.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Prueba.DrawROI = true;
            this.pictureBox_Prueba.Location = new System.Drawing.Point(575, 9);
            this.pictureBox_Prueba.Margin = new System.Windows.Forms.Padding(9);
            this.pictureBox_Prueba.Name = "pictureBox_Prueba";
            this.pictureBox_Prueba.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox_Prueba.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.pictureBox_Prueba.Size = new System.Drawing.Size(267, 382);
            this.pictureBox_Prueba.TabIndex = 2;
            // 
            // pictureBox_Img_Original
            // 
            this.pictureBox_Img_Original._ImageBackground = ((System.Drawing.Image)(resources.GetObject("pictureBox_Img_Original._ImageBackground")));
            this.pictureBox_Img_Original._TitleText = "Cropped image";
            this.pictureBox_Img_Original._Zoom = true;
            this.pictureBox_Img_Original.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_Img_Original.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Img_Original.DrawROI = true;
            this.pictureBox_Img_Original.Location = new System.Drawing.Point(292, 9);
            this.pictureBox_Img_Original.Margin = new System.Windows.Forms.Padding(9);
            this.pictureBox_Img_Original.Name = "pictureBox_Img_Original";
            this.pictureBox_Img_Original.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox_Img_Original.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.pictureBox_Img_Original.Size = new System.Drawing.Size(265, 382);
            this.pictureBox_Img_Original.TabIndex = 1;
            // 
            // pictureBox_sincortar
            // 
            this.pictureBox_sincortar._ImageBackground = ((System.Drawing.Image)(resources.GetObject("pictureBox_sincortar._ImageBackground")));
            this.pictureBox_sincortar._TitleText = "Original image";
            this.pictureBox_sincortar._Zoom = true;
            this.pictureBox_sincortar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox_sincortar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_sincortar.DrawROI = false;
            this.pictureBox_sincortar.Location = new System.Drawing.Point(9, 9);
            this.pictureBox_sincortar.Margin = new System.Windows.Forms.Padding(9);
            this.pictureBox_sincortar.Name = "pictureBox_sincortar";
            this.pictureBox_sincortar.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox_sincortar.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.pictureBox_sincortar.Size = new System.Drawing.Size(265, 382);
            this.pictureBox_sincortar.TabIndex = 0;
            // 
            // dataGridView_resultados
            // 
            this.dataGridView_resultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_resultados.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView_resultados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_resultados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_resultados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_resultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_resultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_resultados.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView_resultados.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_resultados.Name = "dataGridView_resultados";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_resultados.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_resultados.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridView_resultados.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView_resultados.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Goldenrod;
            this.dataGridView_resultados.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView_resultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_resultados.Size = new System.Drawing.Size(851, 181);
            this.dataGridView_resultados.TabIndex = 13;
            this.dataGridView_resultados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_resultados_CellContentClick);
            this.dataGridView_resultados.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_resultados_RowsAdded);
            // 
            // spinLabel1
            // 
            this.spinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.spinLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinLabel1.FontSpin = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.spinLabel1.FontSpinSize = 32F;
            this.spinLabel1.Location = new System.Drawing.Point(0, 0);
            this.spinLabel1.MainBackColor = System.Drawing.Color.Transparent;
            this.spinLabel1.MainFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.spinLabel1.MessageText = "Aún no hay datos";
            this.spinLabel1.Name = "spinLabel1";
            this.spinLabel1.ShadowColor = System.Drawing.Color.Black;
            this.spinLabel1.Size = new System.Drawing.Size(851, 585);
            this.spinLabel1.TabIndex = 0;
            // 
            // _splitter
            // 
            this._splitter.BackgroundImage = global::Billetrack.Properties.Resources.splitterback1;
            this._splitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._splitter.Controls.Add(this._splitterarrow);
            this._splitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._splitter.Location = new System.Drawing.Point(0, 585);
            this._splitter.Margin = new System.Windows.Forms.Padding(0);
            this._splitter.Name = "_splitter";
            this._splitter.Size = new System.Drawing.Size(851, 10);
            this._splitter.TabIndex = 1;
            // 
            // _splitterarrow
            // 
            this._splitterarrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this._splitterarrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitterarrow.Image = global::Billetrack.Properties.Resources.splittershow1;
            this._splitterarrow.Location = new System.Drawing.Point(0, 0);
            this._splitterarrow.Margin = new System.Windows.Forms.Padding(0);
            this._splitterarrow.Name = "_splitterarrow";
            this._splitterarrow.Size = new System.Drawing.Size(851, 10);
            this._splitterarrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._splitterarrow.TabIndex = 0;
            this._splitterarrow.TabStop = false;
            this._splitterarrow.Click += new System.EventHandler(this._splitterarrow_Click);
            // 
            // _controles
            // 
            this._controles.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this._controles.BackgroundImage = global::Billetrack.Properties.Resources.fondoplayer;
            this._controles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._controles.Controls.Add(this.tableLayoutPanel1);
            this._controles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._controles.Location = new System.Drawing.Point(0, 595);
            this._controles.Margin = new System.Windows.Forms.Padding(0);
            this._controles.Name = "_controles";
            this._controles.Size = new System.Drawing.Size(851, 100);
            this._controles.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._play, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._controlesinfo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._controlesOriginPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(851, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar1.Location = new System.Drawing.Point(488, 62);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(6, 12, 12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(351, 26);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            // 
            // _play
            // 
            this._play.BackColor = System.Drawing.Color.Transparent;
            this._play.BackgroundImage = global::Billetrack.Properties.Resources.play;
            this._play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._play.Location = new System.Drawing.Point(372, 3);
            this._play.Name = "_play";
            this.tableLayoutPanel1.SetRowSpan(this._play, 2);
            this._play.Size = new System.Drawing.Size(107, 94);
            this._play.TabIndex = 0;
            this._play.Click += new System.EventHandler(this.play_Click);
            this._play.MouseEnter += new System.EventHandler(this._play_MouseEnter);
            this._play.MouseLeave += new System.EventHandler(this._play_MouseLeave);
            // 
            // _controlesinfo
            // 
            this._controlesinfo.Controls.Add(this.label_estado);
            this._controlesinfo.Controls.Add(this.label_resultados);
            this._controlesinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._controlesinfo.Location = new System.Drawing.Point(482, 6);
            this._controlesinfo.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this._controlesinfo.Name = "_controlesinfo";
            this._controlesinfo.Size = new System.Drawing.Size(369, 44);
            this._controlesinfo.TabIndex = 16;
            // 
            // label_estado
            // 
            this.label_estado.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_estado.ForeColor = System.Drawing.Color.DimGray;
            this.label_estado.Location = new System.Drawing.Point(0, 24);
            this.label_estado.Margin = new System.Windows.Forms.Padding(0);
            this.label_estado.Name = "label_estado";
            this.label_estado.Size = new System.Drawing.Size(369, 24);
            this.label_estado.TabIndex = 17;
            // 
            // label_resultados
            // 
            this.label_resultados.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_resultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_resultados.ForeColor = System.Drawing.Color.DimGray;
            this.label_resultados.Location = new System.Drawing.Point(0, 0);
            this.label_resultados.Margin = new System.Windows.Forms.Padding(0);
            this.label_resultados.Name = "label_resultados";
            this.label_resultados.Size = new System.Drawing.Size(369, 24);
            this.label_resultados.TabIndex = 18;
            // 
            // _controlesOriginPath
            // 
            this._controlesOriginPath.Controls.Add(this.label_path_alambron);
            this._controlesOriginPath.Controls.Add(this.button3);
            this._controlesOriginPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this._controlesOriginPath.Location = new System.Drawing.Point(3, 3);
            this._controlesOriginPath.Name = "_controlesOriginPath";
            this._controlesOriginPath.Padding = new System.Windows.Forms.Padding(6);
            this._controlesOriginPath.Size = new System.Drawing.Size(363, 44);
            this._controlesOriginPath.TabIndex = 19;
            // 
            // label_path_alambron
            // 
            this.label_path_alambron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_path_alambron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_path_alambron.ForeColor = System.Drawing.Color.DimGray;
            this.label_path_alambron.Location = new System.Drawing.Point(39, 6);
            this.label_path_alambron.Name = "label_path_alambron";
            this.label_path_alambron.Size = new System.Drawing.Size(318, 32);
            this.label_path_alambron.TabIndex = 19;
            this.label_path_alambron.Text = "Origin Path :";
            this.label_path_alambron.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.label_path_aceria);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(363, 44);
            this.panel2.TabIndex = 20;
            // 
            // label_path_aceria
            // 
            this.label_path_aceria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_path_aceria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_path_aceria.ForeColor = System.Drawing.Color.DimGray;
            this.label_path_aceria.Location = new System.Drawing.Point(39, 6);
            this.label_path_aceria.Name = "label_path_aceria";
            this.label_path_aceria.Size = new System.Drawing.Size(318, 32);
            this.label_path_aceria.TabIndex = 20;
            this.label_path_aceria.Text = "Destiny Path :";
            this.label_path_aceria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 32);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OfflineMatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this._splitter);
            this.Controls.Add(this._controles);
            this.Name = "OfflineMatching";
            this.Size = new System.Drawing.Size(851, 695);
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_resultados)).EndInit();
            this._splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitterarrow)).EndInit();
            this._controles.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._controlesinfo.ResumeLayout(false);
            this._controlesOriginPath.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _controles;
        private System.Windows.Forms.Panel _splitter;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox _splitterarrow;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel _play;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_resultados;
        private System.Windows.Forms.Panel _controlesinfo;
        private System.Windows.Forms.Label label_estado;
        private System.Windows.Forms.Label label_resultados;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel _controlesOriginPath;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_path_alambron;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_path_aceria;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Picture pictureBox_sincortar;
        private Picture pictureBox_Prueba;
        private Picture pictureBox_Img_Original;
        private SpinPlatform.Controls.SpinLabel spinLabel1;

    }
}
