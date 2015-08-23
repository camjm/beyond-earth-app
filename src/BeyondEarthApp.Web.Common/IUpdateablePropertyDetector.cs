using System.Collections.Generic;

namespace BeyondEarthApp.Web.Common
{
    /// <summary>
    /// Detirmines which properties to update
    /// </summary>
    public interface IUpdateablePropertyDetector
    {
        IEnumerable<string> GetNamesOfPropertiesToUpdate<TTarget>(object objectContainingUpdatedData);
    }
}
