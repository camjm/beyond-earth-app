using System;
using BeyondEarthApp.Data;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    /// <summary>
    /// Creates a filter from a request URI
    /// </summary>
    public interface IPagedDataRequestFactory
    {
        PagedDataRequest Create(Uri requestUri);
    }
}
