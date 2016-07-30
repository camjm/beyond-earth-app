using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class BuildingLinkService : IBuildingLinkService
    {
        private const string PathFragmentBase = "buildings";

        private readonly ICommonLinkService _commonLinkService;
        private readonly ITechnologyLinkService _technologyLinkService;

        public BuildingLinkService(
            ICommonLinkService commonLinkService,
            ITechnologyLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddLinks(Building building)
        {
            AddAllBuildingsLink(building);
            AddUpdateBuildingLink(building);
            AddDeleteBuildingLink(building);
            AddCreateNewBuildingLink(building);
            AddLinksToChildren(building);
        }

        public virtual void AddAllBuildingsLink(Building building)
        {
            building.AddLink(GetAllBuildingsLink());
        }

        public virtual void AddUpdateBuildingLink(Building building)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, building.BuildingId);
            var link = _commonLinkService.GetLink(pathFragment, "updateBuilding", HttpMethod.Put);
            building.AddLink(link);
        }

        public virtual void AddDeleteBuildingLink(Building building)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, building.BuildingId);
            var link = _commonLinkService.GetLink(pathFragment, "deleteBuilding", HttpMethod.Delete);
            building.AddLink(link);
        }

        public virtual void AddCreateNewBuildingLink(Building building)
        {
            var link = _commonLinkService.GetLink(PathFragmentBase, "createBuilding", HttpMethod.Post);
            building.AddLink(link);
        }

        public virtual void AddSelfLink(BuildingPrecis building)
        {
            building.AddLink(GetSelfLink(building.BuildingId));
        }

        public virtual void AddLinksToChildren(Building building)
        {
            _technologyLinkService.AddSelfLink(building.Technology);
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