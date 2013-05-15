using System;
using System.Collections.Generic;
using System.Text;
using DALSA.SaperaLT.SapClassBasic;
using System.IO;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Reflection;
using System.Windows.Forms;


namespace Billetrack
{
    /// <summary>
    /// Class with methods and Properties to use a Dalsa Genie m1400 Camera with EMGU libraries
    /// Cesar Fraga/Adrian Espina February 2013
    /// </summary>
    class CGenieCamera
    {
     

        public const int MAX_CONFIG_FILES = 36;       // 10 numbers + 26 letters
        public int MIN_EXPOSURE = 32;
        public int MAX_EXPOSURE = 66566;

        public int MIN_FRAMERATE = 100;
        public int MAX_FRAMERATE = 15000;

        private string Path_image_source = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_alam\";

        public string PATH_IMAGE_SOURCE
        {
            get { return Path_image_source; }
            set
            {
                Path_image_source = value;

                imgs = new List<string>();
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Path_image_source);
                System.IO.FileInfo[] files = null;
                files = di.GetFiles("*.jpg");
                foreach (System.IO.FileInfo fi in files)
                {

                    if (fi.FullName.Contains("ORIGINAL")) imgs.Add(fi.FullName);

                }


            }
        }
        List<string> imgs;//para guardar la lista de imagenes en el Path_image_source
        int it = 0;//Para recorrer la lista de archivos

