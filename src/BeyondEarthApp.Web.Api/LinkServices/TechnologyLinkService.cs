using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

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

        public void AddSelfLink(Technology technology)
        {
            technology.AddLink(GetSelfLink(technology));
        }

        public virtual Link GetSelfLink(Technology technology)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, technology.TechnologyId);

            var link = _commonLinkService.GetLink(
                pathFragment, 
                Constants.CommonLinkRelValues.Self, 
                HttpMethod.Get);

            return link;
        }

        public void AddLinksToChildren(Technology technology)
        {
            technology.Units.ForEach(x => _unitLinkService.AddSelfLink(x));
            technology.Buildings.ForEach(x => _buildingLinkService.AddSelfLink(x));
        }

        public Link GetAllTechnologiesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}