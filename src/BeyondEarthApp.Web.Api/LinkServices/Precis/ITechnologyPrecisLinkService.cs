using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public interface ITechnologyPrecisLinkService
    {
        void AddSelfLink(TechnologyPrecis technology);

        Link GetAllTechnologiesLink();
    }
}
