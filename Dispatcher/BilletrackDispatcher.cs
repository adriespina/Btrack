using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;

using System.Threading;
using System.Dynamic;
using SpinPlatform.Config;
using SpinPlatform.Log;
using SpinPlatform.Comunicaciones;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Billetrack
{
    public class BilletrackDispatcher:SpinDispatcher
    {
        //Objetos auxiliares
       dynamic _Configuracion;
       public  CMetodosAuxiliares _Aux;
       public  BilletrackDataBase _BilletrackDB;
       public SpinLogFile _Log, _LogError;

       Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
       {
            try
           {
               AppDomain domain = (AppDomain)sender;
                if (args.Name.Contains("SQLite") || args.Name.Contains("Oracle"))
               {
                   string[] assemblyDetail = args.Name.Split(',');                

                   string assemblyBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                   Assembly assembly = Assembly.LoadFrom(assemblyBasePath + @"\" + assemblyDetail[0] + ".dll");
                    return assembly;
               }
           }
           catch (Exception ex)
           {
              

           }
           return null;
       }
       public BilletrackDispatcher()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            _Configuracion = new ExpandoObject();
            _Log = new SpinLogFile();
            _LogError = new SpinLogFile();

           
        }
       override public void Init(dynamic parameters)
        {
            SpinConfig con = new SpinConfig();
            _Configuracion.CONFFile = "BilletrackConfig.xml";
            
            con.GetData(ref _Configuracion, "Parametros");

            _Log.Init(_Configuracion.Log);
            _LogError.Init(_Configuracion.LogError);


           //Clases auxiliares

            _Aux = new CMetodosAuxiliares();
            _Aux.m_RefLine1 = new PointsOfLine(_Configuracion.Detection.RefLine1_Point1_ROW, _Configuracion.Detection.RefLine1_Point1_COL, _Configuracion.Detection.RefLine1_Point2_ROW, _Configuracion.Detection.RefLine1_Point2_COL, _Configuracion.Detection.ShowRefLine1);
            _Aux.m_RefLine2 = new PointsOfLine(_Configuracion.Detection.RefLine2_Point1_ROW, _Configuracion.Detection.RefLine2_Point1_COL, _Configuracion.Detection.RefLine2_Point2_ROW, _Configuracion.Detection.RefLine2_Point2_COL, _Configuracion.Detection.ShowRefLine2);
            _Aux.m_billetSearch = new SearchArea(_Configuracion.Detection.BILLET_SEARCH_INITIAL_ROW, _Configuracion.Detection.BILLET_SEARCH_NROWS, _Configuracion.Detection.BILLET_SEARCH_INITIAL_COL, _Configuracion.Detection.BILLET_SEARCH_NCOLS, _Configuracion.Detection.BILLET_SEARCH_MIN, _Configuracion.Detection.BILLET_SEARCH_MAX, _Configuracion.Detection.BILLET_SEARCH_MAXFRAMES_TOSEARCH, _Configuracion.Detection.BILLET_SEARCH_FRAMES_TO_WAIT);
            _Aux.m_SpotlightOnOff = new SearchArea(_Configuracion.Detection.BILLET_SPOTLIGHT_INITIAL_ROW, _Configuracion.Detection.BILLET_SPOTLIGHT_NROWS, _Configuracion.Detection.BILLET_SPOTLIGHT_INITIAL_COL, _Configuracion.Detection.BILLET_SPOTLIGHT_NCOLS, _Configuracion.Detection.BILLET_SPOTLIGHT_MIN, _Configuracion.Detection.BILLET_SPOTLIGHT_MAX, 0, 0);
            _Aux.BILLET_ROTATION_ANGLE = _Configuracion.Detection.BILLET_ROTATION_ANGLE;
            _Aux.THRESHOLD_FOR_CROP = _Configuracion.Detection.BILLET_THRESHOLD_FOR_CROP;
            _Aux.NROTACIONES = _Configuracion.Detection.BILLET_ROTATION_NUMBER;

           
            _BilletrackDB = new BilletrackDataBase(this,_Configuracion.General.INSTALLATION_NAME, _Configuracion.DataBase.CONNECTION_STRING, _Configuracion.DataBase.DATABASE_DEBUG);
           
     
            // Hilos

            _DispatcherThreads.Add("Acquisition", new AcquisitionThread(this, "Acquisition", _Configuracion));
     
            _DispatcherThreads.Add("Communication", new PCComClientThread(this, "Communication", _Configuracion));
     
           _DispatcherThreads.Add("Processing", new ProcessingThread(this, "Processing", _Configuracion));
     
           ConnectMemory("State", new SharedData<State>(1), "Acquisition", "Communication", "Processing");
            

            //Memorias Compartidas


            ConnectMemory("LastBillet", new SharedData<Billet>(1), "Acquisition", "Communication");
            ConnectMemory("LastImage", new SharedData<Images>(10), "Acquisition", "Processing");
            ConnectMemory("CurrentImage", new SharedData<CurrentImage>(1), "Acquisition");
            ConnectMemory("MatchInformation", new SharedData<Match>(1), "Processing");

     
            //Eventos de sincronizacion 

            CreateEvent("BilletDetected", new AutoResetEvent(false), "Acquisition", "Processing");
            CreateEvent("BilletToProcess", new AutoResetEvent(false), "Acquisition", "Communication");
          
            _Configuracion.LOGTXTMessage = "Log Initialized";
            _Log.SetData(ref _Configuracion, "Informacion");
         
            //Inicializo el estado
            ((SharedData<State>)_DispatcherSharedMemory["State"]).Add(new State());
      
            
        }

       public void AddLogInformation(string mensaje)
       {
           _Configuracion.LOGTXTMessage = mensaje;
           _Log.SetData(ref _Configuracion, "Informacion");
         
       }

       public void AddLogError(string mensaje)
       {
           _Configuracion.LOGTXTMessage = mensaje;
           _LogError.SetData(ref _Configuracion, "Informacion");
         

       }
       public void AddLogDesarrollo(string mensaje)
       {
           _Configuracion.LOGTXTMessage = mensaje;
           _Log.SetData(ref _Configuracion, "Desarrollo");


       }

        public override void GetData(ref dynamic Data, params string[] parameters)
        {
            Data.TRIReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "CurrentImage":
                            Data.CurrentImage = (CurrentImage)((SharedData<CurrentImage>)_DispatcherSharedMemory["CurrentImage"]).Get(0);
                            break;
                        case "LastBillet":
                            Data.LastBillet = (Billet)((SharedData<Billet>)_DispatcherSharedMemory["LastBillet"]).Get(0);
                            break;
                        case "State":
                            Data.State = (State)((SharedData<State>)_DispatcherSharedMemory["State"]).Get(0);
                            ((PCComClientThread)_DispatcherThreads["Communication"])._server.GetData(ref Data, "EstadoSocket");
                            if ((bool)Data.COMSocketDatosConnected )
                            {
                                ((State)(Data.State)).Socket = spinConnectionStatus.connected;
                            }
                            else
                            {
                                ((State)(Data.State)).Socket = spinConnectionStatus.disconnected;
                            }
                           
                            break;
                        case "MatchInfo":
                            Data.MatchedInfo = (Match)((SharedData<Match>)_DispatcherSharedMemory["MatchInformation"]).Get(0);
                            break;
                        
                    }
                }
                Data.TRIErrors = "";
            }
            catch (Exception ex)
            {

                Data.TRIErrors = ex.Message;
                MessageBox.Show("error dispatcher : "+ex.Message);
                //Ademas se lanzaria la excepcion oportuna
            }
        }
        public void PrepareEvent(string thread)
        {
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                dynamic temp = new ExpandoObject();

                switch (thread)
                {
                    case "Acquisition":
                        GetData(ref temp, "CurrentImage");
                        break;
                    case "Processing":
                        GetData(ref temp, "MatchInfo");
                        break;
                    case "State":
                        GetData(ref temp, "State");
                        break;
                    case "LastBillet":
                        GetData(ref temp, "LastBillet");
                        break;

                   
                }

                DataEventArgs args = new DataEventArgs(temp);

                SetEvent(args);
            }

        }
        public override void SetData(ref dynamic Data, params string[] parameters)
        {
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        

                        default:
                            Data.MEPErrors = "Wrong Query";
                            break;
                    }
                }
                Data.MEPErrors = "";
            }
            catch (Exception ex)
            {

                Data.MEPErrors = ex.Message;
                //Ademas se lanzaria la excepcion oportuna
            }

        }
    }
}
