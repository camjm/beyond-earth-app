using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface ITechnologyLinkService
    {
        void AddLinks(Technology technology);

        void AddSelfLink(TechnologyPrecis technology);

        void AddLinksToChildren(Technology technology);

        Link GetSelfLink(long technologyId);

        Link GetAllTechnologiesLink();
    }
}
