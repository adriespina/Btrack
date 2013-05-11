using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;





namespace Billetrack

{
    /// <summary>
    /// Class to draw lines in an image.
    /// </summary>
    public class PointsOfLine
    {
        public Point initPoint;
        public Point endPoint;
        public bool bDraw;
        public PointsOfLine(int ini_row, int ini_col, int fin_row, int fin_col, bool draw)
        {
            initPoint = new Point(ini_col, ini_row);
            endPoint = new Point(fin_col, fin_row);
            bDraw = draw;
        }
    }

    public class SearchArea
    {
        public int initialRow;
        public int nRows;
        public int initialCol;
        public int nCols;
        public int Min;
        public int Max;
        public int MaxFrames;
        public double valueCalculated;
        private int FramesToWait;

public bool IsPresent
{
  get {
      if (valueCalculated>0)
      {
          if (valueCalculated > Min && valueCalculated < Max) return true;

          else return false;
      }
      else     return false; }
  
}

       
        public SearchArea(int initialRow_, int nRows_, int initialCol_, int nCols_, int Min_, int Max_, int MaxFrames_, int FramesToWait_)
        {
            initialRow = initialRow_;
            nRows = nRows_;
            initialCol = initialCol_;
            nCols = nCols_;
            Min = Min_;
            Max = Max_;
            MaxFrames = MaxFrames_;
            valueCalculated = 0;
            FramesToWait = FramesToWait_;

        }
    }

    public class imgStats
    {

        public double Media;
        public double Std;
        public int Suma;
        public int NonZero;
        public double MediaNonZero;
        public int MaxHist;
        public int AnchoHist;
        public int MaxHistNonZero;
        public int AnchoHistNonZero;
        public int Width;
        public int Height;
        public int nPixels;
        public double percNonZero;
        public double percHeightHist;
        public double percHeightHistNonZero;
        public double FocusIndicator;
        public double Param_Focus_normalizefreq;
        public int ExposureTime;

        public imgStats()
        { }
    }
    public class Factory
    {

        public int ID;
        public string Name;
        public  string HostFTP;
        public string UserFTP;
        public string PasswordFTP;
        public string PathImages;

        public string SearchField;
        public string CameraModel;
        public string LightingModel;
        public string CameraIP;
        public int FactoryBackup;

      

        public Factory(string name, string pathImages, string hostFTP, string userFTP, string passwordFTP)
        {
            Name = name;
            HostFTP = hostFTP;
            UserFTP = userFTP;
            PasswordFTP = passwordFTP;
            PathImages = pathImages;

        }
        public Factory(int id, string name, string hostFTP, string userFTP, string PassFTP, string PathImages,
                        string SearchField, string CameraModel, string Lighting, string CameraIP, int FactoryBackup)
        {
            this.ID = id;
            this.Name = name;
            this.HostFTP = hostFTP;
            this.UserFTP = userFTP;
            this.PasswordFTP = PassFTP;
            this.PathImages = PathImages;
            this.SearchField = SearchField;
            this.CameraModel = CameraModel;
            this.LightingModel = Lighting;
            this.CameraModel = CameraIP;
            this.FactoryBackup = FactoryBackup;
        }
        public Factory()
        {
            Name = "";
            HostFTP = "";
            UserFTP = "";
            PasswordFTP = "";
            PathImages = "";

        }
        
    }
    public class Family : IDisposable
    {
        public int ID;
        public string Cast;

