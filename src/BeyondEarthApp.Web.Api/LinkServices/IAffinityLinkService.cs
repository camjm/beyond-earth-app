using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IAffinityLinkService
    {
        void AddSelfLink(Affinity affinity);
    }
}
