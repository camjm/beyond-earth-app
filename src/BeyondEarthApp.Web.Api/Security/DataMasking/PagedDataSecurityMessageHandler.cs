using BeyondEarthApp.Web.Api.Models.Paging;

namespace BeyondEarthApp.Web.Api.Security.DataMasking
{
    public abstract class PagedDataSecurityMessageHandler<T> : DataSecurityMessageHandler<PagedDataInquiryResponse<T>>
    {

    }
}