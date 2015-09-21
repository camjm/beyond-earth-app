using System.Net.Http;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
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
    }
}
