using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IBuildingLinkService
    {
        void AddLinks(Building building);

        void AddLinksToChildren(Building building);
    }
}
