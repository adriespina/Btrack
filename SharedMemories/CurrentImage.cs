using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace Billetrack
{
    [Serializable]
    public class CurrentImage : IDisposable
    {
       public  Image<Rgb, byte> Image;    

       public CurrentImage(Image<Rgb, byte> img)
        {
           
            Image = img;
         
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
                    this.Image.Dispose();

                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ...

            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~CurrentImage()
        {
            this.Dispose(false);
        }

        #endregion

    }
}
