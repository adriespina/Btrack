using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Dynamic;
using System.Management;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Reflection;
using System.IO;

namespace Billetrack
{
    delegate void delegateDisplayResults(dynamic result);
    public partial class FormBilletrack : Form
    {

        BilletrackDispatcher dispatch;
        delegateDisplayResults d_DisplayResults;  //puntero a la funcion de pintar   
        dynamic ConfigData;
        int cont_disk_space = 0;//contador para borrar el disco duro 
      

        public FormBilletrack()
        {
           
            InitializeComponent();
            dispatch = new BilletrackDispatcher();
            dispatch.NewResultEvent += new SpinPlatform.Dispatcher.ResultEventHandler(dispatch_NewResultEvent);
            d_DisplayResults = new delegateDisplayResults(DisplayResults);

            ConfigData = new ExpandoObject();
            dispatch.Init(ConfigData);   
            dispatch.Start();        
            timer_State.Enabled = true;
          
            _cConfiguration._resetApplicationButton.Click += new EventHandler(_resetApplicationButton_Click);
       
        }

        void _resetApplicationButton_Click(object sender, EventArgs e)
        {
            dispatch.Stop();
            dispatch.Init(ConfigData);
            dispatch.Start();
        }

        void dispatch_NewResultEvent(object sender, SpinPlatform.Data.DataEventArgs res)
        {

            try
            {
                this.BeginInvoke(d_DisplayResults, res.DataArgs);
            }
            catch (Exception)
            {
            }
        }
        void DisplayResults(dynamic datos)
        {
            if (datos.TRIErrors == "")
            {
                foreach (string data in datos.TRIReturnedData)
                {
                    switch (data)
                    {
                        #region CurrentImage
                        case "CurrentImage":

                            CurrentImage img = (CurrentImage)datos.CurrentImage;
                            _cMain._cOriginalObj.setImage(img.Image.ToBitmap());                      
                            

                            break;
# endregion
                        #region MatchInfo
                        case "MatchInfo":

                            Match matched = (Match)datos.MatchedInfo;
                              _cMain._cResultsCroppedObj._cPicture.Image = matched.Image_Cropped.ToBitmap();
                            if (matched.Image_Matched != null) _cMain._cResultsMatchedObj._cPicture.Image = matched.Image_Matched.ToBitmap();
                            else
                            {
                                using (Image<Gray, byte> imgen = new Image<Gray, byte>(200, 200))
                                {
                                    _cMain._cResultsMatchedObj._cPicture.Image = imgen.ToBitmap();
                                }
                            }

                            if (matched.Billet != null)
                            {
                                _cMain._labelCastCalculated.MainText = matched.Billet.Family.Cast;
                                _cMain._labelLineCalculated.MainText = matched.Billet.Line.ToString("0");
                                _cMain._labelNumberCalculated.MainText = matched.Billet.NCut.ToString("00");
                                _cMain._labelError.MainText = " OK ";
                            }
                            else
                            {
                                _cMain._labelCastCalculated.MainText = "_";
                                _cMain._labelLineCalculated.MainText = "_";
                                _cMain._labelNumberCalculated.MainText = "_";
                                _cMain._labelError.MainText = " BILLET NOT FOUND ";
                            }

                            matched.Dispose();
                            break;
                        # endregion
                        #region LastBillet
                        case "LastBillet":

                            Billet billet = (Billet)datos.LastBillet;
                            _cMain._labelCast.MainText = billet.Family.Cast;
                            _cMain._labelLine.MainText = billet.Line.ToString("0");
                            _cMain._labelNumber.MainText = billet.NCut.ToString("00");
                            _cMain._labelDate.MainText = billet.Time.ToString();
                            
                            break;
                        # endregion
                        #region State
                        case "State":

                            State Estado = (State)datos.State;

                            if (Estado != null)
                            {                              
                                _cConProcessComputer.StatusConnection = (SpinPlatform.Controls.spinConnectionStatus)Estado.Socket;
                                if (Estado.RemoteDatabase) _cConDBRemote.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.connected;
                                else _cConDBRemote.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
                                if (Estado.Light) _cConLight.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.connected;
                                else _cConLight.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
                                if (Estado.FTP) _cConFTP.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.connected;
                                else _cConFTP.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
                                if (Estado.Camera) _cConCamera.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.connected;
                                else _cConCamera.StatusConnection = SpinPlatform.Controls.spinConnectionStatus.disconnected;
                               
                            }
                            //BUG 
                            try
                            {
                                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                                disk.Get();
                                if (double.Parse(disk["FreeSpace"].ToString()) / Math.Pow(1024, 3) > 1)
                                    _cConHardDrive.Text = "HD = " + Math.Round(double.Parse(disk["FreeSpace"].ToString()) / Math.Pow(1024, 3), 2) + "GB";
                                else if (double.Parse(disk["FreeSpace"].ToString()) / Math.Pow(1024, 2) > 1)
                                    _cConHardDrive.Text = "HD = " + Math.Round(double.Parse(disk["FreeSpace"].ToString()) / Math.Pow(1024, 2), 2) + "MB";
                                else if (double.Parse(disk["FreeSpace"].ToString()) / 1024 > 1)
                                    _cConHardDrive.Text = "HD = " + Math.Round(double.Parse(disk["FreeSpace"].ToString()) / 1024, 2) + "KB";
                                else
                                    _cConHardDrive.Text = "HD = " + Math.Round(double.Parse(disk["FreeSpace"].ToString()), 2) + "Bytes";

                                //if (cont_disk_space==0)
                                //{
                                //    if (double.Parse(disk["FreeSpace"].ToString()) / Math.Pow(1024, 3)<5)
                                //    Free_Disk();
                                //}
                                //cont_disk_space++;
                                disk.Dispose();
                            }
                            catch (Exception e)
                            {

                            }
                            break;
                        #endregion
                    }
                }
            }
        }


