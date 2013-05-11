using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Diagnostics;
using SpinPlatform.Errors;
using Microsoft.Office.Interop.Excel;


namespace Billetrack
{
   
   public class CClassification
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Excel.Range last;
        object misValue = System.Reflection.Missing.Value;
        int lastUsedRow  ;
        string name;

        public CClassification(string excel)
        {

                name = excel;
        
    }
    
        public void InsertMatch(resultMatching[] results,int index)
        {
            try
            {
                xlApp = new Excel.Application();
                //xlWorkBook = xlApp.Workbooks.Open(name, 0, false, 5, misValue, misValue, true, misValue, "\t", false, false, 0,false, 1, 0);
                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

               Workbooks  xlWorkBooks = xlApp.Workbooks;
               xlWorkBook = xlWorkBooks.Open(name, 0, false, 5, misValue, misValue, true, misValue, "\t", false, false, 0, false, 1, 0);
               Sheets xlWorkSheets = xlWorkBook.Worksheets;
               xlWorkSheet = xlWorkSheets.get_Item(1);



                last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                Excel.Range range = xlWorkSheet.get_Range("A1", last);       
                lastUsedRow = last.Row;
                int initialrow = lastUsedRow;

                foreach (resultMatching rst in results)
                {
                    lastUsedRow++;
                    xlWorkSheet.Cells[lastUsedRow, 2] = rst.common_KeyPoints.ToString();
                    xlWorkSheet.Cells[lastUsedRow, 3] = rst.inside_KeyPoints.ToString();
                    xlWorkSheet.Cells[lastUsedRow, 4] = rst.quality.ToString();
                    xlWorkSheet.Cells[lastUsedRow, 5] = rst.total_KeyPoints.ToString();
                    xlWorkSheet.Cells[lastUsedRow, 6] = rst.total_other_keyPoints.ToString();
                    xlWorkSheet.Cells[lastUsedRow, 7] = 0;

                }
               if(index>0) xlWorkSheet.Cells[initialrow+index+1, 7] = 1;
                xlWorkBook.Save();               
                xlWorkBook.Close(true, misValue, misValue);
                xlWorkBooks.Close();
                xlApp.Quit();
                releaseObject(xlWorkSheets);
                releaseObject(xlWorkBooks);
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                //esto cerrara todo proceso que use excel
                foreach (Process proceso in Process.GetProcesses())
                {

                    if (proceso.ProcessName.Contains("EXCEL.EXE"))
                    {
                        proceso.Kill();
                    }
                }
            }
            catch (Exception e)
            {

                throw new SpinException("Error saving matching statistics to excel file : " + e.Message);
            }
        }

            #region dispose

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        } 

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

                    foreach (Process proceso in Process.GetProcesses())
                    {

                        if (proceso.ProcessName.Contains("EXCEL"))
                        {
                            proceso.Kill();
                        }
                    }

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
