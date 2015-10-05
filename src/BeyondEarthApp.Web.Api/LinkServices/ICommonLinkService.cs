using System;
using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Paging;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    /// <summary>
    /// Provides functionality required by the business domain-specific link services
    /// </summary>
    public interface ICommonLinkService
    {
        Link GetLink(
            string pathFragment,
            string relValue,
            HttpMethod httpMethod);

        Link GetLink(
            Uri uri,
            string relValue,
            HttpMethod httpMethod);

        void AddPageLinks(IPageLinkContaining linkContainer);
    }
}
