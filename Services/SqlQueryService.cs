using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services.Sql
{
    public class SqlQueryService
    {
        private readonly string _connectionStringTemplate;

        public SqlQueryService(string connectionStringTemplate)
        {
            _connectionStringTemplate = connectionStringTemplate;
        }

        public SqlObject ExtractSqlObject(string databaseName, string objectName)
        {
            string connStr = string.Format(_connectionStringTemplate, databaseName);
            var dataTable = new DataTable();

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using var cmd = new SqlCommand($"SELECT * FROM [{objectName}]", conn);
                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }

            return new SqlObject
            {
                DatabaseName = databaseName,
                ObjectName = objectName,
                ObjectType = "Table",
                Data = dataTable
            };
        }

        public List<string> GetAllTables(string databaseName)
        {
            string connStr = string.Format(_connectionStringTemplate, databaseName);
            var tables = new List<string>();

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using var cmd = new SqlCommand("SELECT name FROM sys.tables", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    tables.Add(reader.GetString(0));
            }

            return tables;
        }
    }
}
