namespace BeyondEarthApp.Common.Security
{
    /// <summary>
    /// Provides convenient access to data describing the current user.
    /// </summary>
    public interface IUserSession
    {
        string Firstname { get; }

        string Lastname { get; }

        string Username { get; }

        bool IsInRole(string roleName);
    }
}
