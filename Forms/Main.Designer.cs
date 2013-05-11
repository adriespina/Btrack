namespace Billetrack.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this._cPanelDatos = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this._labelError = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelNumberCalculated = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelLineCalculated = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelCastCalculated = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.spinLabel1 = new SpinPlatform.Controls.SpinLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._labelDate = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelNumber = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelLine = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this._labelCast = new SpinPlatform.Controls.SpinBlackDataLabelControl();
            this.panel6 = new System.Windows.Forms.Panel();
            this.spinLabel2 = new SpinPlatform.Controls.SpinLabel();
            this._cOriginalPanel = new System.Windows.Forms.Panel();
            this._cResultsPanel = new System.Windows.Forms.Panel();
            this._cTableResults = new System.Windows.Forms.TableLayoutPanel();
            this._cSliderResultsPanel = new System.Windows.Forms.Panel();
            this._cSliderResultsPic = new System.Windows.Forms.PictureBox();
            this._cSliderDatosPanel = new System.Windows.Forms.Panel();
            this._cSliderDatosPic = new System.Windows.Forms.PictureBox();
            this._cOriginalObj = new Billetrack.Forms.Picture();
            this._cResultsCroppedObj = new Billetrack.Forms.Picture();
            this._cResultsMatchedObj = new Billetrack.Forms.Picture();
            this._cPanelDatos.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this._cOriginalPanel.SuspendLayout();
            this._cResultsPanel.SuspendLayout();
            this._cTableResults.SuspendLayout();
            this._cSliderResultsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cSliderResultsPic)).BeginInit();
            this._cSliderDatosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cSliderDatosPic)).BeginInit();
            this.SuspendLayout();
            // 
            // _cPanelDatos
            // 
            this._cPanelDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._cPanelDatos.Controls.Add(this.panel3);
            this._cPanelDatos.Controls.Add(this.panel2);
            this._cPanelDatos.Controls.Add(this.panel1);
            this._cPanelDatos.Controls.Add(this.panel6);
            this._cPanelDatos.Dock = System.Windows.Forms.DockStyle.Left;
            this._cPanelDatos.Location = new System.Drawing.Point(0, 0);
            this._cPanelDatos.Name = "_cPanelDatos";
            this._cPanelDatos.Size = new System.Drawing.Size(205, 691);
            this._cPanelDatos.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._labelError);
            this.panel3.Controls.Add(this._labelNumberCalculated);
            this.panel3.Controls.Add(this._labelLineCalculated);
            this.panel3.Controls.Add(this._labelCastCalculated);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 369);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 322);
            this.panel3.TabIndex = 6;
            // 
            // _labelError
            // 
            this._labelError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelError.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelError.BackgroundImage")));
            this._labelError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelError.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this._labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelError.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelError.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelError.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelError.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelError.Location = new System.Drawing.Point(0, 222);
            this._labelError.MainColor = System.Drawing.Color.White;
            this._labelError.MainText = "_";
            this._labelError.Margin = new System.Windows.Forms.Padding(0);
            this._labelError.Name = "_labelError";
            this._labelError.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelError.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelError.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelError.ShowToolTips = true;
            this._labelError.Size = new System.Drawing.Size(205, 100);
            this._labelError.SubtitleText = "";
            this._labelError.TabIndex = 7;
            this._labelError.TitleColor = System.Drawing.Color.Silver;
            this._labelError.TitleText = "Error";
            // 
            // _labelNumberCalculated
            // 
            this._labelNumberCalculated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelNumberCalculated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelNumberCalculated.BackgroundImage")));
            this._labelNumberCalculated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelNumberCalculated.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelNumberCalculated.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelNumberCalculated.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumberCalculated.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumberCalculated.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumberCalculated.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumberCalculated.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelNumberCalculated.Location = new System.Drawing.Point(0, 148);
            this._labelNumberCalculated.MainColor = System.Drawing.Color.White;
            this._labelNumberCalculated.MainText = "_";
            this._labelNumberCalculated.Margin = new System.Windows.Forms.Padding(0);
            this._labelNumberCalculated.Name = "_labelNumberCalculated";
            this._labelNumberCalculated.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelNumberCalculated.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelNumberCalculated.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelNumberCalculated.ShowToolTips = true;
            this._labelNumberCalculated.Size = new System.Drawing.Size(205, 74);
            this._labelNumberCalculated.SubtitleText = "";
            this._labelNumberCalculated.TabIndex = 6;
            this._labelNumberCalculated.TitleColor = System.Drawing.Color.Silver;
            this._labelNumberCalculated.TitleText = "Number";
            // 
            // _labelLineCalculated
            // 
            this._labelLineCalculated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelLineCalculated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelLineCalculated.BackgroundImage")));
            this._labelLineCalculated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelLineCalculated.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelLineCalculated.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelLineCalculated.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLineCalculated.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLineCalculated.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLineCalculated.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLineCalculated.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelLineCalculated.Location = new System.Drawing.Point(0, 74);
            this._labelLineCalculated.MainColor = System.Drawing.Color.White;
            this._labelLineCalculated.MainText = "_";
            this._labelLineCalculated.Margin = new System.Windows.Forms.Padding(0);
            this._labelLineCalculated.Name = "_labelLineCalculated";
            this._labelLineCalculated.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelLineCalculated.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelLineCalculated.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelLineCalculated.ShowToolTips = true;
            this._labelLineCalculated.Size = new System.Drawing.Size(205, 74);
            this._labelLineCalculated.SubtitleText = "";
            this._labelLineCalculated.TabIndex = 5;
            this._labelLineCalculated.TitleColor = System.Drawing.Color.Silver;
            this._labelLineCalculated.TitleText = "Line";
            // 
            // _labelCastCalculated
            // 
            this._labelCastCalculated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelCastCalculated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelCastCalculated.BackgroundImage")));
            this._labelCastCalculated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelCastCalculated.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelCastCalculated.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelCastCalculated.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCastCalculated.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCastCalculated.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCastCalculated.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCastCalculated.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelCastCalculated.Location = new System.Drawing.Point(0, 0);
            this._labelCastCalculated.MainColor = System.Drawing.Color.White;
            this._labelCastCalculated.MainText = "_";
            this._labelCastCalculated.Margin = new System.Windows.Forms.Padding(0);
            this._labelCastCalculated.Name = "_labelCastCalculated";
            this._labelCastCalculated.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelCastCalculated.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelCastCalculated.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelCastCalculated.ShowToolTips = true;
            this._labelCastCalculated.Size = new System.Drawing.Size(205, 74);
            this._labelCastCalculated.SubtitleText = "";
            this._labelCastCalculated.TabIndex = 4;
            this._labelCastCalculated.TitleColor = System.Drawing.Color.Silver;
            this._labelCastCalculated.TitleText = "Cast";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Billetrack.Properties.Resources.menu;
            this.panel2.Controls.Add(this.spinLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 335);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panel2.Size = new System.Drawing.Size(205, 34);
            this.panel2.TabIndex = 5;
            // 
            // spinLabel1
            // 
            this.spinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.spinLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinLabel1.FontSpin = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.spinLabel1.FontSpinSize = 12F;
            this.spinLabel1.Location = new System.Drawing.Point(0, 6);
            this.spinLabel1.MainBackColor = System.Drawing.Color.Transparent;
            this.spinLabel1.MainFontColor = System.Drawing.Color.White;
            this.spinLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.spinLabel1.MessageText = " Calculated Information";
            this.spinLabel1.Name = "spinLabel1";
            this.spinLabel1.ShadowColor = System.Drawing.Color.Black;
            this.spinLabel1.Size = new System.Drawing.Size(205, 28);
            this.spinLabel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._labelDate);
            this.panel1.Controls.Add(this._labelNumber);
            this.panel1.Controls.Add(this._labelLine);
            this.panel1.Controls.Add(this._labelCast);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 301);
            this.panel1.TabIndex = 4;
            // 
            // _labelDate
            // 
            this._labelDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelDate.BackgroundImage")));
            this._labelDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelDate.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this._labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelDate.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelDate.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelDate.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelDate.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelDate.Location = new System.Drawing.Point(0, 222);
            this._labelDate.MainColor = System.Drawing.Color.White;
            this._labelDate.MainText = "_";
            this._labelDate.Margin = new System.Windows.Forms.Padding(0);
            this._labelDate.Name = "_labelDate";
            this._labelDate.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelDate.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelDate.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelDate.ShowToolTips = true;
            this._labelDate.Size = new System.Drawing.Size(205, 79);
            this._labelDate.SubtitleText = "";
            this._labelDate.TabIndex = 3;
            this._labelDate.TitleColor = System.Drawing.Color.Silver;
            this._labelDate.TitleText = "Date";
            // 
            // _labelNumber
            // 
            this._labelNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelNumber.BackgroundImage")));
            this._labelNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelNumber.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumber.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumber.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumber.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelNumber.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelNumber.Location = new System.Drawing.Point(0, 148);
            this._labelNumber.MainColor = System.Drawing.Color.White;
            this._labelNumber.MainText = "_";
            this._labelNumber.Margin = new System.Windows.Forms.Padding(0);
            this._labelNumber.Name = "_labelNumber";
            this._labelNumber.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelNumber.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelNumber.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelNumber.ShowToolTips = true;
            this._labelNumber.Size = new System.Drawing.Size(205, 74);
            this._labelNumber.SubtitleText = "";
            this._labelNumber.TabIndex = 2;
            this._labelNumber.TitleColor = System.Drawing.Color.Silver;
            this._labelNumber.TitleText = "Number";
            // 
            // _labelLine
            // 
            this._labelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelLine.BackgroundImage")));
            this._labelLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelLine.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelLine.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLine.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLine.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLine.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelLine.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelLine.Location = new System.Drawing.Point(0, 74);
            this._labelLine.MainColor = System.Drawing.Color.White;
            this._labelLine.MainText = "_";
            this._labelLine.Margin = new System.Windows.Forms.Padding(0);
            this._labelLine.Name = "_labelLine";
            this._labelLine.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelLine.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelLine.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelLine.ShowToolTips = true;
            this._labelLine.Size = new System.Drawing.Size(205, 74);
            this._labelLine.SubtitleText = "";
            this._labelLine.TabIndex = 1;
            this._labelLine.TitleColor = System.Drawing.Color.Silver;
            this._labelLine.TitleText = "Line";
            // 
            // _labelCast
            // 
            this._labelCast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._labelCast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_labelCast.BackgroundImage")));
            this._labelCast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._labelCast.BlackLabelAlign = SpinPlatform.Controls.spinBlackLabelAlign.vertical;
            this._labelCast.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCast.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCast.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCast.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelCast.ForeColor = System.Drawing.Color.OrangeRed;
            this._labelCast.Location = new System.Drawing.Point(0, 0);
            this._labelCast.MainColor = System.Drawing.Color.White;
            this._labelCast.MainText = "_";
            this._labelCast.Margin = new System.Windows.Forms.Padding(0);
            this._labelCast.Name = "_labelCast";
            this._labelCast.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this._labelCast.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelCast.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._labelCast.ShowToolTips = true;
            this._labelCast.Size = new System.Drawing.Size(205, 74);
            this._labelCast.SubtitleText = "";
            this._labelCast.TabIndex = 0;
            this._labelCast.TitleColor = System.Drawing.Color.Silver;
            this._labelCast.TitleText = "Cast";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::Billetrack.Properties.Resources.menu;
            this.panel6.Controls.Add(this.spinLabel2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panel6.Size = new System.Drawing.Size(205, 34);
            this.panel6.TabIndex = 3;
            // 
            // spinLabel2
            // 
            this.spinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.spinLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinLabel2.FontSpin = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.spinLabel2.FontSpinSize = 12F;
            this.spinLabel2.Location = new System.Drawing.Point(0, 6);
            this.spinLabel2.MainBackColor = System.Drawing.Color.Transparent;
            this.spinLabel2.MainFontColor = System.Drawing.Color.White;
            this.spinLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.spinLabel2.MessageText = " Received Information";
            this.spinLabel2.Name = "spinLabel2";
            this.spinLabel2.ShadowColor = System.Drawing.Color.Black;
            this.spinLabel2.Size = new System.Drawing.Size(205, 28);
            this.spinLabel2.TabIndex = 1;
            // 
            // _cOriginalPanel
            // 
            this._cOriginalPanel.Controls.Add(this._cOriginalObj);
            this._cOriginalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cOriginalPanel.Location = new System.Drawing.Point(215, 0);
            this._cOriginalPanel.Name = "_cOriginalPanel";
            this._cOriginalPanel.Padding = new System.Windows.Forms.Padding(10);
            this._cOriginalPanel.Size = new System.Drawing.Size(406, 691);
            this._cOriginalPanel.TabIndex = 3;
            // 
            // _cResultsPanel
            // 
            this._cResultsPanel.Controls.Add(this._cTableResults);
            this._cResultsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._cResultsPanel.Location = new System.Drawing.Point(631, 0);
            this._cResultsPanel.Name = "_cResultsPanel";
            this._cResultsPanel.Size = new System.Drawing.Size(489, 691);
            this._cResultsPanel.TabIndex = 5;
            // 
            // _cTableResults
            // 
            this._cTableResults.ColumnCount = 1;
            this._cTableResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._cTableResults.Controls.Add(this._cResultsCroppedObj, 0, 0);
            this._cTableResults.Controls.Add(this._cResultsMatchedObj, 0, 1);
            this._cTableResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cTableResults.Location = new System.Drawing.Point(0, 0);
            this._cTableResults.Name = "_cTableResults";
            this._cTableResults.RowCount = 2;
            this._cTableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._cTableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._cTableResults.Size = new System.Drawing.Size(489, 691);
            this._cTableResults.TabIndex = 0;
            // 
            // _cSliderResultsPanel
            // 
            this._cSliderResultsPanel.BackColor = System.Drawing.Color.Transparent;
            this._cSliderResultsPanel.BackgroundImage = global::Billetrack.Properties.Resources.splitterback;
            this._cSliderResultsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._cSliderResultsPanel.Controls.Add(this._cSliderResultsPic);
            this._cSliderResultsPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._cSliderResultsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._cSliderResultsPanel.Location = new System.Drawing.Point(621, 0);
            this._cSliderResultsPanel.Margin = new System.Windows.Forms.Padding(0);
            this._cSliderResultsPanel.Name = "_cSliderResultsPanel";
            this._cSliderResultsPanel.Size = new System.Drawing.Size(10, 691);
            this._cSliderResultsPanel.TabIndex = 4;
            // 
            // _cSliderResultsPic
            // 
            this._cSliderResultsPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cSliderResultsPic.Image = global::Billetrack.Properties.Resources.splittershow;
            this._cSliderResultsPic.Location = new System.Drawing.Point(0, 0);
            this._cSliderResultsPic.Margin = new System.Windows.Forms.Padding(0);
            this._cSliderResultsPic.Name = "_cSliderResultsPic";
            this._cSliderResultsPic.Size = new System.Drawing.Size(10, 691);
            this._cSliderResultsPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._cSliderResultsPic.TabIndex = 0;
            this._cSliderResultsPic.TabStop = false;
            this._cSliderResultsPic.Click += new System.EventHandler(this._cSliderResultsPic_Click);
            // 
            // _cSliderDatosPanel
            // 
            this._cSliderDatosPanel.BackColor = System.Drawing.Color.Transparent;
            this._cSliderDatosPanel.BackgroundImage = global::Billetrack.Properties.Resources.splitterback;
            this._cSliderDatosPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._cSliderDatosPanel.Controls.Add(this._cSliderDatosPic);
            this._cSliderDatosPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._cSliderDatosPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._cSliderDatosPanel.Location = new System.Drawing.Point(205, 0);
            this._cSliderDatosPanel.Margin = new System.Windows.Forms.Padding(0);
            this._cSliderDatosPanel.Name = "_cSliderDatosPanel";
            this._cSliderDatosPanel.Size = new System.Drawing.Size(10, 691);
            this._cSliderDatosPanel.TabIndex = 2;
            // 
            // _cSliderDatosPic
            // 
            this._cSliderDatosPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cSliderDatosPic.Image = global::Billetrack.Properties.Resources.splitterhide;
            this._cSliderDatosPic.Location = new System.Drawing.Point(0, 0);
            this._cSliderDatosPic.Margin = new System.Windows.Forms.Padding(0);
            this._cSliderDatosPic.Name = "_cSliderDatosPic";
            this._cSliderDatosPic.Size = new System.Drawing.Size(10, 691);
            this._cSliderDatosPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._cSliderDatosPic.TabIndex = 0;
            this._cSliderDatosPic.TabStop = false;
            this._cSliderDatosPic.Click += new System.EventHandler(this._cSliderDatosPic_Click);
            // 
            // _cOriginalObj
            // 
            this._cOriginalObj._ImageBackground = ((System.Drawing.Image)(resources.GetObject("_cOriginalObj._ImageBackground")));
            this._cOriginalObj._TitleText = "Original image";
            this._cOriginalObj._Zoom = false;
            this._cOriginalObj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._cOriginalObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cOriginalObj.DrawROI = false;
            this._cOriginalObj.Location = new System.Drawing.Point(10, 10);
            this._cOriginalObj.Name = "_cOriginalObj";
            this._cOriginalObj.Padding = new System.Windows.Forms.Padding(3);
            this._cOriginalObj.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._cOriginalObj.Size = new System.Drawing.Size(386, 671);
            this._cOriginalObj.TabIndex = 0;
            // 
            // _cResultsCroppedObj
            // 
            this._cResultsCroppedObj._ImageBackground = global::Billetrack.Properties.Resources.masgray;
            this._cResultsCroppedObj._TitleText = "Cropped image";
            this._cResultsCroppedObj._Zoom = false;
            this._cResultsCroppedObj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._cResultsCroppedObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cResultsCroppedObj.DrawROI = false;
            this._cResultsCroppedObj.Location = new System.Drawing.Point(10, 10);
            this._cResultsCroppedObj.Margin = new System.Windows.Forms.Padding(10);
            this._cResultsCroppedObj.Name = "_cResultsCroppedObj";
            this._cResultsCroppedObj.Padding = new System.Windows.Forms.Padding(3);
            this._cResultsCroppedObj.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._cResultsCroppedObj.Size = new System.Drawing.Size(469, 325);
            this._cResultsCroppedObj.TabIndex = 3;
            // 
            // _cResultsMatchedObj
            // 
            this._cResultsMatchedObj._ImageBackground = global::Billetrack.Properties.Resources.masgray;
            this._cResultsMatchedObj._TitleText = "Matched image";
            this._cResultsMatchedObj._Zoom = false;
            this._cResultsMatchedObj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this._cResultsMatchedObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cResultsMatchedObj.DrawROI = false;
            this._cResultsMatchedObj.Location = new System.Drawing.Point(10, 355);
            this._cResultsMatchedObj.Margin = new System.Windows.Forms.Padding(10);
            this._cResultsMatchedObj.Name = "_cResultsMatchedObj";
            this._cResultsMatchedObj.Padding = new System.Windows.Forms.Padding(3);
            this._cResultsMatchedObj.ROI = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._cResultsMatchedObj.Size = new System.Drawing.Size(469, 326);
            this._cResultsMatchedObj.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._cOriginalPanel);
            this.Controls.Add(this._cSliderResultsPanel);
            this.Controls.Add(this._cResultsPanel);
            this.Controls.Add(this._cSliderDatosPanel);
            this.Controls.Add(this._cPanelDatos);
            this.Name = "Main";
            this.Size = new System.Drawing.Size(1120, 691);
            this._cPanelDatos.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this._cOriginalPanel.ResumeLayout(false);
            this._cResultsPanel.ResumeLayout(false);
            this._cTableResults.ResumeLayout(false);
            this._cSliderResultsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cSliderResultsPic)).EndInit();
            this._cSliderDatosPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cSliderDatosPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _cPanelDatos;
        private System.Windows.Forms.Panel _cSliderDatosPanel;
        private System.Windows.Forms.PictureBox _cSliderDatosPic;
        private System.Windows.Forms.Panel _cOriginalPanel;
        private System.Windows.Forms.Panel _cSliderResultsPanel;
        private System.Windows.Forms.PictureBox _cSliderResultsPic;
        private System.Windows.Forms.Panel _cResultsPanel;
        private System.Windows.Forms.TableLayoutPanel _cTableResults;
        private System.Windows.Forms.Panel panel6;
        private SpinPlatform.Controls.SpinLabel spinLabel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private SpinPlatform.Controls.SpinLabel spinLabel1;
        private System.Windows.Forms.Panel panel1;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelCast;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelDate;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelNumber;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelLine;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelError;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelNumberCalculated;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelLineCalculated;
        public SpinPlatform.Controls.SpinBlackDataLabelControl _labelCastCalculated;
        public Picture _cResultsCroppedObj;
        public Picture _cResultsMatchedObj;
        public Picture _cOriginalObj;
    }
}
