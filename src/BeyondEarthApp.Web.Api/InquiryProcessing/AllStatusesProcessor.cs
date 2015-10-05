using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.InquiryResponses;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class AllStatusesProcessor : IAllStatusesProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IStatusLinkService _statusLinkService;
        private readonly IAllStatusesQueryProcessor _queryProcessor;

        public AllStatusesProcessor(
            IAutoMapper autoMapper, 
            IStatusLinkService statusLinkService, 
            IAllStatusesQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _statusLinkService = statusLinkService;
            _queryProcessor = queryProcessor;
        }

        public StatusesInquiryResponse GetStatuses()
        {
            var statusEntities = _queryProcessor.GetStatuses();

            var statuses = GetStatuses(statusEntities);

            var inquiryResponse = new StatusesInquiryResponse
            {
                Statuses = statuses
            };

            var allStatusesLink = _statusLinkService.GetAllStatusesLink();
            inquiryResponse.AddLink(allStatusesLink);

            return inquiryResponse;
        }

        public virtual List<Status> GetStatuses(IEnumerable<Data.Entities.Status> statusEntities)
        {
            var statuses = statusEntities.Select(x => _autoMapper.Map<Status>(x)).ToList();
            return statuses;
        }
    }
}