using System;
using System.Collections.Generic;
using System.IO;

namespace DMSRuntimeComparer.Helpers
{
    public static class FileSystemHelper
    {
        /// <summary>
        /// Recursively gets all files under a directory with optional search pattern.
        /// </summary>
        /// <param name="rootPath">Root directory</param>
        /// <param name="searchPattern">Optional search pattern, e.g. "*.bak"</param>
        /// <returns>List of full file paths</returns>
        public static List<string> GetFilesRecursive(string rootPath, string searchPattern = "*.*")
        {
            var files = new List<string>();

            try
            {
                foreach (var file in Directory.GetFiles(rootPath, searchPattern))
                {
                    files.Add(file);
                }

                foreach (var dir in Directory.GetDirectories(rootPath))
                {
                    files.AddRange(GetFilesRecursive(dir, searchPattern));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Log or ignore directories without access permission
            }
            catch (Exception ex)
            {
                // Log other exceptions if needed
                Console.WriteLine($"Error reading directory {rootPath}: {ex.Message}");
            }

            return files;
        }

        /// <summary>
        /// Safely combines multiple path segments.
        /// </summary>
        public static string Combine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                throw new ArgumentException("At least one path required");

            string result = paths[0];
            for (int i = 1; i < paths.Length; i++)
            {
                result = Path.Combine(result, paths[i]);
            }
            return result;
        }

        /// <summary>
        /// Checks if a file exists, returns false if path is null or empty.
        /// </summary>
        public static bool FileExists(string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        /// <summary>
        /// Checks if a directory exists, returns false if path is null or empty.
        /// </summary>
        public static bool DirectoryExists(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }
    }
}
