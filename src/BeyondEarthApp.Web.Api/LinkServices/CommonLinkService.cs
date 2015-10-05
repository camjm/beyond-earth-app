using System;
using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Common.Extensions;
using BeyondEarthApp.Web.Common.Security;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class CommonLinkService : ICommonLinkService
    {
        public const string QueryStringFormat = "{0}={1}&{2}={3}";
        private readonly IWebUserSession _userSession;

        public CommonLinkService(IWebUserSession userSession)
        {
            _userSession = userSession;
        }

        /// <summary>
        /// Creates the Link Uri by prepending a versioned base path prefix to the specified path fragment
        /// </summary>
        public virtual Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod)
        {
            // use _userSession.RequestUri.GetBaseUri() instead?
            const string delimitedVersionedApiRouteBaseFormatString =
                Constants.CommonRoutingDefinitions.ApiSegmentName + "/{0}/";

            var path = string.Concat(
                string.Format(
                    delimitedVersionedApiRouteBaseFormatString, 
                    _userSession.ApiVersionInUse),
                pathFragment);

            // construct a properly formed uri to assign to the Href of the link
            var uriBuilder = new UriBuilder
            {
                Scheme = _userSession.RequestUri.Scheme,
                Host = _userSession.RequestUri.Host,
                Port = _userSession.RequestUri.Port,
                Path = path
            };

            return GetLink(uriBuilder.Uri, relValue, httpMethod);
        }

        /// <summary>
        /// Factory method, creating an appropriate Link instance based on the specified Uri
        /// </summary>
        public virtual Link GetLink(Uri uri, string relValue, HttpMethod httpMethod)
        {
            var link = new Link
            {
                Href = uri.AbsoluteUri,
                Rel = relValue,
                Method = httpMethod.Method
            };

            return link;
        }

        /// <summary>
        /// Adds paging Links to a paged response
        /// </summary>
        public void AddPageLinks(IPageLinkContaining linkContainer)
        {
            var versionedBaseUri = _userSession.RequestUri.GetBaseUri();

            AddCurrentPageLink(linkContainer, versionedBaseUri);

            var addPreviousPageLink = ShouldAddPreviousPageLink(linkContainer.PageNumber);
            if (addPreviousPageLink)
            {
                AddPreviousPageLink(linkContainer, versionedBaseUri);
            }

            var addNextPageLink = ShouldAddNextPageLink(linkContainer.PageNumber, linkContainer.PageCount);
            if (addNextPageLink)
            {
                AddNextPageLink(linkContainer, versionedBaseUri);
            }
        }

        /// <summary>
        /// Builds the proper Uri and ands the Link to the paged response
        /// </summary>
        public virtual void AddCurrentPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri)
        {
            var currentPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = GetQueryString(linkContainer.PageNumber, linkContainer.PageSize)
            };

            var currentPageLink = GetLink(
                currentPageUriBuilder.Uri, 
                Constants.CommonLinkRelValues.CurrentPage, 
                HttpMethod.Get);
            linkContainer.AddLink(currentPageLink);
        }

        /// <summary>
        /// Builds the proper Uri and ands the Link to the paged response
        /// </summary>
        public virtual void AddPreviousPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri)
        {
            var previousPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = GetQueryString(linkContainer.PageNumber - 1, linkContainer.PageSize)
            };

            var previousPageLink = GetLink(
                previousPageUriBuilder.Uri,
                Constants.CommonLinkRelValues.PreviousPage,
                HttpMethod.Get);
            linkContainer.AddLink(previousPageLink);
        }

        /// <summary>
        /// Builds the proper Uri and ands the Link to the paged response
        /// </summary>
        public virtual void AddNextPageLink(IPageLinkContaining linkContainer, Uri versionedBaseUri)
        {
            var nextPageUriBuilder = new UriBuilder(versionedBaseUri)
            {
                Query = GetQueryString(linkContainer.PageNumber + 1, linkContainer.PageSize)
            };

            var nextPageLink = GetLink(
                nextPageUriBuilder.Uri,
                Constants.CommonLinkRelValues.NextPage,
                HttpMethod.Get);
            linkContainer.AddLink(nextPageLink);
        }

        public bool ShouldAddPreviousPageLink(int pageNumber)
        {
            return pageNumber > 1;
        }

        public bool ShouldAddNextPageLink(int pageNumber, int pageCount)
        {
            return pageNumber < pageCount;
        }

        public string GetQueryString(int pageNumber, int pageSize)
        {
            return string.Format(
                QueryStringFormat,
                Constants.CommonParameterNames.PageNumber,
                pageNumber,
                Constants.CommonParameterNames.PageSize,
                pageSize);
        }
    }
}