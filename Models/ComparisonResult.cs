using System.Collections.Generic;

namespace DMSRuntimeComparer.Models
{
    /// <summary>
    /// Represents the result of comparing a specific item (file or SQL object).
    /// </summary>
    public class ComparisonResult
    {
        public string Identifier { get; set; }         // Could be file path or SQL object name
        public string ComparisonType { get; set; }     // "File", "SQL", "Metadata"
        public bool AreEqual { get; set; }
        public List<string> Differences { get; set; } = new List<string>();
        public string LeftChecksum { get; set; }
        public string RightChecksum { get; set; }

        public override string ToString()
        {
            var result = AreEqual ? "MATCH" : "DIFF";
            return $"[{ComparisonType}] {Identifier} => {result}";
        }
    }
}
