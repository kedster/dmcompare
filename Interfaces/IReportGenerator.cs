using System.Collections.Generic;
using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services.Reporting
{
    /// <summary>
    /// Interface defining report generation capabilities.
    /// </summary>
    public interface IReportGenerator
    {
        /// <summary>
        /// Exports comparison results to a specified output format/location.
        /// </summary>
        /// <param name="results">Comparison results</param>
        /// <param name="outputPath">File path to export report</param>
        void ExportReport(IEnumerable<ComparisonResult> results, string outputPath);
    }
}
