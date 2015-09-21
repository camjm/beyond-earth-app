using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    /// <summary>
    /// Maps the game entities from the Query Processor to the service model and packages the result into PagedDataInquiryResponse.
    /// </summary>
    public class AllGamesProcessor : IAllGamesProcessor
    {
        public const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllGamesQueryProcessor _queryProcessor;
        private readonly IGameLinkService _gameLinkService;

        public AllGamesProcessor(
            IAutoMapper autoMapper, 
            ICommonLinkService commonLinkService, 
            IAllGamesQueryProcessor queryProcessor, 
            IGameLinkService gameLinkService)
        {
            _autoMapper = autoMapper;
            _commonLinkService = commonLinkService;
            _queryProcessor = queryProcessor;
            _gameLinkService = gameLinkService;
        }

        public PagedDataInquiryResponse<Game> GetGames(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetGames(requestInfo);

            var games = GetGames(queryResult.QueriedItems).ToList();

            var inquiryResponse = new PagedDataInquiryResponse<Game>
            {
                Items = games,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = queryResult.PageSize
            };

            AddLinksToInquiryResponse(inquiryResponse);

            return inquiryResponse;
        }

        public virtual void AddLinksToInquiryResponse(PagedDataInquiryResponse<Game> inquiryResponse)
        {
            inquiryResponse.AddLink(_gameLinkService.GetAllGamesLink());

            _commonLinkService.AddPageLinks(
                inquiryResponse, 
                GetCurrentPageQueryString(inquiryResponse), 
                GetPreviousPageQueryString(inquiryResponse), 
                GetNextPageQueryString(inquiryResponse));
        }

        public virtual IEnumerable<Game> GetGames(IEnumerable<Data.Entities.Game> gameEntities)
        {
            var games = gameEntities.Select(x => _autoMapper.Map<Game>(x)).ToList();

            games.ForEach(x =>
            {
                _gameLinkService.AddSelfLink(x);
                _gameLinkService.AddLinksToChildren(x);
            });

            return games;
        }

        public virtual string GetCurrentPageQueryString(PagedDataInquiryResponse<Game> inquiryResponse)
        {
            return string.Format(
                QueryStringFormat, 
                inquiryResponse.PageNumber, 
                inquiryResponse.PageSize);
        }

        public virtual string GetPreviousPageQueryString(PagedDataInquiryResponse<Game> inquiryResponse)
        {
            return string.Format(
                QueryStringFormat, 
                inquiryResponse.PageNumber - 1, 
                inquiryResponse.PageSize);
        }

        public virtual string GetNextPageQueryString(PagedDataInquiryResponse<Game> inquiryResponse)
        {
            return string.Format(
                QueryStringFormat, 
                inquiryResponse.PageNumber + 1, 
                inquiryResponse.PageSize);
        }
    }
}