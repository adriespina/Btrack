using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb; 

namespace Billetrack
{
   
    class CClassification
    {
        System.Data.OleDb.OleDbConnection MyConnection;
        System.Data.DataSet DtSet;
        System.Data.OleDb.OleDbCommand MyCommand;
        string sql;

        public CClassification(string excel)
        {
           
            //MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='"+c:\\csharp.net-informations.xls+"';Extended Properties=Excel 8.0;");
            MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='"+excel+"';Extended Properties=Excel 8.0;");
            MyCommand = new System.Data.OleDb.OleDbCommand();
            sql = null;
            MyConnection.Open();

        
        }
        public void InsertMatch(resultMatching[] results)
        {

            foreach (resultMatching rst in results)
            {
                   sql = "Insert into [Sheet1$] (common_KeyPoints,inside_KeyPoints,quality,total_KeyPoints,total_other_keyPoints) values("+rst.common_KeyPoints.ToString()+","+rst.inside_KeyPoints_surf.ToString()+","+rst.quality.ToString()+","+rst.total_KeyPoints.ToString()+","+rst.total_other_keyPoints.ToString()+")";
            }
          
            MyCommand.CommandText = sql;
            MyCommand.ExecuteNonQuery();
        
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
                   if(MyConnection.State==System.Data.ConnectionState.Open) MyConnection.Close();

                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ...

            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~CClassification()
        {
            this.Dispose(false);
        }

        #endregion

    }
}
