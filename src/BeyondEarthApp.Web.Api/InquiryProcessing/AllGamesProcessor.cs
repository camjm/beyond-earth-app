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

            // add the relevant hypermedia links
            AddLinksToInquiryResponse(inquiryResponse);

            return inquiryResponse;
        }
        /// <summary>
        /// Adds Links at the Root (page) level, the Game level, and the Child level (Faction and Technologies)
        /// </summary>
        public virtual void AddLinksToInquiryResponse(PagedDataInquiryResponse<Game> inquiryResponse)
        {
            inquiryResponse.AddLink(_gameLinkService.GetAllGamesLink());

            _commonLinkService.AddPageLinks(inquiryResponse);
        }

        public virtual IEnumerable<Game> GetGames(IEnumerable<Data.Entities.Game> gameEntities)
        {
            var games = gameEntities.Select(x => _autoMapper.Map<Game>(x)).ToList();

            // add the self links and the children self links to the game service model
            games.ForEach(x =>
            {
                _gameLinkService.AddSelfLink(x);
                _gameLinkService.AddLinksToChildren(x);
            });

            return games;
        }
    }
}