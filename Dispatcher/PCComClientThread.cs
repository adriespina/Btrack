using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Dynamic;
using SpinPlatform.Comunicaciones;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Billetrack
{
    class PCComClientThread : SpinThreadSocket
    {
        BilletrackDispatcher _Padre;
       
        int _counterWidth = 0, _counterFlatness = 0, _counterAck = 0;
        dynamic _Parametros;
        
        public PCComClientThread(SpinDispatcher padre, string name, dynamic parametros)
            : base(padre, name, (object)parametros.PCCom)
        {
            try
            {
                _Padre = (BilletrackDispatcher)padre;
                _Priority = ThreadPriority.BelowNormal;
                _Parametros = parametros;
            }
            catch (Exception e)
            {
                 _Padre.PrepareEvent("Stop");
                //Logging
                 _Parametros.LOGTXTMessage = "Error in PCComClientThread creation: " + e.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
            }

        }
       
        
        public void SendBilletSteelMaking(Billet billet, List<Billetrack.EventBilletrack> eventos)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            try
            {
                dynamic temp = new ExpandoObject();

                temp.COMMessage = new Byte[200];

                ushort idmessage = 12001;
                System.Buffer.BlockCopy(BitConverter.GetBytes(idmessage), 0, (byte[])temp.COMMessage, 0, 2);

                string palanquilla_prevista,palanquilla_real,error,fecha;
                //hubo matching
                if (billet!=null)
                {
                    //palanquilla = billet.Family.Cast + billet.Line.ToString("0") + "0" + billet.NCut.ToString("00") + "00";
                    palanquilla_prevista = billet.BilletProp1;
                    palanquilla_real = billet.BilletProp2;
                  //  MessageBox.Show("Enviando Aceria . Palanquilla : " + palanquilla_prevista + " ; " + palanquilla_real);
                    fecha = billet.Time.Year.ToString("0000").Substring(2, 2) + billet.Time.Month.ToString("00") + billet.Time.Day.ToString("00") + billet.Time.Hour.ToString("00") + billet.Time.Minute.ToString("00") + billet.Time.Second.ToString("00");
                     error = "00";
                    if (eventos.Count > 0)
                    {
                        if (eventos[0] == EventBilletrack.NoDetected) error = "10";
                        if (eventos[0] == EventBilletrack.NoLight) error = "12";
                    }

                    //Logging
                    _Parametros.LOGTXTMessage = "Sending matching to steel making. Cast: " + billet.Family.Cast + " Line : " + billet.Line.ToString("00");
                    _Padre._Log.SetData(ref _Parametros, "Informacion");
                    
                }
                    //no hubo matching
                else
                {
                    //palanquilla = "000000000000";
                    palanquilla_prevista = "000000000000";
                    palanquilla_real = "000000000000";
                    DateTime ahora = DateTime.Now;
                    fecha = ahora.Year.ToString("0000").Substring(2, 2) + ahora.Month.ToString("00") + ahora.Day.ToString("00") + ahora.Hour.ToString("00") + ahora.Minute.ToString("00") + ahora.Second.ToString("00");
                    error = "00";
                    if (eventos.Count > 0)
                    {
                        if (eventos[0] == EventBilletrack.NoDetected) error = "10";
                        if (eventos[0] == EventBilletrack.NoLight) error = "12";

                    }
                    //Logging
                    _Parametros.LOGTXTMessage = "Sending error matching to steel making";
                    _Padre._Log.SetData(ref _Parametros, "Informacion");
                }
                System.Buffer.BlockCopy(encoding.GetBytes(palanquilla_prevista), 0, (byte[])temp.COMMessage, 2, 12);
                System.Buffer.BlockCopy(encoding.GetBytes(palanquilla_real), 0, (byte[])temp.COMMessage, 14, 12);
                System.Buffer.BlockCopy(encoding.GetBytes(fecha), 0, (byte[])temp.COMMessage, 26, 12);
                System.Buffer.BlockCopy(encoding.GetBytes(error), 0, (byte[])temp.COMMessage, 38, 2);
                string reserva = "0000000000000000";
                System.Buffer.BlockCopy(encoding.GetBytes(reserva), 0, (byte[])temp.COMMessage, 40, 16);
                _server.SetData(ref temp, "EnviarMensaje");
            }


            catch (Exception e)
            {
                //Logging
                _Parametros.LOGTXTMessage = "Error enviando mensaje a Aceria" +e.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
                //throw new SpinPlatform.Errors.SpinException("SendBilletSteelMaking: " + e.Message);
            }
        }

        public void SendBilletRodMill(Billet billet, List<Billetrack.EventBilletrack> eventos)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            try
            {
                dynamic temp = new ExpandoObject();

                temp.COMMessage = new Byte[200];

                ushort idmessage = 13001;
                //System.Buffer.BlockCopy(BitConverter.GetBytes(idmessage), 0, (byte[])temp.COMMessage, 0, 2);

                //ñapa para que coincida con la versin de cesar

                byte[] bytes_ushort=BitConverter.GetBytes(idmessage);
                System.Buffer.BlockCopy(bytes_ushort, 1, (byte[])temp.COMMessage, 0, 1);
                System.Buffer.BlockCopy(bytes_ushort, 0, (byte[])temp.COMMessage, 1, 1);

                string palanquilla, error, fecha, relleno1;
                //hubo matching
                if (billet != null)
                {
                    relleno1 = "0000";
                    palanquilla = billet.Family.Cast + billet.Line.ToString("0") +  billet.NCut.ToString("00") ;
                    fecha = billet.Time.Year.ToString("0000").Substring(2, 2) + billet.Time.Month.ToString("00") + billet.Time.Day.ToString("00") + billet.Time.Hour.ToString("00") + billet.Time.Minute.ToString("00") + billet.Time.Second.ToString("00");
                    error = "00";
                    if (eventos.Count > 0)
                    {
                        if (eventos[0] == EventBilletrack.NoDetected) error = "10";
                        if (eventos[0] == EventBilletrack.NoLight) error = "12";
                    }

                    //Logging
                    _Parametros.LOGTXTMessage = "Sending matching to RodMill. Cast: " + billet.Family.Cast + " Line : " + billet.Line.ToString("00");
                    _Padre._Log.SetData(ref _Parametros, "Informacion");

                }
                //no hubo matching
                else
                {
                    relleno1 = "0000";
                    palanquilla = "000000000";
                    DateTime ahora = DateTime.Now;
                    fecha = ahora.Year.ToString("0000").Substring(2, 2) + ahora.Month.ToString("00") + ahora.Day.ToString("00") + ahora.Hour.ToString("00") + ahora.Minute.ToString("00") + ahora.Second.ToString("00");
                    error = "00";
                    if (eventos.Count > 0)
                    {
                        if (eventos[0] == EventBilletrack.NoDetected) error = "10";
                        if (eventos[0] == EventBilletrack.NoLight) error = "12";

                    }
                    //Logging
                    _Parametros.LOGTXTMessage = "Sending error matching to RodMill";
                    _Padre._Log.SetData(ref _Parametros, "Informacion");
                }
                System.Buffer.BlockCopy(encoding.GetBytes(palanquilla), 0, (byte[])temp.COMMessage, 2, 9);
                System.Buffer.BlockCopy(encoding.GetBytes(relleno1), 0, (byte[])temp.COMMessage, 11, 4);
                System.Buffer.BlockCopy(encoding.GetBytes(fecha), 0, (byte[])temp.COMMessage, 15, 12);
                System.Buffer.BlockCopy(encoding.GetBytes(error), 0, (byte[])temp.COMMessage, 27, 2);
                System.Buffer.BlockCopy(encoding.GetBytes(relleno1), 0, (byte[])temp.COMMessage, 29, 4);
                System.Buffer.BlockCopy(encoding.GetBytes(palanquilla), 0, (byte[])temp.COMMessage, 33, 9);
                _server.SetData(ref temp, "EnviarMensaje");
            }


            catch (Exception e)
            {
                //Logging
                _Parametros.LOGTXTMessage = "Error enviando mensaje a Alambron" + e.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
                //throw new SpinPlatform.Errors.SpinException("SendBilletRodMill: " + e.Message);
            }
        }

        public void SendAcknoledge()
        {
            
            try
            {
                dynamic temp = new ExpandoObject();
                temp.COMMessage = new Byte[200];
                ushort idmessage = 13002;
                //System.Buffer.BlockCopy(BitConverter.GetBytes(idmessage), 0, (byte[])temp.COMMessage, 0, 2);   


                //ñapa para que coincida con la versin de cesar
                byte[] bytes_ushort = BitConverter.GetBytes(idmessage);
                System.Buffer.BlockCopy(bytes_ushort, 1, (byte[])temp.COMMessage, 0, 1);
                System.Buffer.BlockCopy(bytes_ushort, 0, (byte[])temp.COMMessage, 1, 1);


                _server.SetData(ref temp, "EnviarMensaje");
            }


            catch (Exception e)
            {

                throw new SpinPlatform.Errors.SpinException("SendAcknoledge: " + e.Message);
            }
        }

      
        public override void FunctionToExecuteByThread()
        {
            try
            {

  string hora;
  System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
                {

                    Byte[] val = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                    int id = BitConverter.ToUInt16(val,0);

                    //RPC
                    
                    
                    switch (id)
                    {
                           
                        case 22001: //Aceria envia la sincronizacion horaria
                            hora = encoding.GetString(val, 2, 12);
                            SetTime(hora);
                           // this._Padre.AddLogDesarrollo("Recibido mensaje del ordenador de proceso de tipo 22001");   
                            break;
                      
                        case 23001:  //Alambron envia la sincronizacion horaria y le contestamos
                            hora = encoding.GetString(val, 2, 12);
                            SetTime(hora);
                           // this._Padre.AddLogDesarrollo("Recibido mensaje del ordenador de proceso de tipo 23001");
                            SendAcknoledge();
                            break;
                        case 23002://Alambron envia la señal de nueva palanquilla
                            
                            string family=encoding.GetString(val, 2, 6);
                            string line=encoding.GetString(val, 8, 1);
                            string  cut=encoding.GetString(val, 9, 2);
                            string  distance=encoding.GetString(val, 11, 4);
                            this._Padre.AddLogInformation("Recibido mensaje de alambron de nueva palanquilla: familia " + family + ",linea " + line + ",cut " + cut + ",distancia " + distance);
                            //MessageBox.Show("recibido palanquilla  aceria : " + family + " ; " + line + " ; " + cut + " ; " + distance + " ; ");
                             ((SharedData<Billet>)_SharedMemory["LastBillet"]).Set(0, new Billet(new Family(family),int.Parse(line),int.Parse(cut),int.Parse(distance),DateTime.Now));
                            _Events["BilletToProcess"].Set();
                            _Padre.PrepareEvent("LastBillet");
                            break;
                           
                        case 22003: //Aceria envia la señal de nueva palanquilla
                            //read the information sent by PC
                            BilletCom ReceivedBillet = new BilletCom(val);
                            this._Padre.AddLogInformation("Recibido mensaje de aceria de tipo 22003: familia " + ReceivedBillet.Billet.Family.Cast + ",linea " + ReceivedBillet.Billet.Line + ",cut " + ReceivedBillet.Billet.NCut + ",distancia " + ReceivedBillet.Billet.Distance );
                          
                            //MessageBox.Show("recibido palanquilla  aceria : " + ReceivedBillet.Billet.Family.Cast + " ; " + ReceivedBillet.Billet.Line + " ; " + ReceivedBillet.Billet.NCut );
                            ((SharedData<Billet>)_SharedMemory["LastBillet"]).Set(0, ReceivedBillet.Billet);
                            _Events["BilletToProcess"].Set();
                            _Padre.PrepareEvent("LastBillet");

                            //Logging
                            _Parametros.LOGTXTMessage = "Received new  Billet. Cast: " + ReceivedBillet.Billet.Family.Cast + " Line : " + ReceivedBillet.Billet.Line.ToString("00");
                            _Padre._Log.SetData(ref _Parametros, "Informacion");

                            break;

                    }


                }

            }
            catch (Exception e)
            {


                MessageBox.Show("Error com  aceria : " + e.Message);
                //Logging
                _Parametros.LOGTXTMessage = "Error in PCComClientThread loop: " + e.Message;
                _Padre._LogError.SetData(ref _Parametros, "Informacion");
            }
        
           
        }
        public override void Closing()
        {
            Trace.WriteLine("ADRI:   saliendo  del HILO COMUNICACION OP");

        }
        public override bool Stop()
        {
            return base.Stop();
        }

        [DllImport("coredll.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("coredll.dll")]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SYSTEMTIME sysTime);


        public  struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

public void SetTime(string hora)
{
    // Call the native GetSystemTime method
    // with the defined structure.
    SYSTEMTIME systime = new SYSTEMTIME();

    systime.wYear =(ushort)(2000 + ushort.Parse(hora.Substring(0,2)));
    systime.wMonth = ushort.Parse(hora.Substring(2,2));
    systime.wDay = ushort.Parse(hora.Substring(4,2));
    systime.wHour = ushort.Parse(hora.Substring(6,2));
    //bug con la hora (a las 17 pone 19)
    systime.wHour = (ushort)(systime.wHour - 2);
   
    systime.wMinute = ushort.Parse(hora.Substring(8,2));
    systime.wSecond = ushort.Parse(hora.Substring(10,2));

    Win32SetSystemTime(ref systime);
  
}

    }
   
 
}

