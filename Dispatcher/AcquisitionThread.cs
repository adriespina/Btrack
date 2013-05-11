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
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Reflection;

namespace Billetrack
{
    class AcquisitionThread : SpinThreadWhile
    {
        BilletrackDispatcher _Padre;
        dynamic _Parametros;
        CGenieCamera Camera;       
        bool UsingCamera = true;
        Billet _CurrentBillet;

   
        public AcquisitionThread(BilletrackDispatcher padre, string name, dynamic parametros)
            : base(name)
        {
            try
            {
            
                _Padre = (BilletrackDispatcher)padre;
                _Parametros = parametros;
                UsingCamera = parametros.Camera.USING_CAMERA;
               
            }
            catch (Exception e)
            {
                
                  
            }        

        }
        public override void FunctionToExecuteByThread()
        {

            try
            {
                List<EventBilletrack> ImageEvents = new List<EventBilletrack>();

                //get the last Image grabbed by the camera
                Image<Gray, byte> Image = Camera.GetImage();
                if (Image == null)
                {
                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).Camera = false;
                    return;
                }
                else ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).Camera = true;


                //save the online image 
                using (Image<Rgb, byte> imagen_labels = _Padre._Aux.StampTimeAndSaveImg(Image, true))
                {
                    //send the image to be displayed in the form
                    ((SharedData<CurrentImage>)_SharedMemory["CurrentImage"]).Add(new CurrentImage(imagen_labels.Copy()));
                }
                _Padre.PrepareEvent(_Name);
                //check if the billet trigger is set
                if (_Events["BilletToProcess"].WaitOne(0, true))
                {
                    //solo sirve para cuando trabajamos sin camara
                    Camera.NextFileImage();
                    //Get the information of Last Billet                 
                    _CurrentBillet = (Billet)((SharedData<Billet>)_SharedMemory["LastBillet"]).Get(0);
                    _CurrentBillet.Time = DateTime.Now;

                    //Logging
                    _Parametros.LOGTXTMessage = "New Billet acquired. Cast: " + _CurrentBillet.Family.Cast + " Line : " + _CurrentBillet.Line.ToString("00");
                    _Padre._Log.SetData(ref _Parametros, "Informacion");

                    //Check the if the light is on
                    if (!_Padre._Aux.m_SpotlightOnOff.IsPresent)
                    {
                        ImageEvents.Add(EventBilletrack.NoLight);
                        //Logging
                        _Parametros.LOGTXTMessage = "No light detected. Cast: " + _CurrentBillet.Family.Cast + " Line : " + _CurrentBillet.Line.ToString("00");
                        _Padre._LogError.SetData(ref _Parametros, "Informacion");
                        ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).Light = false;
                    }

                    ((State)((SharedData<State>)_SharedMemory["State"]).Get(0)).Light = true;
                    //check other events


                    //Check if Billet is in front the camera
                    int tries = 0;
                    while (tries < _Parametros.Detection.BILLET_SEARCH_FRAMES_TO_WAIT && tries >= 0)
                    {
                        if (_Padre._Aux.m_billetSearch.IsPresent)
                        {
                            tries = -1;
                            continue;
                        }
                        else
                        {
                            //Try with a new image
                            Image = Camera.GetImage();
                            tries++;
                        }
                    }
                    //if billet is not there I save the image in the database
                    if (tries >= _Parametros.Detection.BILLET_SEARCH_FRAMES_TO_WAIT)
                    {
                        ImageEvents.Add(EventBilletrack.NoDetected);
                        _Padre._BilletrackDB.InsertEmptyImage(ImageEvents, _CurrentBillet);

                        //Logging
                        _Parametros.LOGTXTMessage = "No Billet detected. Cast: " + _CurrentBillet.Family.Cast + " Line : " + _CurrentBillet.Line.ToString("00");
                        _Padre._LogError.SetData(ref _Parametros, "Informacion");
                    }
                    // if billet was detected
                    else
                    {
                        //Autoexposure setting
                        if (UsingCamera)
                            Camera.ExposureTime = _Padre._Aux.CalculateExposureTimeSimple((int)_Padre._Aux.m_billetSearch.valueCalculated, 140, Camera.ExposureTime);
                        //Send the image to be processed
                        ((SharedData<Images>)_SharedMemory["LastImage"]).Add(new Images(Image.Copy(), ImageEvents, _CurrentBillet, Camera.ExposureTime));
                        _Events["BilletDetected"].Set();

                    }

                    //free the image resources
                    Image.Dispose();
                }
            }
            catch (Exception e)
            {

                //Logging
                _Parametros.LOGTXTMessage = "Error in Acquisition loop: " + e.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
            }

        }
        public override void Initializate()
        {

            try
            {
                _MillisecondsToSleep = 400;
                Camera = new CGenieCamera(1, 0, UsingCamera);
                Camera.MIN_EXPOSURE = _Parametros.Camera.MIN_EXPOSURE;
                Camera.MAX_EXPOSURE = _Parametros.Camera.MAX_EXPOSURE;
                Camera.MIN_FRAMERATE = _Parametros.Camera.MIN_FRAMERATE;
                Camera.MAX_FRAMERATE = _Parametros.Camera.MAX_FRAMERATE;
                Camera.PATH_IMAGE_SOURCE = _Parametros.Camera.PATH_IMAGE_SOURCE;
                if (UsingCamera) Camera.GrabAsync();
              
            }
            catch (Exception err)
            {
                //Logging
                _Parametros.LOGTXTMessage = "Errorcreating camera " + err.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");               
            }

 
            

        }
        public override void Closing()
        {
            Camera.Close();
           
        }

        
       


    }
}
