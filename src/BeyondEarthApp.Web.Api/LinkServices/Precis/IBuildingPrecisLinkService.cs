using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public interface IBuildingPrecisLinkService
    {
        void AddSelfLink(BuildingPrecis building);
        
        Link GetAllBuildingsLink();
    }
}
