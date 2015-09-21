using BeyondEarthApp.Data;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IAllGamesProcessor
    {
        PagedDataInquiryResponse<Game> GetGames(PagedDataRequest requestInfo);
    }
}
