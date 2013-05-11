using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Comunicaciones;

namespace Billetrack
{
    [Serializable]
        public class BilletCom:IDisposable
        {
        //mienbros privados
       
         int _ID;
       string  _ScheduledBillet;
        string _RealBillet;
        DateTime _CaptureDateTime;
        string _CaptureTime;
        string _CaptureResult;
        string _ProcessingResult;
        string _Warning;
        string _OCRBillet;
        Billet _Billet;
        
 

          //descriptores de acceso
         

       public string ScheduledBillet
{
  get { return _ScheduledBillet; }
  set { _ScheduledBillet = value; }
       }

           public string RealBillet
{
  get { return _RealBillet; }
  set { _RealBillet = value; }
}

             public Billet Billet
{
  get { return _Billet; }
  set { _Billet = value; }
}

        //metodos
         
          public BilletCom( Byte[] msg)
          {
               System.Text.UTF8Encoding   encoding = new System.Text.UTF8Encoding();
              _ScheduledBillet = encoding.GetString(msg, 2, 12);
            _RealBillet = encoding.GetString(msg,14, 12);
              _Billet= new Billet(new Family(encoding.GetString(msg,2,6)),Int16.Parse(encoding.GetString(msg,8,1)),Int16.Parse(encoding.GetString(msg,10,2)));
              _Billet.Time = DateTime.Now;
              
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
          ~BilletCom()
          {
              this.Dispose(false);
          }

          #endregion
 
 
    }
}
