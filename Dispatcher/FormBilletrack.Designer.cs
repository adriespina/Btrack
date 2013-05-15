namespace Billetrack
{
    partial class FormBilletrack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBilletrack));
            this.tableContenedor = new System.Windows.Forms.TableLayoutPanel();
            this._ctopMenu = new SpinPlatform.Controls.SpinTopMenuControl();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchingFromFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cstatusMenu = new SpinPlatform.Controls.SpinStatusStripControl();
            this._cConProcessComputer = new SpinPlatform.Controls.SpinToolStripConnectionControl();
            this._cConFTP = new SpinPlatform.Controls.SpinToolStripConnectionControl();
            this._cConDBRemote = new SpinPlatform.Controls.SpinToolStripConnectionControl();
            this._cConLight = new SpinPlatform.Controls.SpinToolStripConnectionControl();
            this._cConCamera = new SpinPlatform.Controls.SpinToolStripConnectionControl();
            this._cConHardDrive = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._cConfiguration = new SpinPlatform.Controls.SpinConfigurationPanel();
            this.timer_State = new System.Windows.Forms.Timer(this.components);
            this._cMain = new Billetrack.Forms.Main();
            this._cMatchingOffline = new Billetrack.Forms.OfflineMatching();
            this._cImageAnalysis = new Billetrack.Forms.OfflineImageAnalysis();
            this._cClassificationTrainning = new Billetrack.Forms.ClassificationTrainning();
            this.classificationTrainningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableContenedor.SuspendLayout();
            this._ctopMenu.SuspendLayout();
            this._cstatusMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableContenedor
            // 
            this.tableContenedor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableContenedor.BackgroundImage")));
            this.tableContenedor.ColumnCount = 1;
            this.tableContenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableContenedor.Controls.Add(this._ctopMenu, 0, 0);
            this.tableContenedor.Controls.Add(this._cstatusMenu, 0, 2);
            this.tableContenedor.Controls.Add(this.panel1, 0, 1);
            this.tableContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableContenedor.Location = new System.Drawing.Point(0, 0);
            this.tableContenedor.Margin = new System.Windows.Forms.Padding(0);
            this.tableContenedor.Name = "tableContenedor";
            this.tableContenedor.RowCount = 3;
            this.tableContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableContenedor.Size = new System.Drawing.Size(1219, 744);
            this.tableContenedor.TabIndex = 0;
            // 
            // _ctopMenu
            // 
            this._ctopMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_ctopMenu.BackgroundImage")));
            this._ctopMenu.ColorBar = SpinPlatform.Controls.spinColor.orange;
            this._ctopMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ctopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this._ctopMenu.Location = new System.Drawing.Point(0, 0);
            this._ctopMenu.Name = "_ctopMenu";
            this._ctopMenu.Size = new System.Drawing.Size(1219, 40);
            this._ctopMenu.TabIndex = 0;
            this._ctopMenu.Text = "spinTopMenuControl1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(41, 36);
            this.mainToolStripMenuItem.Text = "Main";
            this.mainToolStripMenuItem.Click += new System.EventHandler(this.mainToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(84, 36);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matchingFromFolderToolStripMenuItem,
            this.imageAnalysisToolStripMenuItem,
            this.classificationTrainningToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 36);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // matchingFromFolderToolStripMenuItem
            // 
            this.matchingFromFolderToolStripMenuItem.Name = "matchingFromFolderToolStripMenuItem";
            this.matchingFromFolderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.matchingFromFolderToolStripMenuItem.Text = "Matching from Folder";
            this.matchingFromFolderToolStripMenuItem.Click += new System.EventHandler(this.matchingFromFolderToolStripMenuItem_Click);
            // 
            // imageAnalysisToolStripMenuItem
            // 
            this.imageAnalysisToolStripMenuItem.Name = "imageAnalysisToolStripMenuItem";
            this.imageAnalysisToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.imageAnalysisToolStripMenuItem.Text = "Image Analysis";
            this.imageAnalysisToolStripMenuItem.Click += new System.EventHandler(this.imageAnalysisToolStripMenuItem_Click);
            // 
            // _cstatusMenu
            // 
            this._cstatusMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cstatusMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cConProcessComputer,
            this._cConFTP,
            this._cConDBRemote,
            this._cConLight,
            this._cConCamera,
            this._cConHardDrive});
            this._cstatusMenu.Location = new System.Drawing.Point(0, 716);
            this._cstatusMenu.Name = "_cstatusMenu";
            this._cstatusMenu.Size = new System.Drawing.Size(1219, 28);
            this._cstatusMenu.TabIndex = 1;
            this._cstatusMenu.Text = "spinStatusStripControl1";
            // 
            // _cConProcessComputer
            // 
            this._cConProcessComputer.AutoSize = false;
            this._cConProcessComputer.BackColor = System.Drawing.Color.Transparent;
            this._cConProcessComputer.FontColor = System.Drawing.Color.White;
            this._cConProcessComputer.ForeColor = System.Drawing.Color.White;
            this._cConProcessComputer.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConProcessComputer.Name = "_cConProcessComputer";
            this._cConProcessComputer.Size = new System.Drawing.Size(190, 23);
            this._cConProcessComputer.Spring = true;
            this._cConProcessComputer.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
            this._cConProcessComputer.Text = "Process Computer";
            // 
            // _cConFTP
            // 
            this._cConFTP.AutoSize = false;
            this._cConFTP.BackColor = System.Drawing.Color.Transparent;
            this._cConFTP.FontColor = System.Drawing.Color.White;
            this._cConFTP.ForeColor = System.Drawing.Color.White;
            this._cConFTP.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConFTP.Name = "_cConFTP";
            this._cConFTP.Size = new System.Drawing.Size(190, 23);
            this._cConFTP.Spring = true;
            this._cConFTP.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
            this._cConFTP.Text = "FTP";
            // 
            // _cConDBRemote
            // 
            this._cConDBRemote.AutoSize = false;
            this._cConDBRemote.BackColor = System.Drawing.Color.Transparent;
            this._cConDBRemote.FontColor = System.Drawing.Color.White;
            this._cConDBRemote.ForeColor = System.Drawing.Color.White;
            this._cConDBRemote.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConDBRemote.Name = "_cConDBRemote";
            this._cConDBRemote.Size = new System.Drawing.Size(190, 23);
            this._cConDBRemote.Spring = true;
            this._cConDBRemote.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
            this._cConDBRemote.Text = "DB Remote";
            // 
            // _cConLight
            // 
            this._cConLight.AutoSize = false;
            this._cConLight.BackColor = System.Drawing.Color.Transparent;
            this._cConLight.FontColor = System.Drawing.Color.White;
            this._cConLight.ForeColor = System.Drawing.Color.White;
            this._cConLight.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConLight.Name = "_cConLight";
            this._cConLight.Size = new System.Drawing.Size(190, 23);
            this._cConLight.Spring = true;
            this._cConLight.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
            this._cConLight.Text = "Light";
            // 
            // _cConCamera
            // 
            this._cConCamera.AutoSize = false;
            this._cConCamera.BackColor = System.Drawing.Color.Transparent;
            this._cConCamera.FontColor = System.Drawing.Color.White;
            this._cConCamera.ForeColor = System.Drawing.Color.White;
            this._cConCamera.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConCamera.Name = "_cConCamera";
            this._cConCamera.Size = new System.Drawing.Size(190, 23);
            this._cConCamera.Spring = true;
            this._cConCamera.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
            this._cConCamera.Text = "Camera";
            // 
            // _cConHardDrive
            // 
            this._cConHardDrive.BackColor = System.Drawing.Color.Transparent;
            this._cConHardDrive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._cConHardDrive.ForeColor = System.Drawing.Color.DimGray;
            this._cConHardDrive.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this._cConHardDrive.Name = "_cConHardDrive";
            this._cConHardDrive.Size = new System.Drawing.Size(190, 23);
            this._cConHardDrive.Spring = true;
            this._cConHardDrive.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1219, 676);
            this.panel1.TabIndex = 2;
            // 
            // _cConfiguration
            // 
            this._cConfiguration.BackColor = System.Drawing.Color.WhiteSmoke;
            this._cConfiguration.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this._cConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cConfiguration.ErrorText = "Couldn´t load configuration file";
            this._cConfiguration.FilePath = "BilletrackConfig.xml";
            this._cConfiguration.Location = new System.Drawing.Point(0, 0);
            this._cConfiguration.Name = "_cConfiguration";
            this._cConfiguration.ResetButtonVisible = true;
            this._cConfiguration.ResetText = "Reset";
            this._cConfiguration.SelectionColor = System.Drawing.Color.DarkOrange;
            this._cConfiguration.Size = new System.Drawing.Size(1219, 676);
            this._cConfiguration.TabIndex = 4;
            // 
            // timer_State
            // 
            this.timer_State.Enabled = true;
            this.timer_State.Interval = 500;
            this.timer_State.Tick += new System.EventHandler(this.timer_State_Tick);
            // 
            // _cMain
            // 
            this._cMain.BackColor = System.Drawing.Color.Transparent;
            this._cMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cMain.Location = new System.Drawing.Point(0, 0);
            this._cMain.Margin = new System.Windows.Forms.Padding(0);
            this._cMain.Name = "_cMain";
            this._cMain.Size = new System.Drawing.Size(1219, 676);
            this._cMain.TabIndex = 3;
            // 
            // _cMatchingOffline
            // 
            this._cMatchingOffline.BackColor = System.Drawing.Color.Transparent;
            this._cMatchingOffline.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cMatchingOffline.Location = new System.Drawing.Point(0, 0);
            this._cMatchingOffline.Name = "_cMatchingOffline";
            this._cMatchingOffline.Size = new System.Drawing.Size(1259, 676);
            this._cMatchingOffline.TabIndex = 4;
            // 
            // _cImageAnalysis
            // 
            this._cImageAnalysis.BackColor = System.Drawing.Color.Transparent;
            this._cImageAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cImageAnalysis.ForeColor = System.Drawing.Color.Red;
            this._cImageAnalysis.Image = null;
            this._cImageAnalysis.Location = new System.Drawing.Point(0, 0);
            this._cImageAnalysis.Name = "_cImageAnalysis";
            this._cImageAnalysis.Size = new System.Drawing.Size(1108, 676);
            this._cImageAnalysis.TabIndex = 4;
            // 
            // _cClassificationTrainning
            // 
            this._cClassificationTrainning.BackColor = System.Drawing.Color.Transparent;
            this._cClassificationTrainning.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cClassificationTrainning.Location = new System.Drawing.Point(0, 0);
            this._cClassificationTrainning.Name = "_cClassificationTrainning";
            this._cClassificationTrainning.Size = new System.Drawing.Size(1219, 676);
            this._cClassificationTrainning.TabIndex = 0;
            // 
            // classificationTrainningToolStripMenuItem
            // 
            this.classificationTrainningToolStripMenuItem.Name = "classificationTrainningToolStripMenuItem";
            this.classificationTrainningToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.classificationTrainningToolStripMenuItem.Text = "Classification Trainning";
            this.classificationTrainningToolStripMenuItem.Click += new System.EventHandler(this.classificationTrainningToolStripMenuItem_Click);
            // 
            // FormBilletrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1219, 744);
            this.Controls.Add(this.tableContenedor);
            this.MainMenuStrip = this._ctopMenu;
            this.Name = "FormBilletrack";
            this.Text = "Billetrack";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBilletrack_FormClosing);
            this.Load += new System.EventHandler(this.FormBilletrack_Load);
            this.tableContenedor.ResumeLayout(false);
            this.tableContenedor.PerformLayout();
            this._ctopMenu.ResumeLayout(false);
            this._ctopMenu.PerformLayout();
            this._cstatusMenu.ResumeLayout(false);
            this._cstatusMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableContenedor;
        private SpinPlatform.Controls.SpinTopMenuControl _ctopMenu;
        private SpinPlatform.Controls.SpinStatusStripControl _cstatusMenu;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        public System.Windows.Forms.Panel panel1;
        private Forms.Main _cMain;
        private SpinPlatform.Controls.SpinConfigurationPanel _cConfiguration;
        private System.Windows.Forms.Timer timer_State;
        public SpinPlatform.Controls.SpinToolStripConnectionControl _cConFTP;
        public SpinPlatform.Controls.SpinToolStripConnectionControl _cConDBRemote;
        public SpinPlatform.Controls.SpinToolStripConnectionControl _cConLight;
        public SpinPlatform.Controls.SpinToolStripConnectionControl _cConCamera;
        public System.Windows.Forms.ToolStripStatusLabel _cConHardDrive;
        public SpinPlatform.Controls.SpinToolStripConnectionControl _cConProcessComputer;
        private Forms.OfflineMatching _cMatchingOffline;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchingFromFolderToolStripMenuItem;
        private Forms.OfflineImageAnalysis _cImageAnalysis;
        private System.Windows.Forms.ToolStripMenuItem imageAnalysisToolStripMenuItem;
        private Forms.ClassificationTrainning _cClassificationTrainning;
        private System.Windows.Forms.ToolStripMenuItem classificationTrainningToolStripMenuItem;
    }
}

