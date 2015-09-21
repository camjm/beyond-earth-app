using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface ITechnologyLinkService
    {
        void AddSelfLink(Technology technology);

        void AddLinksToChildren(Technology technology);

        Link GetAllTechnologiesLink();
    }
}
