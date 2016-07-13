using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data.SqlClient;
using tarsierSQL = TarsierEyes.MySQL;
using tarsierSQLString = TarsierEyes.Common.SQLStrings;


/**
 *  AMSCore Generic Class storage
 *  @author Jcabrito <Oct. 27, 2015>
 *  @return mixed
 */
namespace AMSCore
{
    public class GenericStorage
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _constring    = string.Empty;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _apiEndpoint  = string.Empty;        

        public string connectionString
        {
            get
            {
                return _constring;
            }
            set
            {
                _constring = value;
            }
        }

        public string APIEndpoint
        {
            get
            {
                return _apiEndpoint;
            }
            set
            {
                _apiEndpoint = value;
            }
        }

        // sql server connections
        public SqlConnection sqlConnection { get; set; }

        public string httpMethod { get; set; }

        public DataTable getTable(string sql)
        {
            DataTable table = new DataTable();

            try
            {

                using (var con = new MySqlConnection(this.connectionString))
                using (var adapter = new MySqlDataAdapter(sql, con))
                using (new MySqlCommandBuilder(adapter))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return ((table.Rows.Count <= 0) ? null : table);
            
        }

        /**
         * temporary set to non blocking code.
         * always return boolean true
         */
        public bool queueFailedRequest(string module, string reference_id)
        {

            bool response = true;

            tarsierSQL.Que processSql = null;

            if (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(reference_id))
                return response;

            string sql = "call usp_tmpUpdates('" + module + "','" + reference_id + "')";
            
            processSql = tarsierSQL.Que.Execute(this.connectionString.ToString(),
                    sql, tarsierSQL.Que.ExecutionEnum.ExecuteNonQuery);

            return response;

        }

        /**
         * temporary set to non blocking code.
         * always return boolean true
         */
        public bool executeQuery(string sql)
        {

            bool response = true;

            tarsierSQL.Que processSql = null;

            if (string.IsNullOrEmpty(sql))
                return response;

            processSql = tarsierSQL.Que.Execute(this.connectionString.ToString(),
                    sql, tarsierSQL.Que.ExecutionEnum.ExecuteNonQuery);

            return response;

        }

        public object getQueryvalue(string sql)
        {

            object processSql = null;

            if (string.IsNullOrEmpty(sql))
                return processSql;

            processSql = tarsierSQL.Que.GetValue(this.connectionString.ToString(), sql);

            return processSql;

        }


        // Start SQL SERVER connections
        public SqlConnection createSQLDB()
        {
            string newConString = this.connectionString;

            if (string.IsNullOrEmpty(newConString))
                newConString = Conn.AMS_DATABASE_SERVER + Conn.AMS_DATABASE_NAME + Conn.AMS_DATABASE_PASSWORD + Conn.AMS_DATABASE_ID;

            SqlConnection conn = new SqlConnection(newConString);
            
            this.sqlConnection = conn;
            
            return conn;
        }

        public bool executeInsertQuery(string sqlString)
        {

            this.sqlConnection.Open();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, this.sqlConnection);

                if (sqlCommand.ExecuteNonQuery() > 0) return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            this.sqlConnection.Close();

            return false;

        }

        public DataTable executeSelectQuery(string sqlString)
        {

            this.sqlConnection.Open();

            DataTable table = new DataTable();

            SqlDataAdapter adtor = new SqlDataAdapter(sqlString, this.sqlConnection);
            adtor.Fill(table);

            this.sqlConnection.Close();

            return table;

        }
    }

}
