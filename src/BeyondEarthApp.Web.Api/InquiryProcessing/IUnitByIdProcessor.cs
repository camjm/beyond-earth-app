using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public interface IUnitByIdProcessor
    {
        Unit GetUnit(long unitId);
    }
}
