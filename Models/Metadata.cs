using System;

namespace DMSRuntimeComparer.Models
{
    /// <summary>
    /// Represents metadata for a file or data element.
    /// </summary>
    public class Metadata
    {
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public long SizeBytes { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Checksum { get; set; }  // Can be SHA256 or MD5
        public string Source { get; set; }    // "FolderA" or "FolderB"

        public override string ToString()
        {
            return $"{Source}::{RelativePath} [{Checksum}]";
        }
    }
}
