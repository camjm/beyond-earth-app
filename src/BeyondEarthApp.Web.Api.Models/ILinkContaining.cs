using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    /// <summary>
    /// For service model classes
    /// </summary>
    public interface ILinkContaining
    {
        List<Link> Links { get; set; }

        void AddLink(Link link);
    }
}
