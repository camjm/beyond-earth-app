using BeyondEarthApp.Data;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Paging;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IAllGamesProcessor
    {
        PagedDataInquiryResponse<Game> GetGames(PagedDataRequest requestInfo);
    }
}
