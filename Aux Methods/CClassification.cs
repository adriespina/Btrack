﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Diagnostics;
using SpinPlatform.Errors;
using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Billetrack
{
   
   public class CClassification
    {

       public int MAX_ROW_EXCEL= 1000000;
       public int ROW_TO_DELETE_EXCEL = 10000;

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
    
        public void InsertSetMatches(resultMatching[] results,int index)
        {
            try
            {
                  lastUsedRow = OpenXLS();
                int initialrow = lastUsedRow;
                int contador = 0;

                foreach (resultMatching rst in results)
                {
                    contador++;
                    if (rst.common_KeyPoints!=0)
                    {
                        lastUsedRow++;
                        if (contador==index+1) 
                            xlWorkSheet.Cells[lastUsedRow, 1] ="MATCH";
                        else xlWorkSheet.Cells[lastUsedRow, 1] = "0";
                        xlWorkSheet.Cells[lastUsedRow, 2] = rst.quality.ToString("F0");
                        xlWorkSheet.Cells[lastUsedRow, 3] = rst.npoints_included_homography.ToString();  
                        xlWorkSheet.Cells[lastUsedRow, 4] = rst.common_KeyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 5] = rst.inside_KeyPoints.ToString();                       
                        xlWorkSheet.Cells[lastUsedRow, 6] = rst.total_KeyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 7] = rst.total_other_keyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 8] = rst.bHomographyOK.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 9] = rst.bdst_corners_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 10] = rst.npoints_Homography.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 11] = rst.npoints_Homography_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 12] = rst.bdst_corners_local_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 13] = rst.percentage_points_included_homography.ToString("F0");                  
                    }
                }
              
               CloseSaveXLSX();
            }
            catch (Exception e)
            {

                throw new SpinException("Error saving matching statistics to excel file : " + e.Message);
            }
        }

        public void InsertMatch(resultMatching rst)
        {
            try
            {
                lastUsedRow = OpenXLS();

                if (rst.common_KeyPoints != 0)
                    {
                        lastUsedRow++;
                        
                        xlWorkSheet.Cells[lastUsedRow, 1] = "MATCH";                      
                        xlWorkSheet.Cells[lastUsedRow, 2] = rst.quality.ToString("F0");
                        xlWorkSheet.Cells[lastUsedRow, 3] = rst.npoints_included_homography.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 4] = rst.common_KeyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 5] = rst.inside_KeyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 6] = rst.total_KeyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 7] = rst.total_other_keyPoints.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 8] = rst.bHomographyOK.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 9] = rst.bdst_corners_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 10] = rst.npoints_Homography.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 11] = rst.npoints_Homography_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 12] = rst.bdst_corners_local_inside.ToString();
                        xlWorkSheet.Cells[lastUsedRow, 13] = rst.percentage_points_included_homography.ToString("F0"); 
                    }
           

                CloseSaveXLSX();
            }
            catch (Exception e)
            {

                throw new SpinException("Error saving matching statistics to excel file : " + e.Message);
            }
        }

        private void CloseSaveXLSX()
        {
            xlWorkBook.Save();
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //esto cerrara todo proceso que use excel
            //foreach (Process proceso in Process.GetProcesses())
            //{

            //    if (proceso.ProcessName.Contains("EXCEL.EXE"))
            //    {
            //        proceso.Kill();
            //    }
            //}
            Process[] pProcess;
            pProcess = System.Diagnostics.Process.GetProcessesByName("Excel");
            pProcess[0].Kill();
        }

        private int OpenXLS()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(name, 0, false, 5, misValue, misValue, true, misValue, "\t", false, false, 0, false, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = xlWorkSheet.get_Range("A1", last);
            lastUsedRow = last.Row;

            if (lastUsedRow>MAX_ROW_EXCEL)
            {
                for (int i = 2; i < ROW_TO_DELETE_EXCEL+2; i++)
                {
                    ((Range)xlWorkSheet.Rows[i]).Delete(XlDeleteShiftDirection.xlShiftUp);
                }
                xlWorkBook.Save();
                last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                Excel.Range range2 = xlWorkSheet.get_Range("A1", last);
                lastUsedRow = last.Row;
            }
            return lastUsedRow;
        }

       public void DeleteRowsContaining(string value, string column)
        {
      
          
            lastUsedRow = OpenXLS();

            Range rangocolumna = xlWorkSheet.get_Range(column + "1", column+lastUsedRow.ToString());
            Range rangoencontrado = rangocolumna;
            while (rangoencontrado!=null)
            {
                 rangoencontrado = rangocolumna.Find(value, Missing.Value, Missing.Value, Excel.XlLookAt.xlWhole, Missing.Value, Excel.XlSearchDirection.xlNext, Missing.Value, Missing.Value, Missing.Value);
                string direccion = rangoencontrado.get_Address();
                string col = direccion.Substring(direccion.LastIndexOf("$") + 1);
                int columna_borrar = int.Parse(col);
                
            }
            CloseSaveXLSX();

         
        
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
