using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using SpinPlatform.FTP;
using SpinPlatform.Errors;

namespace Billetrack
{
    class ProcessingThread : SpinThreadEvent
    { 
        BilletrackDispatcher _Padre;
        dynamic _Parametros;
        string path_image, total_path, path_image_original, total_path_orig, path_sinimagen;
        int CurrentID;
        CMatching match;
        Images ImageInfo;
        imgStats stats;
        Image<Gray, byte> CroppedImg;
        Billet _MatchedBillet;
        


        public ProcessingThread(BilletrackDispatcher padre, string name, dynamic parametros)
             : base(name)
        {
            _Padre = (BilletrackDispatcher)padre;
            _Parametros = parametros;

            match = new CMatching(Environment.ProcessorCount);
            match.THRESHOLD_INSIDE = parametros.Match.THRESHOLD_INSIDE;
            match.THRESHOLD_QUALITY = parametros.Match.THRESHOLD_QUALITY;
            match.MATCHING_TIMEOUT = parametros.Match.MATCHING_TIMEOUT;
            match.DOUBLE_CHECK = parametros.Match.DOUBLE_CHECK;
            
        }
        public override void FunctionToExecuteByThread()
        {

            try
            {
              
                while (!((SharedData<Images>)_SharedMemory["LastImage"]).Vacio)
                {

                    CurrentID = -1;
                    //Obtain the image of the acquisition Thread
                    ImageInfo = (Images)((SharedData<Images>)_SharedMemory["LastImage"]).Pop();
                  

                    //Crop and rotate the image
                    CroppedImg = _Padre._Aux.RotateAndCropImage(ImageInfo.Image);
                    //MessageBox.Show("girada y crop la imagen en processing");

                    //Obtain the path to save the images and descriptors
                    path_sinimagen = _Padre._BilletrackDB.Factory.Name + "\\" + DateTime.Now.Year.ToString("0000") + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00") + "\\" + ImageInfo._Billet.Family.Cast + "\\";
                    path_image = path_sinimagen + ImageInfo._Billet.Family.Cast + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".jpg";
                    total_path = _Padre._BilletrackDB.Factory.PathImages + path_image;
                    path_image_original = path_image.Substring(0, path_image.Length - 4) + "_ORIGINAL.jpg";
                    total_path_orig = total_path.Substring(0, total_path.Length - 4) + "_ORIGINAL.jpg";

                    //MessageBox.Show("calculados los path en processing");
                    //save the images
                    if (!Directory.Exists(_Padre._BilletrackDB.Factory.PathImages + path_sinimagen)) Directory.CreateDirectory(_Padre._BilletrackDB.Factory.PathImages + path_sinimagen);
                    ImageInfo.Image.Save(total_path_orig);
                    CroppedImg.Save(total_path);

                    //MessageBox.Show("guardadas las imagens en processing");

                    //Calculate the image statistics
                    stats = _Padre._Aux.CalculateStatsImage(CroppedImg, ImageInfo.ExposureTime);

                    //MessageBox.Show("calculadas las estadisticas en processing");

                    //Insert the image in the database and its events
                    InsertImage();

                    //MessageBox.Show("Inserted the image in the database and its events en processing");

                    //Obtain the descriptors and save 
                    Descriptors();
                    //MessageBox.Show("Obtained the descriptors and save  en processing");

                    //Send the images to the next Factories if needed
                    //Also send the images to a backup location if needed
                    SendFTP();
                  
                    //MessageBox.Show("sent the descriptors and save  en processing");

                    //search the match for the current image if needed
                    Image<Gray, byte> matched_image = Match(ref _MatchedBillet);

                    //send the images to be displayed in the form
                    if (matched_image != null) ((SharedData<Match>)_SharedMemory["MatchInformation"]).Add(new Match(CroppedImg.Copy(), matched_image.Copy(), _MatchedBillet));
                    else ((SharedData<Match>)_SharedMemory["MatchInformation"]).Add(new Match(CroppedImg.Copy()));
                    _Padre.PrepareEvent(_Name);


                    //Dispose the variables
                    if (matched_image != null) matched_image.Dispose();
                    if (CroppedImg != null) CroppedImg.Dispose();
                    if (ImageInfo != null) ImageInfo.Dispose();
                }
            }
            catch (Exception err)
            {
                   MessageBox.Show("Error processing : " + err.Message);
                //Logging
                _Parametros.LOGTXTMessage = "Error in processing loop: " + err.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
            }


        }

