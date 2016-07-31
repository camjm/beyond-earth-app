using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Common;
using System.Net.Http;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public class BuildingPrecisLinkService : IBuildingPrecisLinkService
    {
        private const string PathFragmentBase = "buildings";

        private readonly ICommonLinkService _commonLinkService;

        public BuildingPrecisLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public virtual void AddSelfLink(BuildingPrecis building)
        {
            building.AddLink(GetSelfLink(building.BuildingId));
        }

        public virtual Link GetSelfLink(long buildingId)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, buildingId);

            var link = _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);

            return link;
        }

        public virtual Link GetAllBuildingsLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}