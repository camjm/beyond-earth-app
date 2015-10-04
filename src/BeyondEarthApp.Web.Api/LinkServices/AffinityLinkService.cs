using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class AffinityLinkService : IAffinityLinkService
    {
        private const string PathFragmentBase = "affinities";

        private readonly ICommonLinkService _commonLinkService;
        private readonly ITechnologyLinkService _technologyLinkService;

        public AffinityLinkService(
            ICommonLinkService commonLinkService, 
            ITechnologyLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddLinks(Affinity affinity)
        {
            AddSelfLink(affinity);
            AddAllAffinitiesLink(affinity);
            AddAffinityTechnologiesLink(affinity);
            AddLinksToChildren(affinity);
        }

        public virtual void AddSelfLink(Affinity affinity)
        {
            affinity.AddLink(GetSelfLink(affinity.AffinityId));
        }

        public virtual void AddAllAffinitiesLink(Affinity affinity)
        {
            affinity.AddLink(GetAllAffinitiesLink());
        }

        public virtual void AddAffinityTechnologiesLink(Affinity affinity)
        {
            var pathFragment = string.Format("{0}/{1}/technologies", PathFragmentBase, affinity.AffinityId);
            var link = _commonLinkService.GetLink(pathFragment, "affinityTechnologies", HttpMethod.Get);
            affinity.AddLink(link);
        }

        public void AddLinksToChildren(Affinity affinity)
        {
            affinity.AffinityTechnologies.ForEach(x => _technologyLinkService.AddSelfLink(x.Technology));
        }

        public virtual Link GetSelfLink(long affinityId)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, affinityId);
            return _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);
        }

        public virtual Link GetAllAffinitiesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}