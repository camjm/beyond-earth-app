using System;
using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    /// <summary>
    /// Provides functionality required by the business domain-specific link services
    /// </summary>
    public interface ICommonLinkService
    {
        void AddPageLinks(
            IPageLinkContaining linkContainer, 
            string currentPageQueryString, 
            string previousPageQueryString, 
            string nextPageQueryString);

        Link GetLink(
            string pathFragment,
            string relValue,
            HttpMethod httpMethod);

        Link GetLink(
            Uri uri,
            string relValue,
            HttpMethod httpMethod);
    }
}
