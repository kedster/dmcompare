public interface ISqlMount
{
    List<string> RestoreDatabases(IEnumerable<string> bakFilePaths);
    void DropDatabase(string dbName); // NEW
    void CleanupFiles(string dbName); // OPTIONAL
}
