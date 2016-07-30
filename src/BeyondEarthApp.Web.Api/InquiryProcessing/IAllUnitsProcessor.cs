using BeyondEarthApp.Data;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    interface IAllUnitsProcessor
    {
        PagedDataInquiryResponse<UnitPrecis> GetUnits(PagedDataRequest requestInfo);
    }
}
