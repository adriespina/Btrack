namespace Billetrack.Forms
{
    partial class Imagen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._imagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._imagen)).BeginInit();
            this.SuspendLayout();
            // 
            // _imagen
            // 
            this._imagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imagen.Location = new System.Drawing.Point(0, 0);
            this._imagen.Margin = new System.Windows.Forms.Padding(0);
            this._imagen.Name = "_imagen";
            this._imagen.Size = new System.Drawing.Size(517, 475);
            this._imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._imagen.TabIndex = 0;
            this._imagen.TabStop = false;
            // 
            // Imagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 475);
            this.Controls.Add(this._imagen);
            this.Name = "Imagen";
            this.Text = "Imagen";
            ((System.ComponentModel.ISupportInitialize)(this._imagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox _imagen;
    }
}