        private void Descriptors()
        {
            CSurf surf_image = new CSurf(CroppedImg);
            surf_image.Save(total_path);
            surf_image.Dispose();

            //Logging
            _Parametros.LOGTXTMessage = "Images processed and saved. Cast: " + ImageInfo._Billet.Family.Cast + " Line : " + ImageInfo._Billet.Line.ToString("00") + "\nSending via FTP";
            _Padre._Log.SetData(ref _Parametros, "Informacion");
        }

        private void InsertImage()
        {
            try
            {
                CurrentID = _Padre._BilletrackDB.InsertImage(path_image, stats, ImageInfo._Billet);
                _Padre._BilletrackDB.InserEvent(CurrentID, ImageInfo.Events);
                ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = true;

            }
            catch (Exception e)
            {
                ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = false;
                throw new SpinException("Error inserting match in database : " + e.Message);
            }
        }

        private void SendBillet(Billet MatchedBillet, List<EventBilletrack> events)
        {
            if(_Padre._BilletrackDB.Factory.Name.Contains("ACERIA"))    ((PCComClientThread)_Padre._DispatcherThreads["Communication"]).SendBilletSteelMaking(MatchedBillet, events);
            if(_Padre._BilletrackDB.Factory.Name.Contains("ALAMBRON"))     ((PCComClientThread)_Padre._DispatcherThreads["Communication"]).SendBilletRodMill(MatchedBillet, events);
        }

        private Image<Gray, byte> SearchMatching(string searchparameter, out double quality, out int matchedID)
        {
            Image<Gray, byte> img;
            if (_Padre._BilletrackDB.Billetrack.Origin != null)
            {
                Dictionary<int, string> Candidates;
                //ask for all the images that can be the same billet in previous factory place
                try
                {
                    Candidates = _Padre._BilletrackDB.GetCandidates(searchparameter);
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = true;

                }
                catch (Exception e)
                {
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = false;
                    throw new SpinException("Error getting candidates : " + e.Message);
                }
                if (Candidates != null)
                {
                    int[] CandidatesID = new int[Candidates.Count];
                    string[] CandidatesPaths = new string[Candidates.Count];

                    int i = 0;
                    //save the candidates ID and candidates path in separated arrays
                    foreach (KeyValuePair<int, string> entry in Candidates)
                    {
                        CandidatesID[i] = entry.Key;
                        CandidatesPaths[i] = entry.Value;
                        i++;
                    }
                    resultMatching[] resultados = new resultMatching[CandidatesID.Length];


                    //Perform the matching between the image and all the candidates
                    int position_max = match.MatchingOneToVarius(total_path, ref CandidatesPaths, out resultados);

                    if (position_max >= 0)
                    {
                        //if matching was succesful, retrun the matchedID and quality
                        quality = (double)resultados[position_max].quality;
                        matchedID = CandidatesID[position_max];
                        img = new Image<Gray, byte>(CandidatesPaths[position_max]);
                        //delete descriptors files from hard disk
                        File.Delete(CandidatesPaths[position_max] + CSurf.EXTENSION_DESCRIPTORS);
                        File.Delete(CandidatesPaths[position_max] + CSurf.EXTENSION_KEYPOINTS);
                        return img;
                    }
                    else
                    {
                        quality = 0;
                        matchedID = -1;
                        return null;
                        //if matching was wrong 

                    }

                }
                else
                {
                    quality = 0;
                    matchedID = -1;
                    return null;
                    //if matching was wrong 

                }
            }
            else
            {
                quality = 0;
                matchedID = -1;
                return null;
                //if matching was wrong 

            }
        }


        public override void Initializate()
        {
            _WakeUpThreadEvent = _Events["BilletDetected"];

        }
        public override void Closing()
        {


        }

