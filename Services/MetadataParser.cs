using System;
using System.IO;
using System.Security.Cryptography;
using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services
{
    public class MetadataParser
    {
        /// <summary>
        /// Calculates SHA256 checksum for a file.
        /// </summary>
        public string CalculateChecksum(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Updates Metadata objects in-place with checksum values.
        /// </summary>
        public void PopulateChecksums(FolderNode rootNode)
        {
            void Recurse(FolderNode node)
            {
                for (int i = 0; i < node.Files.Count; i++)
                {
                    var file = node.Files[i];
                    var checksum = CalculateChecksum(file);

                    // Replace file path with a Metadata object for clarity (or alternatively build a map)
                    var info = new FileInfo(file);
                    var meta = new Metadata
                    {
                        FileName = info.Name,
                        RelativePath = Path.GetRelativePath(rootNode.Path, file),
                        SizeBytes = info.Length,
                        ModifiedDate = info.LastWriteTimeUtc,
                        Checksum = checksum,
                        Source = null
                    };
                    // Here you might want to store metadata somewhere; this example does not mutate FolderNode.Files list
                }

                foreach (var sub in node.SubFolders)
                    Recurse(sub);
            }

            Recurse(rootNode);
        }
    }
}
