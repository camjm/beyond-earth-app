using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Api.LinkServices.Precis;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class AllUnitsProcessor : IAllUnitsProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllUnitsQueryProcessor _queryProcessor;
        private readonly IUnitPrecisLinkService _unitLinkService;

        public AllUnitsProcessor(
            IAutoMapper autoMapper,
            ICommonLinkService commonLinkService,
            IAllUnitsQueryProcessor queryProcessor,
            IUnitPrecisLinkService unitLinkService)
        {
            _autoMapper = autoMapper;
            _commonLinkService = commonLinkService;
            _queryProcessor = queryProcessor;
            _unitLinkService = unitLinkService;
        }

        public PagedDataInquiryResponse<UnitPrecis> GetUnits(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetUnits(requestInfo);

            var units = GetUnits(queryResult.QueriedItems).ToList();

            var inquiryResponse = new PagedDataInquiryResponse<UnitPrecis>
            {
                Items = units,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = queryResult.PageSize
            };

            if (!requestInfo.ExcludeLinks)
            {
                // add the relevant hypermedia links
                AddLinksToInquiryResponse(inquiryResponse);
            }

            return inquiryResponse;
        }

        /// <summary>
        /// Adds Links at the Root (page) level, the Unit level, and the Child level (Faction and Technologies)
        /// </summary>
        public virtual void AddLinksToInquiryResponse(PagedDataInquiryResponse<UnitPrecis> inquiryResponse)
        {
            inquiryResponse.AddLink(_unitLinkService.GetAllUnitsLink());

            _commonLinkService.AddPageLinks(inquiryResponse);
        }

        public virtual IEnumerable<UnitPrecis> GetUnits(IEnumerable<Data.Entities.Unit> unitEntities)
        {
            var units = unitEntities.Select(x => _autoMapper.Map<UnitPrecis>(x)).ToList();

            // add the self links and the children self links to the unit service model
            units.ForEach(x =>
            {
                _unitLinkService.AddSelfLink(x);
            });

            return units;
        }
    }
}