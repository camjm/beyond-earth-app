using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Common;
using System.Net.Http;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public class UnitPrecisLinkService : IUnitPrecisLinkService
    {
        private const string PathFragmentBase = "units";

        private readonly ICommonLinkService _commonLinkService;

        public UnitPrecisLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public virtual void AddSelfLink(UnitPrecis unit)
        {
            unit.AddLink(GetSelfLink(unit.UnitId));
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