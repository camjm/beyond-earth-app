using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.LinkServices.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class BuildingLinkService : IBuildingLinkService
    {
        private const string PathFragmentBase = "buildings";

        private readonly ICommonLinkService _commonLinkService;
        private readonly IBuildingPrecisLinkService _buildingPrecisLinkService;
        private readonly ITechnologyPrecisLinkService _technologyPrecisLinkService;

        public BuildingLinkService(
            ICommonLinkService commonLinkService,
            IBuildingPrecisLinkService buildingPrecisLinkService,
            ITechnologyPrecisLinkService technologyPrecisLinkService)
        {
            _commonLinkService = commonLinkService;
            _buildingPrecisLinkService = buildingPrecisLinkService;
            _technologyPrecisLinkService = technologyPrecisLinkService;
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
            var link = _buildingPrecisLinkService.GetAllBuildingsLink();
            building.AddLink(link);
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
        
        public virtual void AddLinksToChildren(Building building)
        {
            _technologyPrecisLinkService.AddSelfLink(building.Technology);
        }
    }
}