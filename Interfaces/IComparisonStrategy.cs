using DMSRuntimeComparer.Models;

namespace DMSRuntimeComparer.Services.ComparisonStrategies
{
    /// <summary>
    /// Strategy pattern interface for comparing two Metadata objects.
    /// </summary>
    public interface IComparisonStrategy
    {
        ComparisonResult Compare(Metadata left, Metadata right);
    }
}
