using System;

namespace BeyondEarthApp.Data.Exceptions
{
    /// <summary>
    /// Thrown by data access objects when the primary data object is not found.
    /// </summary>
    [Serializable]
    public class RootObjectNotFoundException : Exception
    {
        public RootObjectNotFoundException(string message) : base(message) { }
    }
}
