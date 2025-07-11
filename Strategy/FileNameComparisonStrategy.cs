// FileNameComparisonStrategy.cs - placeholder for Strategy layer
using DMSRuntimeComparer.Models;
using System;

namespace DMSRuntimeComparer.Services.ComparisonStrategies
{
    public class FileNameComparisonStrategy : IComparisonStrategy
    {
        public ComparisonResult Compare(Metadata left, Metadata right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Metadata objects cannot be null");
            }

            bool areEqual = string.Equals(left.FileName, right.FileName, StringComparison.OrdinalIgnoreCase);

            return new ComparisonResult
            {
                Identifier = left.RelativePath,
                ComparisonType = "FileName",
                AreEqual = areEqual,
                Differences = areEqual ? null : new System.Collections.Generic.List<string> { $"File names differ: '{left.FileName}' vs '{right.FileName}'" },
                LeftChecksum = left.Checksum,
                RightChecksum = right.Checksum
            };
        }
    }
}