        #region eventos menu superior
        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cImageAnalysis.CloseCamera();
            if (panel1.Controls.Contains(_cMatchingOffline) && _cMatchingOffline._running)
            {
                if (MessageBox.Show("You are running a matching experiment, do you want it to run in background?", "Alarm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                    _cMatchingOffline.play_Click(this, null);

            }
            if (!panel1.Controls.Contains(_cMain))
            {
                SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200,panel1,"",global::Billetrack.Properties.Resources.bg22);
                a.Show();

                panel1.Controls.Clear();
                panel1.Controls.Add(_cMain);
                panel1.Refresh();

                dispatch.Start();
                timer_State.Enabled = true;

                a.CloseLoading();
            }
            
        }
        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cImageAnalysis.CloseCamera();
            if (panel1.Controls.Contains(_cMatchingOffline) && _cMatchingOffline._running)
            {
                if (MessageBox.Show("You are running a matching experiment, do you want it to run in background?", "Alarm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                    _cMatchingOffline.play_Click(this, null);

            }
            if (!panel1.Controls.Contains(_cConfiguration))
            {
                SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel1, "", global::Billetrack.Properties.Resources.bg22);
                a.Show();

                panel1.Controls.Clear();
                _cConfiguration.LoadNewConfiguration();
                panel1.Controls.Add(_cConfiguration);
                panel1.Refresh();

                a.CloseLoading();
            }
        }
      

        private void FormBilletrack_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cImageAnalysis.CloseCamera();
            //BUG no cierra bien
            timer_State.Enabled = false;
          //  _cMain.Dispose();
            dispatch.Stop();

            if (panel1.Controls.Contains(_cMatchingOffline))
            {
                if (_cMatchingOffline.classificator!=null) _cMatchingOffline.classificator.Dispose();
            
            }
        }
        
        #endregion

        private void timer_State_Tick(object sender, EventArgs e)
        {
            dispatch.PrepareEvent("State");
        }

        private void matchingFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cImageAnalysis.CloseCamera();
            if (panel1.Controls.Contains(_cMain))
            {
                dispatch.Stop();
                timer_State.Enabled = false;

            }
            if (!panel1.Controls.Contains(_cMatchingOffline))
            {
                SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel1, "", global::Billetrack.Properties.Resources.bg22);
                a.Show();

                panel1.Controls.Clear();
                panel1.Controls.Add(_cMatchingOffline);
                panel1.Refresh();

                a.CloseLoading();
            }
          
        }

        private void imageAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Contains(_cMain))
            {
                dispatch.Stop();
                timer_State.Enabled = false;

            }
            if (panel1.Controls.Contains(_cMatchingOffline) && _cMatchingOffline._running)
            {
                if (MessageBox.Show("You are running a matching experiment, do you want it to run in background?", "Alarm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                    _cMatchingOffline.play_Click(this, null);

            }
            if (!panel1.Controls.Contains(_cImageAnalysis))
            {
                SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel1, "", global::Billetrack.Properties.Resources.bg22);
                a.Show();

                panel1.Controls.Clear();
                panel1.Controls.Add(_cImageAnalysis);
                panel1.Refresh();
                _cImageAnalysis.OpenCamera();
                a.CloseLoading();
            }
          
        }

        private void FormBilletrack_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                SpinPlatform.Controls.spinLoadingScreenForm a = new SpinPlatform.Controls.spinLoadingScreenForm(200, panel1, "BILLETRACK", 60, global::Billetrack.Properties.Resources.bg22);
                a.Show();
                System.Threading.Thread.Sleep(1000);
                panel1.Controls.Clear();
                panel1.Controls.Add(_cMain);
                panel1.Refresh();

                a.CloseLoading();
            }
            else
                panel1.Controls.Add(_cMain);
        }
    }
}
