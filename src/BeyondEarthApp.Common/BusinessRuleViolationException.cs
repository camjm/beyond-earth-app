using System;

namespace BeyondEarthApp.Common
{
    /// <summary>
    /// Trivial Exception type to indicate an attempted violation of the business logic
    /// </summary>
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message) { }
    }
}
