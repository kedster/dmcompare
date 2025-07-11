using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DMSRuntimeComparer.Services.Sql
{
    public class SqlMountService : ISqlMount
    {
        private readonly string _serverConnectionString;

        public SqlMountService(string serverConnectionString)
        {
            _serverConnectionString = serverConnectionString;
        }

        public List<string> RestoreDatabases(IEnumerable<string> bakFilePaths)

        {
            var restored = new List<string>();

            foreach (var bakPath in bakFilePaths)
            {
                string dbName = Path.GetFileNameWithoutExtension(bakPath) + "_Restored";

                // Step 1: Get logical names
                string logicalDataName = null, logicalLogName = null;
                using (var conn = new SqlConnection(_serverConnectionString))
                {
                    conn.Open();
                    using var cmd = new SqlCommand($"RESTORE FILELISTONLY FROM DISK = N'{bakPath}'", conn);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        logicalDataName = reader.GetString(0);
                        if (reader.Read()) logicalLogName = reader.GetString(0);
                    }
                }

                if (logicalDataName == null || logicalLogName == null)
                    throw new InvalidOperationException($"Logical file names not found in: {bakPath}");

                // Step 2: Restore database
                string restoreSql = $@"
RESTORE DATABASE [{dbName}]
FROM DISK = N'{bakPath}'
WITH MOVE N'{logicalDataName}' TO N'C:\SQLData\{dbName}.mdf',
     MOVE N'{logicalLogName}' TO N'C:\SQLData\{dbName}_log.ldf',
     REPLACE;";

                using (var conn = new SqlConnection(_serverConnectionString))
                {
                    conn.Open();
                    using var cmd = new SqlCommand(restoreSql, conn);
                    cmd.CommandTimeout = 0; // no timeout
                    cmd.ExecuteNonQuery();
                    restored.Add(dbName);
                }
            }

            return restored;
        }

        public void DropDatabase(string dbName)
        {
            using (var conn = new SqlConnection(_serverConnectionString))
            {
                conn.Open();
                using var cmd = new SqlCommand($"DROP DATABASE IF EXISTS [{dbName}];", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void CleanupFiles(string dbName)
        {
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RestoredDbs");
            string mdf = Path.Combine(dataPath, $"{dbName}.mdf");
            string ldf = Path.Combine(dataPath, $"{dbName}_log.ldf");

            if (File.Exists(mdf)) File.Delete(mdf);
            if (File.Exists(ldf)) File.Delete(ldf);
        }

        private readonly Dictionary<string, (string mdf, string ldf)> _dbFiles = new();

        public void AddDatabaseFiles(string dbName, string mdfPath, string ldfPath)
        {
            if (string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(mdfPath) || string.IsNullOrEmpty(ldfPath))
                throw new ArgumentException("Database name and file paths cannot be null or empty.");

            _dbFiles[dbName] = (mdfPath, ldfPath);
        }

        public (string mdf, string ldf) GetDatabaseFiles(string dbName)
        {
            if (_dbFiles.TryGetValue(dbName, out var files))
                return files;

            throw new KeyNotFoundException($"No files found for database: {dbName}");
        }

    }


}
