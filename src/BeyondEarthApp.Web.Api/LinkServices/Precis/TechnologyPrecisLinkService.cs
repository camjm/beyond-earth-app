using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Common;
using System.Net.Http;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public class TechnologyPrecisLinkService : ITechnologyPrecisLinkService
    {
        private const string PathFragmentBase = "technologies";

        private readonly ICommonLinkService _commonLinkService;

        public TechnologyPrecisLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public virtual void AddSelfLink(TechnologyPrecis technology)
        {
            technology.AddLink(GetSelfLink(technology.TechnologyId));
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