        public override bool Stop()
        {
            if (match.smartThreadPool != null)
            {
                match.smartThreadPool.Cancel(true);

                //CUIDADO!!!! de esta manera no espera a que acabe de manera controlado pasando por el closing
                //Lo hago asi para que no se quede esperando por el timeout del smartThreadPool (15 seg)
                this._MainThread.Abort();
            }

            return base.Stop();
        }
        private void SendFTP()
        {
           
            if (_Padre._BilletrackDB.Billetrack.Destiny != null)
            {

                try
                {
                    SpinFTP ftp = new SpinFTP();
                  
                    foreach (Factory factory in _Padre._BilletrackDB.Billetrack.Destiny)
                    {
                        //START THE COMMUNICATION
                       
                        ftp.Connect(factory.HostFTP, factory.UserFTP, factory.PasswordFTP);
                        ftp.Start();
                        //Send the cropped image
                        ftp.UploadFile(total_path, path_sinimagen);
                        //send the original image
                        ftp.UploadFile(total_path_orig, path_sinimagen);
                        //send the descriptors
                        ftp.UploadFile(total_path + CSurf.EXTENSION_DESCRIPTORS, path_sinimagen);
                        //send the keypoints
                        ftp.UploadFile(total_path + CSurf.EXTENSION_KEYPOINTS, path_sinimagen);
                        ftp.Stop();
                        ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).FTP = true;

                    }
                }
                catch (Exception error)
                {
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).FTP = false;

                    //throw new SpinException("Error sending the files to the next factory" + error.Message);
                }
            }
           
            if (_Padre._BilletrackDB.Billetrack.Backup != null)
            {

                try
                {
                    SpinFTP ftp = new SpinFTP();

                    //START THE COMMUNICATION
                    //MessageBox.Show("connecting ftp : " + _Padre._BilletrackDB.Billetrack.Backup.HostFTP + " " + _Padre._BilletrackDB.Billetrack.Backup.UserFTP + " " + _Padre._BilletrackDB.Billetrack.Backup.PasswordFTP);
                    ftp.Connect(_Padre._BilletrackDB.Billetrack.Backup.HostFTP, _Padre._BilletrackDB.Billetrack.Backup.UserFTP, _Padre._BilletrackDB.Billetrack.Backup.PasswordFTP);
                    ftp.Start();
                    //Send the cropped image
                    ftp.UploadFile(total_path, path_image);
                    //send the original image
                    ftp.UploadFile(total_path_orig, path_image_original);
                    //send the descriptors
                    ftp.UploadFile(total_path + CSurf.EXTENSION_DESCRIPTORS, path_image + CSurf.EXTENSION_DESCRIPTORS);
                    //send the keypoints
                    ftp.UploadFile(total_path + CSurf.EXTENSION_KEYPOINTS, path_image + CSurf.EXTENSION_KEYPOINTS);
                    ftp.Stop();
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).FTP = true;


                }
                catch (Exception error)
                {
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).FTP = false;
                    MessageBox.Show("error sending ftp : "+error.Message);
                    //throw new SpinException("Error sending the files to the backup" + error.Message);
                }


            }
        }
        private Image<Gray, byte> Match(ref Billet MatchedBillet)
        {
            string searchparameter = ImageInfo._Billet.Family.Cast;
            double quality = 0;
            int matchedID = -1;
            if (MatchedBillet!=null) MatchedBillet.Dispose();
            Image<Gray, byte> matched_image = SearchMatching(searchparameter, out quality, out matchedID);
            if (_StopEvent.WaitOne(0, true))
            {              
                return null;
            }

          
            //if matching was succesful, insert the match in the database
           

            try
            {
                MatchedBillet = _Padre._BilletrackDB.InsertMatch(CurrentID, matchedID, quality);
                ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = true;

            }
            catch (Exception e)
            {
               
                ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).RemoteDatabase = false;

                throw new SpinException("Error inserting match in database : " + e.Message);
            }
            if (MatchedBillet!=null) MatchedBillet.Time = ImageInfo._Billet.Time;

            //send the images to the Process Computer
            SendBillet(MatchedBillet, ImageInfo.Events);

            if (_Padre._BilletrackDB.Billetrack.Origin != null)
            {
                if (MatchedBillet != null)
                {
                    //Logging
                    _Parametros.LOGTXTMessage = "New matching. Cast: " + ImageInfo._Billet.Family.Cast + " Line : " + ImageInfo._Billet.Line.ToString("00") + " really was  Cast: " + MatchedBillet.Family.Cast + " Line : " + MatchedBillet.Line.ToString("00");
                    _Padre._Log.SetData(ref _Parametros, "Informacion");
                }
                else
                {
                    //Logging
                    _Parametros.LOGTXTMessage = "No matching found. Cast: " + ImageInfo._Billet.Family.Cast + " Line : " + ImageInfo._Billet.Line.ToString("00");
                    _Padre._Log.SetData(ref _Parametros, "Informacion");
                    _Padre._LogError.SetData(ref _Parametros, "Informacion");
                }
            }
            return matched_image;

        }
    }

}
