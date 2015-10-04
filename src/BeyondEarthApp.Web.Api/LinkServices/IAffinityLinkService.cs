using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IAffinityLinkService
    {
        void AddLinks(Affinity affinity);

        void AddSelfLink(Affinity affinity);

        void AddLinksToChildren(Affinity affinity);

        Link GetSelfLink(long affinityId);

        Link GetAllAffinitiesLink();
    }
}
