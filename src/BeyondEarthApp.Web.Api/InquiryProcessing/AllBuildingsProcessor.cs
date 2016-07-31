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
    public class AllBuildingsProcessor : IAllBuildingsProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllBuildingsQueryProcessor _queryProcessor;
        private readonly IBuildingPrecisLinkService _buildingLinkService;

        public AllBuildingsProcessor(
            IAutoMapper autoMapper,
            ICommonLinkService commonLinkService,
            IAllBuildingsQueryProcessor queryProcessor,
            IBuildingPrecisLinkService buildingLinkService)
        {
            _autoMapper = autoMapper;
            _commonLinkService = commonLinkService;
            _queryProcessor = queryProcessor;
            _buildingLinkService = buildingLinkService;
        }

        public PagedDataInquiryResponse<BuildingPrecis> GetBuildings(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetBuildings(requestInfo);

            var buildings = GetBuildings(queryResult.QueriedItems).ToList();

            var inquiryResponse = new PagedDataInquiryResponse<BuildingPrecis>
            {
                Items = buildings,
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
        /// Adds Links at the Root (page) level, the Building level, and the Child level (Faction and Technologies)
        /// </summary>
        public virtual void AddLinksToInquiryResponse(PagedDataInquiryResponse<BuildingPrecis> inquiryResponse)
        {
            inquiryResponse.AddLink(_buildingLinkService.GetAllBuildingsLink());

            _commonLinkService.AddPageLinks(inquiryResponse);
        }

        public virtual IEnumerable<BuildingPrecis> GetBuildings(IEnumerable<Data.Entities.Building> buildingEntities)
        {
            var buildings = buildingEntities.Select(x => _autoMapper.Map<BuildingPrecis>(x)).ToList();

            // add the self links and the children self links to the building service model
            buildings.ForEach(x =>
            {
                _buildingLinkService.AddSelfLink(x);
            });

            return buildings;
        }
    }
}