using System;

namespace BeyondEarthApp.Data.Exceptions
{
    /// <summary>
    /// /// Thrown by data access objects when a required child of the primary data object is not found.
    /// </summary>
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message) { }
    }
}
