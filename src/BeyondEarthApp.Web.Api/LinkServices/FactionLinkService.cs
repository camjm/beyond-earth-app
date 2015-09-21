using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class FactionLinkService : IFactionLinkService
    {
        private const string PathFragmentBase = "factions";

        private readonly ICommonLinkService _commonLinkService;

        public FactionLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public void AddSelfLink(Faction faction)
        {
            faction.AddLink(GetSelfLink(faction));
        }

        public virtual Link GetSelfLink(Faction faction)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, faction.FactionId);

            var link = _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);

            return link;
        }
    }
}