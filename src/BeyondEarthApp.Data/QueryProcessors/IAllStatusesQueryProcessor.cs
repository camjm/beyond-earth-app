using System.Collections.Generic;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IAllStatusesQueryProcessor
    {
        IEnumerable<Status> GetStatuses();
    }
}
