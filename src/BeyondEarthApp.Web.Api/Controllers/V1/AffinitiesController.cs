using System;
using System.Web.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("affinities")]
    public class AffinitiesController : ApiController
    {
        [Route("{affinityId:long}", Name = "GetAffinityRoute")]
        public Affinity GetAffinity(long affinityId)
        {
            throw new NotImplementedException();
        }

    }
}
