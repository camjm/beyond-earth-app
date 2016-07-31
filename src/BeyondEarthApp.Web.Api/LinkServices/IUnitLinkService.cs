using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IUnitLinkService
    {
        void AddLinks(Unit unit);

        void AddLinksToChildren(Unit unit);
    }
}
