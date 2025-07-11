using System;
using System.Data.SqlClient;

namespace DMSRuntimeComparer.Helpers
{
    public class SqlConnectionHelper : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public SqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Opens and returns an open SqlConnection. Reuses existing if open.
        /// </summary>
        public SqlConnection OpenConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();

            return _connection;
        }

        /// <summary>
        /// Closes and disposes the connection.
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null)
            {
                if (_connection.State != System.Data.ConnectionState.Closed)
                    _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
