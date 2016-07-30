using BeyondEarthApp.Data;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IAllGamesProcessor
    {
        PagedDataInquiryResponse<GamePrecis> GetGames(PagedDataRequest requestInfo);
    }
}
