using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface ITechnologyByIdProcessor
    {
        Technology GetTechnology(long technologyId);
    }
}