        SapAcquisition Acq = null;
        SapAcqDevice AcqDevice = null;
        SapBuffer Buffers = null;
        public SapTransfer Transfer = null;
        SapView View = null;
        MyAcquisitionParams acqParams;
        bool m_initialized;					/*!< Bool que nos indica si la clase se ha inicializado correctamente. */
        bool usingCamera=true;					/*!< Bool que nos indica si la clase se ha inicializado correctamente. */
        Image<Gray, Byte> Current_image;
        object locker;
        /// <summary>
        /// Width of the images acquired by the camera
        /// </summary>
        public int Width
        {
            get
            {
                if (!m_initialized)
                {
                    return -1;
                }
                else
                {

                    return Buffers.Width;
                }
            }
          
        }
        /// <summary>
        ///  Height of the images acquired by the camera
        /// </summary>
        public int Height
        {
            get
            {
                if (!m_initialized)
                {
                    return -1;
                }
                else
                {

                    return Buffers.Height;
                }
            }

        }
        /// <summary>
        /// Exposure time of the camera
        /// </summary>
        public int ExposureTime
        {
            get
            {
                if (usingCamera)
                {
                    int exposure_time = -1;

                    if (!m_initialized)
                    {
                        return -1;
                    }

                    else if (AcqDevice.GetFeatureValue("ExposureTime", out  exposure_time) == true)
                    {
                        return exposure_time;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else return 0;
            }
            set
            {
                try
                {
                    if (!m_initialized)
                    {

                    }
                    else
                    {
                        if (value < MIN_EXPOSURE)
                        {
                            value = MIN_EXPOSURE;
                        }

                        else if (value > MAX_EXPOSURE)
                        {
                            value = MAX_EXPOSURE;
                        }

                        AcqDevice.SetFeatureValue("ExposureTime", value);

                    }
                }
                catch (Exception e)
                {                    
                     throw new SpinPlatform.Errors.SpinException("CGenieCamera: ExposureTime: " + e.Message);
                }
            }
        }
        /// <summary>
        /// Frame rate used by the camera
        /// </summary>
        public int FrameRate
        {
            get
            {
                int FrameRate = -1;

                if (!m_initialized)
                {
                    return -1;
                }

                else if (AcqDevice.GetFeatureValue("FrameRate", out  FrameRate) == true)
                {
                    return FrameRate;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    if (!m_initialized)
                    {

                    }
                    else
                    {
                        if (value < MIN_FRAMERATE)
                        {
                            value = MIN_FRAMERATE;
                        }

                        else if (value > MAX_FRAMERATE)
                        {
                            value = MAX_FRAMERATE;
                        }

                        AcqDevice.SetFeatureValue("FrameRate", value);

                    }
                }
                catch (Exception e)
                {
                    
                    throw new SpinPlatform.Errors.SpinException("CGenieCamera: FrameRate: " + e.Message);
                }
            }
        }
        /// <summary>
        /// Internal temperature of the Camera
        /// </summary>
        public int InternalTemperature
        {
            get
            {
                int InternalTemperature = -1;

                if (!m_initialized)
                {
                    return -1;
                }

                else if (AcqDevice.GetFeatureValue("InternalTemperature", out  InternalTemperature) == true)
                {
                    return InternalTemperature;
                }
                else
                {
                    return 0;
                }
            }
           
        }
        /// <summary>
        /// Flag that indicates if the camera is grabbing
        /// </summary>
        public bool IsGrabbing
        {
            get
            {             

                if (!m_initialized)
                {
                    return false;
                }
                else
                {
                    return Transfer.Grabbing;
                }
            }

        }

        /// <summary>
        /// Constructor of the CGenieCamera Class. It is possible to specify the server and the camara used
        /// </summary>
        /// <param name="server">camera server index. Note that index=0 is ths system system and it is not valid</param>
        /// <param name="camera">camera index to select a camera</param>
        public CGenieCamera(int server = 1, int camera = 0, bool usingcamera=true) {

            try
            {
                m_initialized = false;

                if (usingcamera)
                {
                    locker = new object();

                    acqParams = new MyAcquisitionParams();
                    InitAcqParams(acqParams, server, camera);
                    SapLocation loc = new SapLocation(acqParams.ServerName, acqParams.ResourceIndex);
                    if (SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.Acq) > 0)
                    {
                        Acq = new SapAcquisition(loc, acqParams.ConfigFileName);
                        Buffers = new SapBuffer(1, Acq, SapBuffer.MemoryType.ScatterGather);
                        Transfer = new SapAcqToBuf(Acq, Buffers);

                        // Create acquisition object
                        if (!Acq.Create())
                        {
                            DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
                            throw new Exception("Error during SapAcquisition creation!\n");

                        }
                    }

                    if (SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.AcqDevice) > 0)
                    {
                        AcqDevice = new SapAcqDevice(loc, acqParams.ConfigFileName);
                        Buffers = new SapBuffer(1, AcqDevice, SapBuffer.MemoryType.ScatterGather);
                        Transfer = new SapAcqDeviceToBuf(AcqDevice, Buffers);

                        // Create acquisition object
                        if (!AcqDevice.Create())
                        {
                            DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
                            throw new Exception("Error during SapAcqDevice creation!\n");

                        }
                    }
                    //View = new SapView(Buffers);

                    // End of frame event
                    Transfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                    Transfer.XferNotify += new DALSA.SaperaLT.SapClassBasic.SapXferNotifyHandler(CallbackNewFrame);
                    Transfer.XferNotifyContext = this;

                    // Create buffer object
                    if (!Buffers.Create())
                    {
                        DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
                        throw new Exception("Error during SapBuffer creation!\n");
                    }


                    // Create buffer object
                    if (!Transfer.Create())
                    {
                        DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
                        throw new Exception("Error during SapTransfer creation!\n");
                    }

                    // Create buffer object
                    //if (!View.Create())
                    //{
                    //    DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
                    //    throw new Exception("Error during SapView creation!\n");
                    //}

                    Current_image = new Image<Gray, byte>(Buffers.Width, Buffers.Height);
                }
                else
                {
                    usingCamera = usingcamera;
                }
                m_initialized = true;
            }
            catch (Exception e)
            {
                m_initialized = false;
                throw new SpinPlatform.Errors.SpinException("CGenieCamera: Error creating the Camera :" + e.Message);
            }
            

        }
        /// <summary>
        /// Obtain the last Image grabbed by the camera
        /// </summary>
        /// <returns>the last image of the camera</returns>
        public Image<Gray, byte> GetImage()
        {

            try
            {
                if (!m_initialized)
                {
                    return null;
                }
                else
                {
                    if (usingCamera)
                    {
                        Image<Gray, byte> temp;
                        lock (locker)
                        {
                            temp = Current_image.Copy();
                        }
                        return temp;
                    }
                    //if camera is not available ,we take the image from folder
                    else
                    {

                        if (it < imgs.Count)
                            return new Image<Gray, byte>(imgs[it]);
                        else
                        {
                            it = 0;
                            return null;
                        }
                    }
                }
            }
            catch (Exception  e)
            {

                throw new SpinPlatform.Errors.SpinException("CGenieCamera: GetImage: Error acquiring from the Camera :" + e.Message);
            }

        }
        public void NextFileImage()
        {
            if (!usingCamera)
            {
                it++;
                if (it >= imgs.Count)
                {
                    it = 0;
                    MessageBox.Show("all images in folder have been processed");
                } 
            }

        }        
        static bool InitAcqParams(MyAcquisitionParams acqParams, int server = 1, int camera = 0)
        {

            // Get total number of boards in the system
            string[] configFileNames = new string[MAX_CONFIG_FILES];
            int serverCount = SapManager.GetServerCount();
            //string configFileIndexToPrint;

            if (serverCount == 0)
            {
                throw new Exception("No camera server found. Check camera installation");
            }
            // Scan the boards to find those that support acquisition
            bool serverFound = false;
            bool cameraFound = false;

            if (SapManager.GetResourceCount(server, SapManager.ResourceType.Acq) != 0)
            {

                serverFound = true;
            }
            if (SapManager.GetResourceCount(server, SapManager.ResourceType.AcqDevice) != 0)
            {
                cameraFound = true;
            }


            // At least one acquisition server must be available
            if (!serverFound && !cameraFound)
            {
                throw new Exception("No acquisition server found!\n");

            }

            //Selecciono el 1º por defecto. Cambiar
            acqParams.ServerName = SapManager.GetServerName(server);


            // Scan all the acquisition devices on that server and show menu to user
            int deviceCount = SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.Acq);
            int cameraCount = SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.AcqDevice);
            int allDeviceCount = deviceCount + cameraCount;


            if (allDeviceCount == 0)
            {
                throw new Exception("No camera  found. Check camera connection");

            }

            //Selecciono el 1º por defecto. Cambiar

            acqParams.ResourceIndex = camera;


            ////////////////////////////////////////////////////////////

            // List all files in the config directory
            string configPath = Environment.GetEnvironmentVariable("SAPERADIR") + "\\CamFiles\\User\\";
            if (!Directory.Exists(configPath))
            {
                throw new Exception("Directory : " + configPath.ToString() + "  Does not exist");

            }
            string[] ccffiles = Directory.GetFiles(configPath, "*.ccf");
            int configFileCount = ccffiles.Length;
            if (configFileCount == 0)
            {
                throw new Exception("No config file found.\nUse CamExpert to generate a config file");
            }
            else
            {
                if (acqParams.ConfigFileName=="")  acqParams.ConfigFileName = configFileNames[0];
            }
            return true;
        }
        /// <summary>
        /// Start the asynchronous grabbing of images. The images can be accessed using GetImage
        /// </summary>
        /// <returns>the error code of grab start</returns>
        public int GrabAsync()
        {
            try
            {
                if (!m_initialized)
                {
                    return -1;
                }
                else if (IsGrabbing)
                {
                    return -2;
                }
                else
                {
                    if (Transfer.Grab() == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                
                 throw new SpinPlatform.Errors.SpinException("CGenieCamera: GrabAsync: " + e.Message);
            }

        }
        void CallbackNewFrame(object sender, DALSA.SaperaLT.SapClassBasic.SapXferNotifyEventArgs e)
        {
            // Create an array large enough to hold all buffer data
            int size = Buffers.Width * Buffers.Height;
            byte[] dataBuf = new byte[size];
            
            // Pin the array to avoid Garbage collector move it
            GCHandle dataBufHandle = GCHandle.Alloc(dataBuf, GCHandleType.Pinned);
            IntPtr dataBufAddress = dataBufHandle.AddrOfPinnedObject();
            // Read back buffer data

            Buffers.Read(0, size, dataBufAddress);
            using (Image<Gray, byte> Image = new Image<Gray, byte>(Buffers.Width, Buffers.Height, Buffers.Width, dataBufAddress))
            {
                //vigilar que no se acceda desde mas de un sitio a la veza Current_image
                lock (locker)
                {
                    Current_image = Image.Copy();
                }
            }

            // Unpin the array
            dataBufHandle.Free();  
            
        }
        /// <summary>
        /// Grab a finite number of images defined by parameter frames
        /// </summary>
        /// <param name="frames">number of frames to be grabbed</param>
        /// <returns>error code</returns>
        public int Snap(int frames)
        {
            if (!m_initialized)
            {
                return -1;
            }
            else if (IsGrabbing)
            {
                return -2;
            }
            else
            {
                if (Transfer.Snap(frames) == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }
        bool Wait(int timeout)
        {
            if (!m_initialized)
            {
                return false;
            }
            else
            {
                return Transfer.Wait(timeout);
            }

        }
        /// <summary>
        /// Stop the grabbing process
        /// </summary>
        /// <returns>error code</returns>
        public bool Freeze()
        {
            if (!m_initialized)
            {
                return false;
            }
            else
            {
                if (Transfer.Freeze()) return Transfer.Wait(1000);
                else
                    return false;

            }

        }
        /// <summary>
        /// Abort the Grabbing process
        /// </summary>
        /// <returns>error code</returns>
        public bool Abort()
        {
            if (!m_initialized)
            {
                return false;
            }
            else
            {
                return Transfer.Abort();
            }

        }
        /// <summary>
        /// Save the last image in BMP format using the path given
        /// </summary>
        /// <param name="filename">name of the output file</param>
        /// <returns>error code</returns>
        public bool SaveToBMP(string filename)
        {
            if (!m_initialized)
            {
                return false;
            }
            else
            {
                return Buffers.Save(filename, "-format bmp");
            }

        }     
        public SapBuffer GetBuffer()
        {
            if (!m_initialized)
            {
                return null;
            }
            else
            {
                return Buffers;
            }
        }
        /// <summary>
        /// Close and Dispose the Camera Class
        /// </summary>
        public void Close()
        {
            try
            {
                DestroysObjects(Acq, AcqDevice, Buffers, Transfer, View);
            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CGenieCamera: Close: " + e.Message);
            }
        }
        static void DestroysObjects(SapAcquisition acq, SapAcqDevice camera, SapBuffer buf, SapTransfer xfer, SapView view)
        {

            try
            {
                if (xfer != null)
                {
                    xfer.Destroy();
                    xfer.Dispose();
                }

                if (camera != null)
                {
                    camera.Destroy();
                    camera.Dispose();
                }

                if (acq != null)
                {
                    acq.Destroy();
                    acq.Dispose();
                }

                if (buf != null)
                {
                    buf.Destroy();
                    buf.Dispose();
                }

                if (view != null)
                {
                    view.Destroy();
                    view.Dispose();
                }
            }
            catch (Exception e)
            {
                
                 throw new SpinPlatform.Errors.SpinException("CGenieCamera: DestroysObjects : " + e.Message);
            }
        }
    }
     public class MyAcquisitionParams
    {
        public MyAcquisitionParams()
        {
            serverName = "";
            resourceIndex = 0;
            configFileName = "";
        }
        
        public MyAcquisitionParams(string ServerName, int ResourceIndex)
        {
            serverName = ServerName;
            resourceIndex = ResourceIndex;
            configFileName = "";
        }

        public MyAcquisitionParams(string ServerName, int ResourceIndex, string ConfigFileName)
        {
            serverName = ServerName;
            resourceIndex = ResourceIndex;
            configFileName = ConfigFileName;
        }

        public string ConfigFileName
        {
            get { return configFileName; }
            set { configFileName = value; }
        }

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        public int ResourceIndex
        {
            get { return resourceIndex; }
            set { resourceIndex = value; }
        }

        protected string serverName;
        protected int resourceIndex;
        protected string configFileName;
    }
}
