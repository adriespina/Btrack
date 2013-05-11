using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace Billetrack.Forms
{
    public partial class Picture : UserControl
    {
        private string _name = "Title";
        private bool _zoom = false;
        private bool _selectingROI = false;
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), DefaultValue("Zoom")]
        [Bindable(true)]
        [Description("With zoomable button?")]
        [Category("Spin")]
        public bool _Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;

                if (_zoom)
                {
                    zoom.Visible = true;
                }
                else
                {
                    zoom.Visible = false;
                }
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), DefaultValue("null")]
        [Bindable(true)]
        [Description("Image background")]
        [Category("Spin")]
        public Image _ImageBackground
        {
            get { return _cPicture.BackgroundImage; }
            set
            {
                _cPicture.BackgroundImage = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), DefaultValue("Title")]
        [Bindable(true)]
        [Description("Title Text display")]
        [Category("Spin")]
        public string _TitleText
        {
            get { return _name; }
            set
            {
                _name = value;
                _cTitle.Text = value;

                if (value == null || value == "")
                {
                    this._cabecera.Controls.Remove(_cTitle);
                }
                else if (!this.Controls.Contains(_cTitle))
                {
                    this._cabecera.Controls.Add(_cTitle);
                }
                Invalidate();
            }
        }

        private Point _RectStartPointControl;
        private Point _RectEndPointControl;
        private Point _RectStartPointImage;
        private Point _RectEndPointImage;
        private Rectangle _ROIImage;
        private Rectangle _ROIControl;
        private Pen selectionBrush = new Pen(Color.Red,2);
        private bool _DrawROI = false;

        public bool DrawROI
        {
            get { return _DrawROI; }
            set { _DrawROI = value; _cPicture.Invalidate(); }
        }

        public Rectangle ROI
        {
            get { return _ROIImage; }
            set 
            {
                _ROIImage = value;
                if (_cPicture.Image!=null)
                    _ROIControl = new Rectangle(value.X * _cPicture.Size.Width / _cPicture.Image.Width, value.Y * _cPicture.Size.Height / _cPicture.Image.Height, value.Width * _cPicture.Size.Width / _cPicture.Image.Width, value.Height * _cPicture.Size.Height / _cPicture.Image.Height);
            }
        }

        public Picture()
        {
            InitializeComponent();
        }

        public void setImage(string path)
        {
            SpinPlatform.Controls.spinLoadingScreenForm c = new SpinPlatform.Controls.spinLoadingScreenForm(200, this, "", null);
            c.BackColor = Color.FromArgb(80, 80, 80);
            c.Show();

            _cPicture.Image = Image.FromFile(path);
            _cPicture.Refresh();

            c.CloseLoading();
        }

        public void setImage(Bitmap bitmap)
        {
            if (_cPicture.Image == null)
            {
                SpinPlatform.Controls.spinLoadingScreenForm c = new SpinPlatform.Controls.spinLoadingScreenForm(200, this._cPicture, "", null);
                c.BackColor = Color.FromArgb(80, 80, 80);
                c.Show();

                _cPicture.Image = bitmap;
                _cPicture.Refresh();

                c.CloseLoading();
            }
            else
            {
                _cPicture.Image = bitmap;
            }
        }

        public void setImageWithAnim(Bitmap bitmap)
        {
                SpinPlatform.Controls.spinLoadingScreenForm c = _cPicture.Image == null ? new SpinPlatform.Controls.spinLoadingScreenForm(200, this._cPicture, "", null) : new SpinPlatform.Controls.spinLoadingScreenForm(200, this._cPicture, "", _cPicture.Image,ImageLayout.Stretch);
                c.BackColor = Color.FromArgb(80, 80, 80);
                c.Show();

                _cPicture.Image = bitmap;
                _cPicture.Refresh();

                c.CloseLoading();
        }

        private void _cPicture_DoubleClick(object sender, EventArgs e)
        {
            if ( _cPicture.Image!=null)
            {
                _cPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                _cPicture.Refresh(); 
            }
        }

        private void _cPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectingROI = true;
                _RectStartPointImage = new Point((int)Math.Round((double)(e.Location.X*_cPicture.Image.Width/_cPicture.Size.Width)),(int)(Math.Round((double)(e.Location.Y*_cPicture.Image.Height/_cPicture.Size.Height))));
                _RectStartPointControl = e.Location;
            }

        }

        private void _cPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selectingROI = false;
                _RectEndPointImage = new Point((int)Math.Round((double)(e.Location.X * _cPicture.Image.Width/_cPicture.Size.Width )), (int)(Math.Round((double)(e.Location.Y *_cPicture.Image.Height/ _cPicture.Size.Height  ))));
                _RectEndPointControl = e.Location;
                if (Math.Abs(_RectEndPointControl.X-_RectStartPointControl.X)>10&&Math.Abs(_RectEndPointControl.Y-_RectStartPointControl.Y)>10)
                {
                    _ROIControl = new Rectangle(_RectStartPointControl.X, _RectStartPointControl.Y, Math.Abs(_RectEndPointControl.X - _RectStartPointControl.X), Math.Abs(_RectEndPointControl.Y - _RectStartPointControl.Y));
                    _ROIImage = new Rectangle(_RectStartPointImage.X, _RectStartPointImage.Y, Math.Abs(_RectEndPointImage.X - _RectStartPointImage.X), Math.Abs(_RectEndPointImage.Y - _RectStartPointImage.Y));
                    this._cPicture.Invalidate();
                }
            }
        }

        private void _cPicture_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (_cPicture.Image != null && (_DrawROI || _selectingROI))
            {
                if (_ROIControl != null && _ROIControl.Width > 0 && _ROIControl.Height > 0)
                {
                    e.Graphics.DrawRectangle(selectionBrush, _ROIControl);
                 
                }
            }
        }

        private void zoom_Click(object sender, EventArgs e)
        {
            if (_cPicture.Image != null)
            {
                zoom.Enabled = false;
                Imagen formImagen = new Imagen();
                formImagen._imagen.Image = (Image)_cPicture.Image.Clone();
                formImagen.TopMost = true;
                formImagen.FormClosing += new FormClosingEventHandler(formImagen_FormClosing);
                formImagen.Show();
            }
        }

        void formImagen_FormClosing(object sender, FormClosingEventArgs e)
        {
            zoom.Enabled = true;
        }

        private void _cPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _selectingROI)
            {
                _RectEndPointImage = new Point((int)Math.Round((double)(e.Location.X * _cPicture.Image.Width / _cPicture.Size.Width)), (int)(Math.Round((double)(e.Location.Y * _cPicture.Image.Height / _cPicture.Size.Height))));
                _RectEndPointControl = e.Location;
                if (Math.Abs(_RectEndPointControl.X - _RectStartPointControl.X) > 10 && Math.Abs(_RectEndPointControl.Y - _RectStartPointControl.Y) > 10)
                {
                    _ROIControl = new Rectangle(_RectStartPointControl.X, _RectStartPointControl.Y, Math.Abs(_RectEndPointControl.X - _RectStartPointControl.X), Math.Abs(_RectEndPointControl.Y - _RectStartPointControl.Y));
                    _ROIImage = new Rectangle(_RectStartPointImage.X, _RectStartPointImage.Y, Math.Abs(_RectEndPointImage.X - _RectStartPointImage.X), Math.Abs(_RectEndPointImage.Y - _RectStartPointImage.Y));
                    this._cPicture.Invalidate();
                }
            }
        }

       

        
    }
}
