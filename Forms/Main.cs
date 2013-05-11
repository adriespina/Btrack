using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpinPlatform.Controls;
using System.Threading;
using System.Dynamic;
using System.Drawing.Drawing2D;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Billetrack.Forms
{
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
            _cResultsCroppedObj._cPicture.Image = null;
            _cResultsMatchedObj._cPicture.Image = null;
        }
           

        #region eventos anim

        private void _cSliderDatosPic_Click(object sender, EventArgs e)
        {
            SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(200, this, "", global::Billetrack.Properties.Resources.bg22);
            b.Show();

            if (this.Controls.Contains(_cPanelDatos))
            {
                _cSliderDatosPic.Image = global::Billetrack.Properties.Resources.splittershow;
                this.Controls.RemoveByKey("_cPanelDatos");
            }
            else
            {
                _cSliderDatosPic.Image = global::Billetrack.Properties.Resources.splitterhide;
                this.Controls.Add(_cPanelDatos);
            }
            this.Refresh();
            b.CloseLoading();
        }

        private void _cSliderResultsPic_Click(object sender, EventArgs e)
        {
            SpinPlatform.Controls.spinLoadingScreenForm b = new SpinPlatform.Controls.spinLoadingScreenForm(200, this, "", global::Billetrack.Properties.Resources.bg22);
            b.Show();

            if (this.Controls.Contains(_cResultsPanel))
            {
                _cSliderResultsPic.Image = global::Billetrack.Properties.Resources.splitterhide;
                this.Controls.RemoveByKey("_cResultsPanel");
            }
            else
            {
                _cSliderResultsPic.Image = global::Billetrack.Properties.Resources.splittershow;
                this.Controls.Add(_cResultsPanel);
            }
            this.Refresh();
            b.CloseLoading();
        }

        #endregion


    }
}
