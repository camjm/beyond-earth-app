using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IBuildingLinkService
    {
        void AddSelfLink(Building building);
    }
}
