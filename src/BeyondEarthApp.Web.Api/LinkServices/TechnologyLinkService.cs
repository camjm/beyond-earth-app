using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.LinkServices.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class TechnologyLinkService : ITechnologyLinkService
    {
        private const string PathFragmentBase = "technologies";

        private readonly ICommonLinkService _commonLinkService;
        private readonly IUnitPrecisLinkService _unitLinkService;
        private readonly IBuildingPrecisLinkService _buildingLinkService;
        private readonly ITechnologyPrecisLinkService _technologyLinkService;

        public TechnologyLinkService(
            ICommonLinkService commonLinkService,
            IUnitPrecisLinkService unitLinkService,
            IBuildingPrecisLinkService buildingLinkService,
            ITechnologyPrecisLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _unitLinkService = unitLinkService;
            _buildingLinkService = buildingLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddLinks(Technology technology)
        {
            //AddSelfLink(technology);
            AddAllTechnologiesLink(technology);
            AddTechnologyUnitsLink(technology);
            AddTechnologyBuildingsLink(technology);
            AddLinksToChildren(technology);
        }
        
        public virtual void AddAllTechnologiesLink(Technology technology)
        {
            var link = _technologyLinkService.GetAllTechnologiesLink();
            technology.AddLink(link);
        }

        public virtual void AddTechnologyUnitsLink(Technology technology)
        {
            var pathFragment = string.Format("{0}/{1}/units", PathFragmentBase, technology.TechnologyId);
            var link = _commonLinkService.GetLink(pathFragment, "technologyUnits", HttpMethod.Get);
            technology.AddLink(link);
        }

        public virtual void AddTechnologyBuildingsLink(Technology technology)
        {
            var pathFragment = string.Format("{0}/{1}/buildings", PathFragmentBase, technology.TechnologyId);
            var link = _commonLinkService.GetLink(pathFragment, "technologyBuildings", HttpMethod.Get);
            technology.AddLink(link);
        }

        public void AddLinksToChildren(Technology technology)
        {
            technology.Units.ForEach(x => _unitLinkService.AddSelfLink(x));
            technology.Buildings.ForEach(x => _buildingLinkService.AddSelfLink(x));
        }
    }
}