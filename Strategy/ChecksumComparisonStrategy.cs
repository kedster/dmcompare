using DMSRuntimeComparer.Models;
using System;

namespace DMSRuntimeComparer.Services.ComparisonStrategies
{
    public class ChecksumComparisonStrategy : IComparisonStrategy
    {
        public ComparisonResult Compare(Metadata left, Metadata right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Metadata objects cannot be null");
            }

            bool areEqual = string.Equals(left.Checksum, right.Checksum, StringComparison.OrdinalIgnoreCase);

            return new ComparisonResult
            {
                Identifier = left.RelativePath,
                ComparisonType = "Checksum",
                AreEqual = areEqual,
                Differences = areEqual ? null : new System.Collections.Generic.List<string> { $"Checksum mismatch: {left.Checksum} vs {right.Checksum}" },
                LeftChecksum = left.Checksum,
                RightChecksum = right.Checksum
            };
        }
    }
}
