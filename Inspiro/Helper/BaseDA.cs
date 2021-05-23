using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inspiro.Helper
{
    public class BaseDA : IDisposable
    {
        private readonly ConnectionSettings connection;
        protected IDbConnection DbConnection;

        public BaseDA()
        {
            this.connection = new ConnectionSettings();
        }

        public string ExecuteScalar(string query)
        {
            var result = String.Empty;

            using (SqlConnection conn = new SqlConnection(connection.conSql))
            {
                if (string.IsNullOrEmpty(conn.ConnectionString))
                {
                    //string profile = "DefaultConnection";
                    //var connectionString = ConfigHelper.GetConnectionString(profile);
                    var connectionString = connection.conSql;
                    conn.ConnectionString = connectionString;
                }

                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandText = query,
                        CommandType = CommandType.Text,
                    };

                    result = Convert.ToString(cmd.ExecuteScalar());
                }
                catch (SqlException ex)
                {
                    result = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }

        public DataTable ExecuteQueryWithParam(string query, List<SqlParameter> listParam, ref string message)
        {
            var result = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection.conSql))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = conn,
                        CommandText = query,
                        CommandType = CommandType.Text,
                        CommandTimeout = 0
                    };

                    if (listParam.Count != 0)
                    {
                        foreach (var item in listParam)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    result.Load(cmd.ExecuteReader());
                }
                catch (SqlException ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                        if (DbConnection != null && DbConnection.State == ConnectionState.Open)
                        {
                            DbConnection.Close();
                        }
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
