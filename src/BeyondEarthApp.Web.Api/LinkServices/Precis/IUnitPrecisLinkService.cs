using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public interface IUnitPrecisLinkService
    {
        void AddSelfLink(UnitPrecis unit);

        Link GetAllUnitsLink();
    }
}
