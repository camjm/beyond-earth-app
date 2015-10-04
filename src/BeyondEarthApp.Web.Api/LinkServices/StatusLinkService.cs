using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class StatusLinkService : IStatusLinkService
    {
        private const string PathFragmentBase = "statuses";

        private readonly ICommonLinkService _commonLinkService;

        public StatusLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public Link GetAllStatusesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                "statuses",
                HttpMethod.Get);
        }
    }
}