using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services
{
    public class FolderComparer
    {
        /// <summary>
        /// Recursively builds a FolderNode tree from a root path.
        /// </summary>
        public FolderNode BuildFolderTree(string rootPath)
        {
            var rootNode = new FolderNode(rootPath);

            try
            {
                foreach (var dir in Directory.GetDirectories(rootPath))
                {
                    rootNode.SubFolders.Add(BuildFolderTree(dir));
                }

                rootNode.Files.AddRange(Directory.GetFiles(rootPath));
            }
            catch (Exception ex)
            {
                // Log or handle access exceptions as needed
                Console.WriteLine($"Error accessing {rootPath}: {ex.Message}");
            }

            return rootNode;
        }

        /// <summary>
        /// Compares two folder trees and returns file metadata differences.
        /// </summary>
        public List<ComparisonResult> CompareFolders(FolderNode folderA, FolderNode folderB)
        {
            var results = new List<ComparisonResult>();

            // Flatten files by relative path for easier comparison
            var filesA = FlattenFiles(folderA);
            var filesB = FlattenFiles(folderB);

            var allPaths = new HashSet<string>(filesA.Keys.Concat(filesB.Keys));

            foreach (var relativePath in allPaths)
            {
                filesA.TryGetValue(relativePath, out var metaA);
                filesB.TryGetValue(relativePath, out var metaB);

                if (metaA == null)
                {
                    results.Add(new ComparisonResult
                    {
                        Identifier = relativePath,
                        ComparisonType = "File",
                        AreEqual = false,
                        Differences = new List<string> { "Missing in Folder A" }
                    });
                    continue;
                }

                if (metaB == null)
                {
                    results.Add(new ComparisonResult
                    {
                        Identifier = relativePath,
                        ComparisonType = "File",
                        AreEqual = false,
                        Differences = new List<string> { "Missing in Folder B" }
                    });
                    continue;
                }

                // Compare checksums (assuming calculated)
                var areEqual = string.Equals(metaA.Checksum, metaB.Checksum, StringComparison.OrdinalIgnoreCase);
                var diffs = new List<string>();

                if (!areEqual)
                {
                    diffs.Add($"Checksum differs: A={metaA.Checksum}, B={metaB.Checksum}");
                }

                results.Add(new ComparisonResult
                {
                    Identifier = relativePath,
                    ComparisonType = "File",
                    AreEqual = areEqual,
                    Differences = diffs,
                    LeftChecksum = metaA.Checksum,
                    RightChecksum = metaB.Checksum
                });
            }

            return results;
        }

        /// <summary>
        /// Helper: Flatten FolderNode files into relative paths to Metadata.
        /// </summary>
        private Dictionary<string, Metadata> FlattenFiles(FolderNode root)
        {
            var dict = new Dictionary<string, Metadata>(StringComparer.OrdinalIgnoreCase);
            void Recurse(FolderNode node, string basePath)
            {
                foreach (var file in node.Files)
                {
                    var relative = Path.GetRelativePath(basePath, file);
                    var info = new FileInfo(file);

                    // For now, checksum will be null; MetadataParser calculates it later
                    var meta = new Metadata
                    {
                        FileName = Path.GetFileName(file),
                        RelativePath = relative,
                        SizeBytes = info.Length,
                        ModifiedDate = info.LastWriteTimeUtc,
                        Checksum = null, // To be calculated
                        Source = null
                    };
                    dict[relative] = meta;
                }

                foreach (var sub in node.SubFolders)
                {
                    Recurse(sub, basePath);
                }
            }

            Recurse(root, root.Path);
            return dict;
        }
    }
}
