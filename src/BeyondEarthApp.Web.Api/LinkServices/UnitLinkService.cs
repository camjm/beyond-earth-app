using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class UnitLinkService : IUnitLinkService
    {
        private const string PathFragmentBase = "units";

        private readonly ICommonLinkService _commonLinkService;
        private readonly ITechnologyLinkService _technologyLinkService;

        public UnitLinkService(
            ICommonLinkService commonLinkService,
            ITechnologyLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
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
            unit.AddLink(GetAllUnitsLink());
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

        public virtual void AddSelfLink(UnitPrecis unit)
        {
            unit.AddLink(GetSelfLink(unit.UnitId));
        }

        public virtual void AddLinksToChildren(Unit unit)
        {
            _technologyLinkService.AddSelfLink(unit.Technology);
        }
        
        public virtual Link GetSelfLink(long unitId)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, unitId);

            var link = _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);

            return link;
        }

        public virtual Link GetAllUnitsLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}