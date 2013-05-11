using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using SpinPlatform.IO.DataBase;
using SpinPlatform.Errors;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Billetrack
{
    /// <summary>
    /// Class to manage the Billetrack Database
    /// </summary>
   public  class BilletrackDataBase
    {

       private SpinOracleDb _billetrackDB;
        Factory LocalFactory;
        BilletrackSystem LocalBilletrack;
       private bool _modoDepuracion=false;

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
               MessageBox.Show("buscando en dll.error: "+ex.Message);
               
            
           }
           return null;
       }
    
        public Factory Factory
        {
            get { return LocalFactory; }

        }
        public BilletrackSystem Billetrack
        {
            get { return LocalBilletrack; }

        }
        
       public bool ModoDepuracion
       {
        get {return _modoDepuracion;}
        set {_modoDepuracion=ModoDepuracion;}
       }

       public BilletrackDataBase(string factoryname, string DBConnectionString, bool _modoDepuracion)
       {
           try
           {
               AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            
               this._modoDepuracion = _modoDepuracion;
               if (!_modoDepuracion)
               {
                   this._billetrackDB = new SpinOracleDb();
                       this._billetrackDB.SetConecction(DBConnectionString);                 
                       this._billetrackDB.Start();
                       if (this._billetrackDB.Connected)
                       {
                           LocalFactory = GetFactoryFromDB(factoryname);
                          LocalBilletrack = GetContextFromDB(factoryname);
                       }
                       else throw new SpinException("Error conectando con la BD");
                }
               else
               {
                   //de aqui hasta abajo Solo para depuracion
                   LocalFactory = new Factory(factoryname, @"C:\Users\Cesar\Desktop\Billetrack Adrian\Imagenes Billetrack\", "127.0.0.1", "Prueba", "Prueba");
                   LocalBilletrack = new BilletrackSystem();

                   //para depuracion añado como destino y origen a si mismo

                   LocalBilletrack.Destiny.Add(LocalFactory);
                   LocalBilletrack.Origin.Add(LocalFactory);
               }
           }
           catch (Exception E)
           {
               throw new SpinException("error connecting BilletrackDataBase : " + E.Message);
                                                   
           }
        }

     
        #region DONE
        BilletrackSystem GetContextFromDB(String name)
        {
            BilletrackSystem ret = new BilletrackSystem();

            // OBTENEMOS EL FACTORY PLACE DE MI MISMO:

            this.LocalFactory = this.GetFactoryFromDB(name);

            if (this.LocalFactory != null)
            {
                // OBTENEMOS LOS SUCESORES:
                string sqlQuery = "Select * from FACTORYPLACE where FACTORYPLACE.IDFACTORYPLACE IN(SELECT BILLETRACKS.FACTORYIDDESTINY FROM BILLETRACKS,FACTORYPLACE WHERE FACTORYPLACE.IDFACTORYPLACE=BILLETRACKS.FACTORYIDORIGIN AND FACTORYPLACE.IDFACTORYPLACE=" + this.LocalFactory.ID + ")";
                OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
                ret.Destiny = null;
                int currdest = 0;
                if (dr.HasRows)
                {
                    ret.Backup = null;
                    ret.Destiny = new List<Factory>();
                    while (dr.Read())
                    {
                        ret.Destiny.Add(new Factory(
                                        this.GetFieldInt32(dr, "IDFactoryPlace"),
                                        this.GetFieldString(dr, "FactoryName"),
                                        this.GetFieldString(dr, "HostFTP"),
                                        this.GetFieldString(dr, "UserFTP"),
                                        this.GetFieldString(dr, "PassWordFTP"),
                                        this.GetFieldString(dr, "PathImages"),
                                        this.GetFieldString(dr, "SearchField"),
                                        this.GetFieldString(dr, "CameraModel"),
                                        this.GetFieldString(dr, "CameraIP"),
                                        this.GetFieldString(dr, "LightingModel"),
                                        this.GetFieldInt320(dr, "FactoryBackup")
                            ));
                        currdest++;
                    }
                }
                else
                {
                    // No hay sucesores, tenemos que ir al de Backup
                    if (this.LocalFactory.FactoryBackup != -1)
                    {
                        ret.Backup = this.GetFactoryFromDB(this.LocalFactory.FactoryBackup);
                    }

                }

                // OBTENEMOS LOS PREDECESORES:
                sqlQuery = "Select * from FACTORYPLACE where FACTORYPLACE.IDFACTORYPLACE IN(SELECT BILLETRACKS.FACTORYIDORIGIN FROM BILLETRACKS,FACTORYPLACE WHERE FACTORYPLACE.IDFACTORYPLACE=BILLETRACKS.FACTORYIDDESTINY AND FACTORYPLACE.FACTORYNAME='" + name + "')";
                dr = this._billetrackDB.SelectCall(sqlQuery);
                ret.Origin = null;
                currdest = 0;
                if (dr.HasRows)
                {
                    ret.Origin = new List<Factory>();
                    while (dr.Read())
                    {
                        ret.Origin.Add(new Factory(
                                        this.GetFieldInt320(dr, "IDFactoryPlace"),
                                        this.GetFieldString(dr, "FactoryName"),
                                        this.GetFieldString(dr, "HostFTP"),
                                        this.GetFieldString(dr, "UserFTP"),
                                        this.GetFieldString(dr, "PassWordFTP"),
                                        this.GetFieldString(dr, "PathImages"),
                                        this.GetFieldString(dr, "SearchField"),
                                        this.GetFieldString(dr, "CameraModel"),
                                        this.GetFieldString(dr, "CameraIP"),
                                        this.GetFieldString(dr, "LightingModel"),
                                        this.GetFieldInt320(dr, "FactoryBackup")
                            ));

                    }
                }
            }
            return ret;
        }
        Factory GetFactoryFromDB(String name)
        {
            Factory ret=new Factory();

            string sqlQuery = "Select * from FACTORYPLACE where FACTORYNAME='" + name + "'";
            OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
            if (dr == null) return null;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ret = new Factory(
                    this.GetFieldInt320(dr, "IDFactoryPlace"),
                    this.GetFieldString(dr, "FactoryName"),
                    this.GetFieldString(dr, "HostFTP"),
                    this.GetFieldString(dr, "UserFTP"),
                    this.GetFieldString(dr, "PassWordFTP"),
                    this.GetFieldString(dr, "PathImages"),
                    this.GetFieldString(dr, "SearchField"),
                    this.GetFieldString(dr, "CameraModel"),
                    this.GetFieldString(dr, "CameraIP"),
                    this.GetFieldString(dr, "LightingModel"),
                    this.GetFieldInt320(dr, "FactoryBackup")
                    );
                }
                return ret;
            }
            else return null;
         }
        Factory GetFactoryFromDB(int ID)
        {
            Factory ret = new Factory();

            string sqlQuery = "Select * from FACTORYPLACE where IDFACTORYPLACE=" + ID ;

            OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ret = new Factory(
                    this.GetFieldInt320(dr, "IDFactoryPlace"),
                    this.GetFieldString(dr, "FactoryName"),
                    this.GetFieldString(dr, "HostFTP"),
                    this.GetFieldString(dr, "UserFTP"),
                    this.GetFieldString(dr, "PassWordFTP"),
                    this.GetFieldString(dr, "PathImages"),
                    this.GetFieldString(dr, "SearchField"),
                    this.GetFieldString(dr, "CameraModel"),
                    this.GetFieldString(dr, "CameraIP"),
                    this.GetFieldString(dr, "LightingModel"),
                    this.GetFieldInt320(dr, "FactoryBackup")
                    );
                }
                return ret;
            }
            else return null;
            
        }
        public Dictionary<int, String> GetCandidates(string SearchValue)
        {
            //  return new Dictionary<int, String>();

            if (!_modoDepuracion)
            {
                if (this.LocalFactory != null)
                {

                    Dictionary<int, String> candidates = new Dictionary<int, string>();

                    string sqlQuery = "Select * from CANDIDATES where CANDIDATES.CASTNUMBER='"+SearchValue+"' AND CANDIDATES.IDFACTORY IN(SELECT BILLETRACKS.FACTORYIDORIGIN FROM BILLETRACKS WHERE BILLETRACKS.FACTORYIDDESTINY=" + this.LocalFactory.ID + ")";
                    OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);

                    if (dr.HasRows)
                    {
                        int curr_candidate_id;
                        string curr_candidate_path;
                        while (dr.Read())
                        {

                            curr_candidate_id = this.GetFieldInt320(dr, "IDIMAGE");
                            curr_candidate_path = this.LocalFactory.PathImages+this.GetFieldString(dr, "IMAGEPATH");
                            candidates.Add(curr_candidate_id, curr_candidate_path);

                        } //while dr.read();
                        return candidates;
                    } // if (dr.hasrows)
                    else return null;
                } // if (localfactory!=null)
                else return null;
            }
            else
            {

                //Solo para depuracion
                int i = 0;
                string pathacer = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_acer";
                Dictionary<int, String> candidates = new Dictionary<int, string>();
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pathacer);
                System.IO.FileInfo[] files = null;
                files = di.GetFiles("*.jpg");
                foreach (System.IO.FileInfo fi in files)
                {

                    if (!fi.FullName.Contains("ORIGINAL")) candidates.Add(i, fi.FullName);
                    i++;

                }
                return candidates;
            }
        }
        public Dictionary<int, String> GetCandidates(params string[] parameters )
        {
            if (parameters.Length == 0)
            {
                throw new SpinException("Search Parameters Empty in GetCandidates Method ");
            }
            //  return new Dictionary<int, String>();
            string whereclause = "";
            Boolean firstcondition = true;
            try
            {
                foreach (string parameter in parameters)
                {
                    if (firstcondition)
                    {
                        whereclause = "WHERE (CANDIDATES.CASTNUMBER='" + parameter + "'";
                        firstcondition = false;
                    }
                    else
                    {
                        whereclause = " OR CANDIDATES.CASTNUMBER='" + parameter + "'";
                    }
                    whereclause += ") ";
                }
            }
            catch (Exception ex)
            {

            }
            
            if (!_modoDepuracion)
            {
                if (this.LocalFactory != null)
                {

                    Dictionary<int, String> candidates = new Dictionary<int, string>();
                    
                    string sqlQuery = "Select * from CANDIDATES "+whereclause +"AND CANDIDATES.IDFACTORY IN(SELECT BILLETRACKS.FACTORYIDORIGIN FROM BILLETRACKS WHERE BILLETRACKS.FACTORYIDDESTINY=" + this.LocalFactory.ID + ")";
                    OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);

                    if (dr.HasRows)
                    {
                        int curr_candidate_id;
                        string curr_candidate_path;
                        while (dr.Read())
                        {

                            curr_candidate_id = this.GetFieldInt320(dr, "IDIMAGE");
                            curr_candidate_path = this.LocalFactory.PathImages + this.GetFieldString(dr, "IMAGEPATH");
                            candidates.Add(curr_candidate_id, curr_candidate_path);

                        } //while dr.read();
                        return candidates;
                    } // if (dr.hasrows)
                    else return null;
                } // if (localfactory!=null)
                else return null;
            }
            else
            {

                //Solo para depuracion
                int i = 0;
                string pathacer = @"C:\Users\Cesar\Desktop\Billetrack Adrian\datos\212119_acer";
                Dictionary<int, String> candidates = new Dictionary<int, string>();
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pathacer);
                System.IO.FileInfo[] files = null;
                files = di.GetFiles("*.jpg");
                foreach (System.IO.FileInfo fi in files)
                {

                    if (!fi.FullName.Contains("ORIGINAL")) candidates.Add(i, fi.FullName);
                    i++;

                }
                return candidates;
            }
        }
       public int InsertImage(string Path, imgStats stats)
        {
            if (!_modoDepuracion)
            {
                //Insertamos la imagen correspondiente
                String insertQuery =
                    "INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED)" +
                    "VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N')";
                return this._billetrackDB.InsertCallReturninId(insertQuery, "IDIMAGE");
            }
            else return 0;
        }
        //!!!DONE 

        public int InsertImage(string Path, imgStats stats, Billet data)
        {
            // RPC
            // METER ESTADISTICOS EN BD

            if (!_modoDepuracion)
            {
                int idBillet = this.InsertBillet(data);
                //Insertamos la imagen correspondiente
                String insertQuery =
                    "INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED,DISTANCE)" +
                    "VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N',"+data.Distance +")";
                int returnedid = this._billetrackDB.InsertCallReturninId(insertQuery, "IDIMAGE");

                //Insertamos el match nuevo correspondiente a esta imagen
                insertQuery =
                    "INSERT INTO MATCHES(IDBILLET,IDIMAGEORIGIN)" +
                    "VALUES (" + idBillet + "," + returnedid + ")";
                if (this._billetrackDB.InsertCall(insertQuery)) { } // OK;
                else { } // algo fue mal al meter el match
                return returnedid; 
            }
            else return 0;
        }

       public int InsertImage(string Path, imgStats stats, int IdBillet)
        {
            if (!_modoDepuracion)
            {
           //Insertamos la imagen correspondiente
            String insertQuery =
                "INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED)" +
                "VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N')";
            int returnedid = this._billetrackDB.InsertCallReturninId(insertQuery, "IDIMAGE");

            //Insertamos el match nuevo correspondiente a esta imagen
            insertQuery =
                "INSERT INTO MATCHES(IDBILLET,IDIMAGEORIGIN)" +
                "VALUES (" + IdBillet + "," + returnedid + ")";
            if (this._billetrackDB.InsertCall(insertQuery)) { } // OK;
            else { } // algo fue mal al meter el match
            return returnedid;
            }
            else return 0;
        }
       //!!!DONE
        public int InsertBillet(Billet data)
        {
            data.Family.ID = this.InsertFamily(data.Family);
            //Insertamos la imagen correspondiente

            String insertQuery = data.GetSQLInsert("BILLETS");

            return this._billetrackDB.InsertCallReturninId(insertQuery, "IDBILLET");
        }
        //!!!DONE
        public int InsertFamily(Family data)
        {
            int idfamily=-1;
            //Comprobamos que no esta metido:
            string sqlQuery = "Select * from FAMILIES where CASTNUMBER='" + data.Cast+"'";

            OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    idfamily = this.GetFieldInt320(dr, "IDFAMILY");
                }
            }
            else
            {
                String insertQuery = data.GetSQLInsert("FAMILIES");
                idfamily = this._billetrackDB.InsertCallReturninId(insertQuery, "IDFAMILY");
            }
            return idfamily;
        }
        #endregion
        #region TODO
        //TO DO
       public Dictionary<int, String> GetCandidates(int SearchValue)
        { return new Dictionary<int, String>(); }
        //TO DO
       public Dictionary<int, String> GetCandidates(DateTime SearchValue)
        { return new Dictionary<int, String>(); }
        #endregion
       //TO DO Devuelve el ImageID
     
           //TO DO 
      public void InsertEmptyImage(List<Billetrack.EventBilletrack> eventos,Billet billet=null) { }
      public void InserEvent(int ImageID, List<Billetrack.EventBilletrack> eventos) {

          foreach (EventBilletrack  item in eventos)
          {
              string sqlQuery = "Select idevent from events where DESCRIPTION='" + item.ToString().ToUpper()+ "'";


              OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
              if (dr.HasRows)
              {
                  while (dr.Read())
                  {
                    int  idevent = this.GetFieldInt320(dr, "IDEVENT");
                    string insertquery = "INSERT INTO IMAGEEVENTS(IDIMAGE,IDEVENT) VALUES(" + ImageID + "," + idevent + ")";
                    this._billetrackDB.InsertCall(insertquery);
                 }
                  
              }
              
          }
      
      
      }

       public Billet InsertMatch(int ImageID1, int Image2, double precision) {

           try
           {
               if (!_modoDepuracion)
               {
                   if (Image2 >= 0)
                   {
                       int idbillet = -1;
                       int precisionInt = (int)precision;
                       int idfactoryorigen = -1;
                       int idfactorydestino = -1;
                       int idbilletrack = -1;

                       // Seleccionamos la factoria de la imagen destino:

                       string sqlQuery = "Select idfactory from images where idimage=" + ImageID1;


                       OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               idfactorydestino = this.GetFieldInt320(dr, "IDFACTORY");

                           }

                       }

                       // Seleccionamos la factoria de la imagen origen:

                       sqlQuery = "Select idfactory from images where idimage=" + Image2;

                       dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               idfactoryorigen = this.GetFieldInt320(dr, "IDFACTORY");

                           }

                       }

                       sqlQuery = "Select idbilletrack from billetracks where factoryidorigin=" + idfactoryorigen + " and factoryiddestiny=" + idfactorydestino;

                       dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               idbilletrack = this.GetFieldInt320(dr, "IDBILLETRACK");

                           }

                       }




                       string UpdateQuery = "UPDATE MATCHES SET IDIMAGEDESTINY=" + ImageID1 + ",ACCURACY=" + precisionInt + ",IDBILLETRACK=" + idbilletrack + " where MATCHES.IDIMAGEORIGIN=" + Image2;
                       this._billetrackDB.UpdateCall(UpdateQuery);


                       // Si el billet de la imagen destino es distinto del origen, lo borramos.
                       sqlQuery = "Select idbillet from matches where IDIMAGEORIGIN=" + Image2 + "";

                       dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               idbillet = this.GetFieldInt320(dr, "IDBILLET");
                           }
                       }
                       // 
                       if (idbillet > 0)
                       {
                           UpdateQuery = "UPDATE MATCHES SET IDBILLET=" + idbillet + " where IDIMAGEORIGIN=" + ImageID1;
                           this._billetrackDB.UpdateCall(UpdateQuery);
                       }

                       sqlQuery = "Select * from BILLETINFORMATION where IDBILLET=" + idbillet + "";

                       dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               return new Billet(new Family(this.GetFieldString(dr, "CASTNUMBER")), this.GetFieldString(dr, "BILLETDESCRIPTOR"), this.GetFieldInt320(dr, "LINE"), this.GetFieldInt320(dr, "NCUT"), 0, this.GetFieldInt320(dr, "POSITION"));
                           }
                       }
                   }
                   else ErrorMatch(ImageID1);
               }

               return null;
           }
           catch (Exception e)
           {
               throw new SpinException("Error en Metodo InsertMatch:"+e.GetBaseException().ToString());
           }
       }
       //To DO
       public void ErrorMatch(int ImageID1) {

           if (!_modoDepuracion)
           {
               string UpdateQuery = "UPDATE IMAGES SET ERRORMATCH='Y' where IDIMAGE=" + ImageID1 ;
               this._billetrackDB.UpdateCall(UpdateQuery);
           }
       }

       public void VaciaInstalacion(Factory fact, bool borrandoimagenes)
       {

       }
        #region AuxMethods
       private Int32 GetFieldInt32(OracleDataReader dr, string FieldName)
       {
            if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
               return dr.GetInt32((dr.GetOrdinal(FieldName)));
           else return -1;
       }

       private Int32 GetFieldInt320(OracleDataReader dr, string FieldName)
       {
           if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
               return dr.GetInt32((dr.GetOrdinal(FieldName)));
           else return 0;
       }


       private string GetFieldString(OracleDataReader dr, string FieldName)
       {
           if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
               return dr.GetString((dr.GetOrdinal(FieldName)));
           else return null;
       }


        #endregion

    }
}
