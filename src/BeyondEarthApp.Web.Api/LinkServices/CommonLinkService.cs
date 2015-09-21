using System;
using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common.Extensions;
using BeyondEarthApp.Web.Common.Security;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class CommonLinkService : ICommonLinkService
    {
        private readonly IWebUserSession _userSession;

        public CommonLinkService(IWebUserSession userSession)
        {
            _userSession = userSession;
        }

        public Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod)
        {
            // use _userSession.RequestUri.GetBaseUri() instead?
            const string delimitedVersionedApiRouteBaseFormatString =
                Constants.CommonRoutingDefinitions.ApiSegmentName + "/{0}/";

            var path = string.Concat(
                string.Format(delimitedVersionedApiRouteBaseFormatString, _userSession.ApiVersionInUse),
                pathFragment);

            var uriBuilder = new UriBuilder
            {
                Scheme = _userSession.RequestUri.Scheme,
                Host = _userSession.RequestUri.Host,
                Port = _userSession.RequestUri.Port,
                Path = path
            };

            var link = new Link
            {
                Href = uriBuilder.Uri.AbsoluteUri,
                Rel = relValue,
                Method = httpMethod.Method
            };

            return link;
        }

        public void AddPageLinks(
            IPageLinkContaining linkContainer, 
            string currentPageQueryString, 
            string previousPageQueryString,
            string nextPageQueryString)
        {
            var versionedBaseUri = _userSession.RequestUri.GetBaseUri();

            AddCurrentPageLink(linkContainer, versionedBaseUri, currentPageQueryString);

            var addPreviousPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            if (addPreviousPageLink)
            {
                AddPreviousPageLink(linkContainer, versionedBaseUri, previousPageQueryString);
            }

            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);
            if (addNextPageLink)
            {
                AddNextPageLink(linkContainer, versionedBaseUri, nextPageQueryString);
            }
        }

        public virtual void AddCurrentPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var currentPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var currentPageLink = GetCurrentPageLink(currentPageUriBuilder.Uri);
            linkContainer.AddLink(currentPageLink);
        }

        public virtual void AddPreviousPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var previousPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var previousPageLink = GetPreviousPageLink(previousPageUriBuilder.Uri);
            linkContainer.AddLink(previousPageLink);
        }

        public virtual void AddNextPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri, string pageQueryString)
        {
            var nextPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = pageQueryString
            };

            var nextPageLink = GetNextPageLink(nextPageUriBuilder.Uri);
            linkContainer.AddLink(nextPageLink);
        }

        public virtual Link GetCurrentPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.CurrentPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetPreviousPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.PreviousPage,
                Method = HttpMethod.Get.Method
            };
        }

        public virtual Link GetNextPageLink(Uri uri)
        {
            return new Link
            {
                Href = uri.AbsoluteUri,
                Rel = Constants.CommonLinkRelValues.NextPage,
                Method = HttpMethod.Get.Method
            };
        }

        public bool ShouldAddPreviousPageLink(int pageNumber)
        {
            return pageNumber > 1;
        }

        public bool ShouldAddNextPageLink(int pageNumber, int pageCount)
        {
            return pageNumber < pageCount;
        }
    }
}