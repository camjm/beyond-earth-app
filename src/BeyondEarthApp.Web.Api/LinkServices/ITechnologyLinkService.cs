using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface ITechnologyLinkService
    {
        void AddLinks(Technology technology);

        void AddSelfLink(Technology technology);

        void AddLinksToChildren(Technology technology);

        Link GetSelfLink(long technologyId);

        Link GetAllTechnologiesLink();
    }
}
