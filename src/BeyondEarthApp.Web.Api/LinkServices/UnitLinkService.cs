using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class UnitLinkService : IUnitLinkService
    {
        private const string PathFragmentBase = "units";

        private readonly ICommonLinkService _commonLinkService;

        public UnitLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public void AddSelfLink(Unit unit)
        {
            unit.AddLink(GetSelfLink(unit));
        }

        public virtual Link GetSelfLink(Unit unit)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, unit.UnitId);

            var link = _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);

            return link;
        }
    }
}