using System;
using BeyondEarthApp.Common.Security;

namespace BeyondEarthApp.Web.Common.Security
{
    /// <summary>
    /// Provides convenient access to data describing the current request.
    /// </summary>
    public interface IWebUserSession : IUserSession
    {
        string ApiVersionInUse { get; }

        Uri RequestUri { get; }

        string HttpRequestMethod { get; }
    }
}