        public DateTime CreationTime;
        public string Prop1;
        public string Prop2;
        public string DescriptionProp1;
        public string DescriptionProp2;
   public Family(string colada)
        {
            Cast = colada;
            CreationTime = DateTime.Now;
        
        }
        public string GetSQLInsert(string tableName)
        {
            bool addedone = false;
            string values = "";
            string result = "INSERT INTO " + tableName + "(";
            if (ID > 0)
            {
                result += "IDFAMILY";
                addedone = true;
                values += this.ID.ToString();
       
            }
            #region Descriptor
            if (Cast.Length > 0)
            {
                if (addedone)
                {
                    result += ",";
                    values += ",";
                }
                else addedone = true;
                result += "CASTNUMBER";
                values += "'"+Cast+"'" ;
            }
            #endregion
            #region Line
            if (CreationTime!=null)
            {
                if (addedone)
                {
                    result += ",";
                    values += ",";
                }
                else addedone = true;
                result += "CASTDATE";
                values += "to_date('" + CreationTime + "','dd/mm/yyyy hh24:mi:ss')";
            }
            #endregion
           
            result += ") VALUES (" + values + ")";


            return result;



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
        ~Family()
        {
            this.Dispose(false);
        }

        #endregion


    }
    public class Billet : IDisposable
    {
        public Family Family;
        public DateTime Time;
        public int ID;
        public int Distance;
        public string Descriptor;
        public int Line;
        public int Position;
        public int NCut;
        public string BilletProp1;
        public double BilletProp3; //RPC double?? en BD es Int32.
        public string BilletProp2;
        public double BilletProp4; //RPC double?? en BD es Int32.

        public Billet(Family family, string descriptor, int line, int number, int distancia,int position = 0)
        {
            ID = -1;
            Family = family;
            Descriptor = descriptor;
            Line = line;
            NCut = number;
            Position = position;
            Distance = distancia;
     
        
        }
        public Billet(Family family, int line, int number)
        {
            Family = family;
            Line = line;
            NCut = number; 
        }
        public Billet(Family family, int line, int number, int distancia, DateTime hora)
        {
            Family = family;
            Line = line;
            NCut = number;
            Distance = distancia;
            Time = hora;
        }
 

        public Billet(Family family)
        {
            Family = family;
          
        }
        public string GetSQLInsert(string tableName)
        {
            bool addedone = false;
            string values="";
        string result="INSERT INTO "+tableName+"(";
        if (ID > 0)
        {
            result += "IDBILLET";
            addedone = true;
            values+=this.ID.ToString();
        }
        #region Descriptor
        if (Descriptor != null)
        {
            if (Descriptor.Length > 0)
            {
                if (addedone)
                {
                    result += ",";
                    values += ",";
                }
                else addedone = true;
                result += "BILLETDESCRIPTOR";
                values += "'" + Descriptor + "'";
            }
        }
        #endregion
        #region Line
        if (Line >= 0)
        {
            if (addedone)
            {
                result += ",";
                values += ",";
            }
            else addedone = true;
            result += "LINE";
            values += this.Line;
        }
        #endregion
        #region Family
        if (Family!=null)
        {
            if (addedone)
            {
                result += ",";
                values += ",";
            }
            else addedone = true;
            result += "IDFAMILY";
            values += this.Family.ID;
        }
        #endregion
        #region Position
        if (Position >= 0)
        {
            if (addedone)
            {
                result += ",";
                values += ",";
            }
            else addedone = true;
            result += "Position";
            values += this.Position;
        }
        #endregion
        #region NCut
        if (NCut >= 0)
        {
            if (addedone)
            {
                result += ",";
                values += ",";
            }
            else addedone = true;
            result += "NCut";
            values += this.NCut;
        }
        #endregion
      
        //public string BilletProp1;
        //public double BilletProp3; //RPC double?? en BD es Int32.
        //public string BilletProp2;
        //public double BilletProp4;

        result += ") VALUES (" + values + ")";
        return result;



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
        ~Billet()
        {
            this.Dispose(false);
        }

        #endregion

        
    }

    public class BilletrackSystem
    {
       public  List <Factory> Origin;
       public List<Factory> Destiny;
       public Factory Backup;
       public bool BilletisKnowOrigin;
       public bool BilletisKnownDestiny;
       public BilletrackSystem()
        {
            Destiny = new List<Factory>();
            Origin = new List<Factory>();
                    
    }
    }
    public class ImageBilletrack
    {
        UInt32 IDImage;
        Factory FactoryPlace;
        string PathImage; //Año/mes/familia/imagen.jpg
        imgStats ImageStats;

    }   
    public enum EventBilletrack { Unknow, NoDetected, NoLight,BadCropped, Snow, Oxyde }
}
