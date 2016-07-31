using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.LinkServices.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class UnitLinkService : IUnitLinkService
    {
        private const string PathFragmentBase = "units";

        private readonly ICommonLinkService _commonLinkService;
        private readonly IUnitPrecisLinkService _unitPrecisLinkService;
        private readonly ITechnologyPrecisLinkService _technologyLinkService;

        public UnitLinkService(
            ICommonLinkService commonLinkService,
            IUnitPrecisLinkService unitPrecisLinkService,
            ITechnologyPrecisLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _unitPrecisLinkService = unitPrecisLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddLinks(Unit unit)
        {
            AddAllUnitsLink(unit);
            AddUpdateUnitLink(unit);
            AddDeleteUnitLink(unit);
            AddCreateNewUnitLink(unit);
            AddLinksToChildren(unit);
        }

        public virtual void AddAllUnitsLink(Unit unit)
        {
            var link = _unitPrecisLinkService.GetAllUnitsLink();
            unit.AddLink(link);
        }

        public virtual void AddUpdateUnitLink(Unit unit)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, unit.UnitId);
            var link = _commonLinkService.GetLink(pathFragment, "updateUnit", HttpMethod.Put);
            unit.AddLink(link);
        }

        public virtual void AddDeleteUnitLink(Unit unit)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, unit.UnitId);
            var link = _commonLinkService.GetLink(pathFragment, "deleteUnit", HttpMethod.Delete);
            unit.AddLink(link);
        }

        public virtual void AddCreateNewUnitLink(Unit unit)
        {
            var link = _commonLinkService.GetLink(PathFragmentBase, "createUnit", HttpMethod.Post);
            unit.AddLink(link);
        }
        
        public virtual void AddLinksToChildren(Unit unit)
        {
            _technologyLinkService.AddSelfLink(unit.Technology);
        }
    }
}