using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;
using SpinPlatform.Errors;
//using System.Data.OracleClient;

namespace SpinPlatform.IO.DataBase
{
    /// <summary>
    /// Módulo para conexión con bases de datos Oracle
    /// Utiliza objetos de tipo ADO
    ///   /// </summary>
   
    


    class SpinOracleDb
    {
        #region Variables
        private string _dbConnectionString;
        private OracleCommand _dbCommand = null;
        private OracleConnection _dbConnection = null;
        private bool _connected = false;

        #endregion

        public bool Connected
        {
            get { return this._connected; }
        }


        public void SetConecction(string DBConnectionString)
        {
            this._dbConnectionString = DBConnectionString;
        }


        /// <summary>
        /// Open the Oracle Connection
        /// No parameters needed, all passed by config file
        /// </summary>
        public void Start()
        {
            if (this._connected) return;
            try
            {
                    this._dbConnection = new OracleConnection();
                    _dbConnection.ConnectionString = _dbConnectionString;
                    
                    _dbConnection.Open();
                    this._connected = true;
            }            
            catch (OracleException e)
            {
                // Do something
                this._connected = false;
                throw new SpinException("SpinOracleDb: Error en metodo Start :" + e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                _dbConnection.Close();
                _dbConnection.Dispose();

                this._connected = false;
            }
            catch (OracleException e)
            {
                // Do something
                if (_dbConnection.State == ConnectionState.Open) this._connected = true;
                else this._connected = false;
                throw new SpinException("SpinOracleDb: Error en metodo Stop :" + e.Message);
            }

        }

        
        public bool ProcedureCall(string ProcedureName, string[] ProcedureParams, OracleDbType[] ProcedureParamsType,string[] ProcedureParamsValues)
        {
            try
            {
                if (!this._connected) return false;
                if (ProcedureParams.Length != ProcedureParamsValues.Length)
                {
                    // length of params and param values must be the same!!
                    return false;
                }

                this._dbCommand = new OracleCommand(ProcedureName, this._dbConnection);
                this._dbCommand.CommandType = System.Data.CommandType.StoredProcedure;


                for (int i = 0; i < ProcedureParams.Length; i++)
                {
                    this._dbCommand.Parameters.Add(":" + ProcedureParams[i].ToString(), ProcedureParamsType[i]).Value = ProcedureParamsValues[i];
                }

                _dbCommand.ExecuteNonQuery();
                this._dbCommand.Dispose();
                
                return true;
            }
            catch (OracleException e)
            {
                throw new SpinException("SpinOracleDb: Error en metodo ProcedureCall :" + e.Message);
                return false;
            }
                
                     
        }
        /// <summary>
        /// Inserts a new row into the database.
        /// If no rows affected or not connected, return false
        /// true if Insert was successful
        /// </summary>
        /// <param name="InsertQuery">Query to insert values in the database</param>
        /// <returns>true if Insert succeeded, false in case or not connected or not inserted</returns>
        public bool InsertCall(string InsertQuery)
        {

            try
            {
                if (!this._connected) return false;
                this._dbCommand = new OracleCommand(InsertQuery, this._dbConnection);
                int fieldsaffected = this._dbCommand.ExecuteNonQuery();
                this._dbCommand.Dispose();


                return (fieldsaffected > 0);
            }
            catch (Exception E)
            {

                throw new SpinException("SpinOracleDb: Error en metodo InsertCall" + E.Message);
            }
         }


        /// <summary>
        /// Insert a new row in the database, returning one field defined in FieldId
        /// Useful for autoincrement primary_keys
        /// </summary>
        /// <param name="InsertQuery">Query to be executed against the database</param>
        /// <param name="FieldId">Name of the column with the autoincrement Id</param>
        /// <returns>the value of the primary key for the inserted row</returns>
        public int InsertCallReturninId(string InsertQuery,string FieldId)
        {
            try
            {
                InsertQuery += " RETURNING " + FieldId + " into :returnedID";

                if (!this._connected) return 0;
                this._dbCommand = new OracleCommand(InsertQuery, this._dbConnection);
                this._dbCommand.Parameters.Add("returnedID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                this._dbCommand.ExecuteNonQuery();
                int id = Int32.Parse(this._dbCommand.Parameters["returnedID"].Value.ToString());
                this._dbCommand.Dispose();

                return id;
            }
            catch (Exception e)
            {
                
                throw new SpinException("SpinOracleDb: Error en metodo InsertCallReturningId :" + e.Message);
            }
        }



        /// <summary>
        /// Permite Ejecutar una sentencia Select pasada como parametro
        /// Forma para leer los resultados
        /// while (dr.Read())
        ///    {
        ///        // Formas de leer el campo:
        ///        dr.GetString((dr.GetOrdinal("nombrecolumna"))); // Se puede sacar cualquier tipo de datos.
        ///        dr.GetFloat(dr.GetOrdinal("nombrecolumna")));
        ///        dr["nombrecolumna"].ToString(); // En string
        ///    }
        /// </summary>
        /// <param name="SelectQuery">Consulta Select en Formato SQL</param>
        /// <returns>Un Objeto OracleDataReader con los valores retornados (null en caso de error)</returns>
        public OracleDataReader SelectCall(string SelectQuery)
        {
            try
            {
                if (!this._connected) return null;
                this._dbCommand = new OracleCommand(SelectQuery, this._dbConnection);
                this._dbCommand.CommandType = CommandType.Text;

                OracleDataReader dr = _dbCommand.ExecuteReader();
                this._dbCommand.Dispose();

                return dr;
            }
            catch (Exception e)
            {
                
                throw new SpinException("SpinOracleDb: Error en metodo SelectCall :" + e.Message);
            }
        }

        public bool UpdateCall(string UpdateQuery)
        {
            try
            {
                if (!this._connected) return false;
                this._dbCommand = new OracleCommand(UpdateQuery, this._dbConnection);
                int retorno = this._dbCommand.ExecuteNonQuery();
                this._dbCommand.Dispose();

                return (retorno > 0);
            }
            catch (Exception e)
            {
                
                throw new SpinException("SpinOracleDb: Error en metodo UpdateCall :" + e.Message);
            }
         }

        public int DeleteCall(string DeleteQuery)
        {
            return 0;
        }

        public bool NonQueryCall(string NonQuery)
        {
            try
            {
                if (!this._connected) return false;
                this._dbCommand = new OracleCommand(NonQuery, this._dbConnection);
                int retorno = this._dbCommand.ExecuteNonQuery();
                this._dbCommand.Dispose();

                return (retorno > 0);
            }
            catch (Exception e)
            {

                throw new SpinException("SpinOracleDb: Error en metodo NonQueryCall :" + e.Message);
            }
        }


    }
}
