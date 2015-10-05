using BeyondEarthApp.Web.Api.Models.InquiryResponses;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IAllStatusesProcessor
    {
        StatusesInquiryResponse GetStatuses();
    }
}
