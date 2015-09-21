using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class BuildingLinkService : IBuildingLinkService
    {
        private const string PathFragmentBase = "buildings";

        private readonly ICommonLinkService _commonLinkService;

        public BuildingLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public void AddSelfLink(Building building)
        {
            building.AddLink(GetSelfLink(building));
        }

        public virtual Link GetSelfLink(Building building)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, building.BuildingId);

            var link = _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);

            return link;
        }
    }
}