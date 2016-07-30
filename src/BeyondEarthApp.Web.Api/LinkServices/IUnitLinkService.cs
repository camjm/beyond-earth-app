using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IUnitLinkService
    {
        void AddLinks(Unit unit);

        void AddSelfLink(UnitPrecis unit);

        void AddLinksToChildren(Unit unit);

        Link GetSelfLink(long unitId);

        Link GetAllUnitsLink();
    }
}
