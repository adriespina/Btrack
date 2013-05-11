namespace Billetrack.Forms
{
    partial class Picture
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
            this._cPicture = new System.Windows.Forms.PictureBox();
            this.contenedorImagen = new System.Windows.Forms.Panel();
            this._cTitle = new System.Windows.Forms.Label();
            this._cabecera = new System.Windows.Forms.Panel();
            this.zoom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._cPicture)).BeginInit();
            this.contenedorImagen.SuspendLayout();
            this._cabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cPicture
            // 
            this._cPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._cPicture.BackgroundImage = global::Billetrack.Properties.Resources.logo;
            this._cPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._cPicture.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._cPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cPicture.Location = new System.Drawing.Point(6, 5);
            this._cPicture.Margin = new System.Windows.Forms.Padding(0);
            this._cPicture.Name = "_cPicture";
            this._cPicture.Size = new System.Drawing.Size(373, 308);
            this._cPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._cPicture.TabIndex = 3;
            this._cPicture.TabStop = false;
            this._cPicture.Paint += new System.Windows.Forms.PaintEventHandler(this._cPicture_Paint);
            this._cPicture.DoubleClick += new System.EventHandler(this._cPicture_DoubleClick);
            this._cPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this._cPicture_MouseDown);
            this._cPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this._cPicture_MouseMove);
            this._cPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this._cPicture_MouseUp);
            // 
            // contenedorImagen
            // 
            this.contenedorImagen.Controls.Add(this._cPicture);
            this.contenedorImagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorImagen.Location = new System.Drawing.Point(3, 23);
            this.contenedorImagen.Margin = new System.Windows.Forms.Padding(0);
            this.contenedorImagen.Name = "contenedorImagen";
            this.contenedorImagen.Padding = new System.Windows.Forms.Padding(6, 5, 6, 6);
            this.contenedorImagen.Size = new System.Drawing.Size(385, 319);
            this.contenedorImagen.TabIndex = 4;
            // 
            // _cTitle
            // 
            this._cTitle.AutoSize = true;
            this._cTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this._cTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this._cTitle.Location = new System.Drawing.Point(0, 0);
            this._cTitle.Margin = new System.Windows.Forms.Padding(0);
            this._cTitle.Name = "_cTitle";
            this._cTitle.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this._cTitle.Size = new System.Drawing.Size(60, 20);
            this._cTitle.TabIndex = 4;
            this._cTitle.Text = "label1";
            // 
            // _cabecera
            // 
            this._cabecera.Controls.Add(this.zoom);
            this._cabecera.Controls.Add(this._cTitle);
            this._cabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this._cabecera.Location = new System.Drawing.Point(3, 3);
            this._cabecera.Margin = new System.Windows.Forms.Padding(0);
            this._cabecera.Name = "_cabecera";
            this._cabecera.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._cabecera.Size = new System.Drawing.Size(385, 20);
            this._cabecera.TabIndex = 5;
            // 
            // zoom
            // 
            this.zoom.BackgroundImage = global::Billetrack.Properties.Resources.mas;
            this.zoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.zoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.zoom.Location = new System.Drawing.Point(358, 0);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(25, 20);
            this.zoom.TabIndex = 5;
            this.zoom.Click += new System.EventHandler(this.zoom_Click);
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.contenedorImagen);
            this.Controls.Add(this._cabecera);
            this.DoubleBuffered = true;
            this.Name = "Picture";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(391, 345);
            ((System.ComponentModel.ISupportInitialize)(this._cPicture)).EndInit();
            this.contenedorImagen.ResumeLayout(false);
            this._cabecera.ResumeLayout(false);
            this._cabecera.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox _cPicture;
        private System.Windows.Forms.Panel contenedorImagen;
        private System.Windows.Forms.Label _cTitle;
        private System.Windows.Forms.Panel _cabecera;
        private System.Windows.Forms.Panel zoom;

    }
}
