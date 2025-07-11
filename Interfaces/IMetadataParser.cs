using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services.Metadata
{
    /// <summary>
    /// Interface defining metadata extraction and parsing behavior.
    /// </summary>
    public interface IMetadataParser
    {
        /// <summary>
        /// Calculates and returns checksum string for given file.
        /// </summary>
        string CalculateChecksum(string filePath);

        /// <summary>
        /// Populates checksum and other metadata information recursively on a FolderNode.
        /// </summary>
        void PopulateChecksums(FolderNode rootNode);
    }
}
