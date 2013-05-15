using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Comunicaciones;

namespace Billetrack
{
    [Serializable]
    public class State : IDisposable
    {
        //mienbros privados

     spinConnectionStatus _Socket;      

        bool _RemoteDatabase;      
        bool _Working_OK;      
        bool _Camera;
        bool _Light;       
        bool _FTP;
        bool _Level2ClientAlive;
        string _Disk;
       


        //descriptores de acceso

        public string Disk { get { return _Disk; } set { _Disk = value; } }
        public spinConnectionStatus Socket { get { return _Socket; } set { _Socket = value; } }   
        public bool RemoteDatabase { get { return _RemoteDatabase; } set { _RemoteDatabase = value; } }
        public bool Light {  get { return _Light; } set { _Light = value; } }
  
        public bool Working_OK
        {
            get
            {
                if (Socket == spinConnectionStatus.connected && _Light && RemoteDatabase  && Camera && FTP )
                {
                    _Working_OK = true;
                }
                else _Working_OK = false;
                return _Working_OK;

            }

        }

        public bool Camera { get { return _Camera; } set { _Camera = value; } }
        public bool FTP { get { return _FTP; } set { _FTP = value; } }
        public bool Level2ClientAlive { get { return _Level2ClientAlive; } set { _Level2ClientAlive = value; } }

        //metodos

        public State()
        {

            
            _Socket = spinConnectionStatus.disconnected;     
            _RemoteDatabase = true;           
            _Working_OK = true;           
            _Camera = true;
            _FTP = true;          
            _Level2ClientAlive = true;
            _Light = true;

        }


        #region dispose

        // Indica si ya se llamo al método Dispose. (default = false)
        private Boolean disposed;

        /// <summary>
        /// Implementación de IDisposable. No se sobreescribe.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            // GC.SupressFinalize quita de la cola de finalización al objeto.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Limpia los recursos manejados y no manejados.
        /// </summary>
        /// <param name="disposing">
        /// Si es true, el método es llamado directamente o indirectamente
        /// desde el código del usuario.
        /// Si es false, el método es llamado por el finalizador
        /// y sólo los recursos no manejados son finalizados.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Preguntamos si Dispose ya fue llamado.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Llamamos al Dispose de todos los RECURSOS MANEJADOS.


                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ...

            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~State()
        {
            this.Dispose(false);
        }

        #endregion


    }
}
