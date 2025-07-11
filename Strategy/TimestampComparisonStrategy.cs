using DMSRuntimeComparer.Models;
using System;

namespace DMSRuntimeComparer.Services.ComparisonStrategies
{
    public class TimestampComparisonStrategy : IComparisonStrategy
    {
        private readonly TimeSpan _tolerance;

        /// <summary>
        /// Constructor allowing tolerance for timestamp difference.
        /// </summary>
        /// <param name="tolerance">Allowed difference (default 2 seconds)</param>
        public TimestampComparisonStrategy(TimeSpan? tolerance = null)
        {
            _tolerance = tolerance ?? TimeSpan.FromSeconds(2);
        }

        public ComparisonResult Compare(Metadata left, Metadata right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Metadata objects cannot be null");
            }

            var diff = left.ModifiedDate - right.ModifiedDate;
            bool areEqual = Math.Abs(diff.TotalSeconds) <= _tolerance.TotalSeconds;

            return new ComparisonResult
            {
                Identifier = left.RelativePath,
                ComparisonType = "Timestamp",
                AreEqual = areEqual,
                Differences = areEqual ? null : new System.Collections.Generic.List<string>
                {
                    $"Timestamp differs by {Math.Abs(diff.TotalSeconds)} seconds: {left.ModifiedDate:u} vs {right.ModifiedDate:u}"
                },
                LeftChecksum = left.Checksum,
                RightChecksum = right.Checksum
            };
        }
    }
}
