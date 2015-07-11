using System;

namespace BeyondEarthApp.Common
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
