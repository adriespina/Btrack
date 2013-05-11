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


namespace Billetrack
{
    class ComThread : SpinThreadWhile
    {
        BilletrackDispatcher _Padre;
        dynamic _Parametros;
       



        public ComThread(BilletrackDispatcher padre, string name, dynamic parametros)
            : base(name)
        {
            _Padre = (BilletrackDispatcher)padre;
            _Parametros = parametros;
            
        }
        public override void FunctionToExecuteByThread()
        {
            //solo para depuracion
            Random ram= new Random();
            ((SharedData<Billet>)_SharedMemory["LastBillet"]).Set(0,new Billet(new Family("111111"),ram.Next(0,9),ram.Next(0,30)));
            _Events["BilletToProcess"].Set();
           
        }
        public override void Initializate()
        {
            _MillisecondsToSleep = 4000;
            

        }
        public override void Closing()
        {
           
        }

        
       


    }
}
