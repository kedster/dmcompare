using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services
{
    public class ReportGenerator
    {
        /// <summary>
        /// Generates a CSV report for comparison results.
        /// </summary>
        public void ExportCsvReport(IEnumerable<ComparisonResult> results, string outputPath)
        {
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("Identifier,Type,Equal,Differences,LeftChecksum,RightChecksum");

            foreach (var r in results)
            {
                var diffs = string.Join("|", r.Differences ?? new List<string>());
                var line = $"{EscapeCsv(r.Identifier)},{r.ComparisonType},{r.AreEqual},{EscapeCsv(diffs)},{r.LeftChecksum},{r.RightChecksum}";
                sb.AppendLine(line);
            }

            File.WriteAllText(outputPath, sb.ToString());
        }

        private string EscapeCsv(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                return $"\"{s.Replace("\"", "\"\"")}\"";
            return s;
        }
    }
}
