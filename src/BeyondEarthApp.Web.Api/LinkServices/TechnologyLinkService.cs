using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class TechnologyLinkService : ITechnologyLinkService
    {
        private const string PathFragmentBase = "technologies";

        private readonly ICommonLinkService _commonLinkService;
        private readonly IBuildingLinkService _buildingLinkService;
        private readonly IUnitLinkService _unitLinkService;

        public TechnologyLinkService(
            ICommonLinkService commonLinkService, 
            IBuildingLinkService buildingLinkService, 
            IUnitLinkService unitLinkService)
        {
            _commonLinkService = commonLinkService;
            _buildingLinkService = buildingLinkService;
            _unitLinkService = unitLinkService;
        }

        public void AddLinks(Technology technology)
        {
            //AddSelfLink(technology);
            AddAllTechnologiesLink(technology);
            AddTechnologyUnitsLink(technology);
            AddTechnologyBuildingsLink(technology);
            AddLinksToChildren(technology);
        }

        public virtual void AddSelfLink(TechnologyPrecis technology)
        {
            technology.AddLink(GetSelfLink(technology.TechnologyId));
        }

        public virtual void AddAllTechnologiesLink(Technology technology)
        {
            technology.AddLink(GetAllTechnologiesLink());
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

        public virtual Link GetSelfLink(long technologyId)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, technologyId);
            return _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);
        }

        public virtual Link GetAllTechnologiesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}