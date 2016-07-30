using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IBuildingByIdProcessor
    {
        Building GetBuilding(long buildingId);
    }
}
