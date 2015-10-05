using System;
using log4net;

namespace BeyondEarthApp.Common.Logging
{
    /// <summary>
    /// Wrapper to prevent tight coupling to the static log4net LogManager. Used to request <see cref="ILog"/> instances.
    /// </summary>
    public interface ILogManager
    {
        ILog GetLog(Type typeAssociatedWithRequestedLog);
    }
}
