// FolderNode.cs - placeholder for Models layer
using System.Collections.Generic;

namespace DMSRuntimeComparer.Models
{
    /// <summary>
    /// Represents a folder and its contents in a tree structure.
    /// </summary>
    public class FolderNode
    {
        public string Path { get; set; }
        public List<FolderNode> SubFolders { get; set; } = new List<FolderNode>();
        public List<string> Files { get; set; } = new List<string>();

        public FolderNode() { }

        public FolderNode(string path)
        {
            Path = path;
        }
    }
}
