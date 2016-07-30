using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IBuildingLinkService
    {
        void AddLinks(Building building);

        void AddSelfLink(BuildingPrecis building);

        void AddLinksToChildren(Building building);

        Link GetSelfLink(long buildingId);

        Link GetAllBuildingsLink();
    }
}
