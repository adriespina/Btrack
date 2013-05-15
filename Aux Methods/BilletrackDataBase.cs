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
       private BilletrackDispatcher _padre;

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
           catch (Exception e)
           {
               throw new SpinException("BilletrackDataBase: Error en metodo CurrentDomain_Assembly" + e.Message);
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

       public BilletrackDataBase(BilletrackDispatcher padre,string factoryname, string DBConnectionString, bool _modoDepuracion)
       {
           try
           {
               AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            
               this._padre = padre;
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
                   else throw new SpinException("BilletrackDataBase: Base de Datos no Conectada, sin excepcion lanzada");
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
               throw new SpinException("BilletrackDataBase: error en Construtor : " + E.Message);
           }
        }

     
        #region DONE
        BilletrackSystem GetContextFromDB(String name)
        {
            try
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
                    dr.Dispose();

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
                    dr.Dispose();
                }
                return ret;
            }
            catch (Exception e)
            {
                throw new SpinException("BilletrackDataBase: GetContextFromDB(string)" + e.Message);
            }
        }
        Factory GetFactoryFromDB(String name)
        {
            try
            {
                Factory ret = new Factory();

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
                    dr.Dispose();
                    return ret;
                }
                else
                {
                    dr.Dispose();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new SpinException("BilletrackDataBase: GetFactoryFromDB(string)" + e.Message);
            }
         }
        Factory GetFactoryFromDB(int ID)
        {
            try
            {
                Factory ret = new Factory();

                string sqlQuery = "Select * from FACTORYPLACE where IDFACTORYPLACE=" + ID;

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
                    dr.Dispose();
                    return ret;
                }
                else
                {
                    dr.Dispose();
                    return null;
                }

            }
            catch (Exception e)
            {
            throw new SpinException("BilletrackDataBase: GetFactoryFromDB(int)" + e.Message);
            }
        }
        public Dictionary<int, String> GetCandidates(string SearchValue)
        {
            //  return new Dictionary<int, String>();
            try
            {
                string mensajelog = "";

                if (!_modoDepuracion)
                {
                    if (this.LocalFactory != null)
                    {

                        Dictionary<int, String> candidates = new Dictionary<int, string>();

                        string sqlQuery = "Select * from CANDIDATES where CANDIDATES.CASTNUMBER='" + SearchValue + "' AND CANDIDATES.IDFACTORY IN(SELECT BILLETRACKS.FACTORYIDORIGIN FROM BILLETRACKS WHERE BILLETRACKS.FACTORYIDDESTINY=" + this.LocalFactory.ID + ")";
                        OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
                        mensajelog += "Pedidos candidatos de esta colada:" + SearchValue;

                        if (dr.HasRows)
                        {
                            int curr_candidate_id;
                            string curr_candidate_path;
                            while (dr.Read())
                            {

                                curr_candidate_id = this.GetFieldInt320(dr, "IDIMAGE");
                                curr_candidate_path = this.LocalFactory.PathImages + "\\" + this.GetFieldString(dr, "IMAGEPATH");
                                candidates.Add(curr_candidate_id, curr_candidate_path);

                            } //while dr.read();
                            dr.Dispose();
                            return candidates;
                        } // if (dr.hasrows)
                        else
                        {
                            dr.Dispose();
                            return null;
                        }
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
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: GetCandidates(string)" + e.Message);
            
            }
        }
        public Dictionary<int, String> GetCandidates(params string[] parameters )
        {
            try
            {
            if (parameters.Length == 0)
            {
                throw new SpinException("Search Parameters Empty in GetCandidates Method ");
            }
            string mensajelog = "";
            //  return new Dictionary<int, String>();
            string whereclause = "";
            Boolean firstcondition = true;
           
                mensajelog += "Pedidos candidatos de estas coladas:";
                foreach (string parameter in parameters)
                {
                    mensajelog += parameter + ",";
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
                        dr.Dispose();
                        return candidates;
                    } // if (dr.hasrows)
                    else
                    {
                        dr.Dispose();
                        return null;
                    }
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
            catch (Exception e)
            {
                throw new SpinException("BilletrackDataBase: GetCandidates(string[])" + e.Message);
            }
        }
       public int InsertImage(string Path, imgStats stats)
        {
            try
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
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: InsertImage(string,imgstats)" + e.Message);
            
            }
        }
        //!!!DONE 

        public int InsertImage(string Path, imgStats stats, Billet data)
        {
            try
            {
                if (!_modoDepuracion)
                {
                    int idBillet = this.InsertBillet(data);
                    //Insertamos la imagen correspondiente
                    String insertQuery =
                        "INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED,DISTANCE"+stats.GetSQLInsertColumns()+")" +
                        "VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N'," + data.Distance +","+stats.GetSQLValues()+")";
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
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: InsertImage(string,imgstats,billet)" + e.Message);
            
            }
        }

       public int InsertImage(string Path, imgStats stats, int IdBillet)
        {
            try
            {
                if (!_modoDepuracion)
                {
                    //Insertamos la imagen correspondiente
                    String insertQuery =
                         "INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED,DISTANCE" + stats.GetSQLInsertColumns() + ")" +
                        "VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N',-1," + stats.GetSQLValues() + ")";
               
                        //"INSERT INTO IMAGES(IDFACTORY,IMAGEPATH,IMAGEDATE,ISCORRECT,WASCONSUMED)" +
                        //"VALUES (" + this.LocalFactory.ID + ",'" + Path + "',to_date('" + DateTime.Now + "','dd/mm/yyyy hh24:mi:ss'),'Y','N')";
                       
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
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: InsertImage(string,imgstats,int)" + e.Message);
            
            }
        }
       //!!!DONE
        public int InsertBillet(Billet data)
        {
            try
            {
                data.Family.ID = this.InsertFamily(data.Family);
                //Insertamos la imagen correspondiente

                String insertQuery = data.GetSQLInsert("BILLETS");

                return this._billetrackDB.InsertCallReturninId(insertQuery, "IDBILLET");

            }
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: InsertBillet(Billet)" + e.Message);
            
            }
        }
        //!!!DONE
        public int InsertFamily(Family data)
        {
            try
            {
                int idfamily = -1;
                //Comprobamos que no esta metido:
                string sqlQuery = "Select * from FAMILIES where CASTNUMBER='" + data.Cast + "'";

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
                dr.Dispose();
                return idfamily;
            }
            catch (Exception e)
            {
                
                throw new SpinException("BilletrackDataBase: InsertFamily" + e.Message);
            
            }
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
          try
          {

              foreach (EventBilletrack item in eventos)
              {
                  string sqlQuery = "Select idevent from events where DESCRIPTION='" + item.ToString().ToUpper() + "'";


                  OracleDataReader dr = this._billetrackDB.SelectCall(sqlQuery);
                  if (dr.HasRows)
                  {
                      while (dr.Read())
                      {
                          int idevent = this.GetFieldInt320(dr, "IDEVENT");
                          string insertquery = "INSERT INTO IMAGEEVENTS(IDIMAGE,IDEVENT) VALUES(" + ImageID + "," + idevent + ")";
                          this._billetrackDB.InsertCall(insertquery);
                      }

                  }
                  dr.Dispose();

              }


          }
          catch (Exception e)
          {
              throw new SpinException("BilletrackDataBase: InsertEvent" + e.Message);
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
                       dr.Dispose();
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
                       dr.Dispose();
                       sqlQuery = "Select idbilletrack from billetracks where factoryidorigin=" + idfactoryorigen + " and factoryiddestiny=" + idfactorydestino;

                       dr = this._billetrackDB.SelectCall(sqlQuery);
                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               idbilletrack = this.GetFieldInt320(dr, "IDBILLETRACK");

                           }

                       }
                       dr.Dispose();


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
                       dr.Dispose();
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
                       dr.Dispose();
                   }
                   else ErrorMatch(ImageID1);
               }

               return null;
           }
           catch (Exception e)
           {
               throw new SpinException("BilletrackDataBase : Metodo InsertMatch:"+e.Message);
           }
       }
       //To DO
       public void ErrorMatch(int ImageID1) {
           try
           {

               if (!_modoDepuracion)
               {
                   string UpdateQuery = "UPDATE IMAGES SET ERRORMATCH='Y' where IDIMAGE=" + ImageID1;
                   this._billetrackDB.UpdateCall(UpdateQuery);
               }
           }
           catch (Exception e)
           {
               
               throw new SpinException("BilletrackDataBase: ErrorMatch" + e.Message);
            
           }
       }

       public void VaciaInstalacion(Factory fact, bool borrandoimagenes)
       {

       }
        #region AuxMethods
       private Int32 GetFieldInt32(OracleDataReader dr, string FieldName)
       {
           try
           {
               if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
                   return dr.GetInt32((dr.GetOrdinal(FieldName)));
               else return -1;
           }
           catch (Exception e)
           {
               
               throw new SpinException("BilletrackDataBase: GetFieldInt32" + e.Message);
            
           }
       }

       private Int32 GetFieldInt320(OracleDataReader dr, string FieldName)
       {
           try
           {
               if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
                   return dr.GetInt32((dr.GetOrdinal(FieldName)));
               else return 0;
           }
           catch (Exception e)
           {
               
               throw new SpinException("BilletrackDataBase: GetFieldInt320" + e.Message);
           }
       }


       private string GetFieldString(OracleDataReader dr, string FieldName)
       {
           try
           {
               if (!dr.IsDBNull(dr.GetOrdinal(FieldName)))
                   return dr.GetString((dr.GetOrdinal(FieldName)));
               else return null;
           }
           catch (Exception e)
           {
               
               throw new SpinException("BilletrackDataBase: GetFieldString" + e.Message);
           }
       }


        #endregion

    }
}
