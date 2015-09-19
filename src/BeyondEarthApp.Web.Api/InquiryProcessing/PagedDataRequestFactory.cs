using System;
using System.Net;
using System.Net.Http;
using System.Web;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Extensions;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Data;
using log4net;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class PagedDataRequestFactory : IPagedDataRequestFactory
    {
        private readonly ILog _Log;

        public PagedDataRequestFactory(ILogManager logManager)
        {
            _Log = logManager.GetLog(typeof(PagedDataRequestFactory));
        }

        /// <summary>
        /// Constructs a PagedDataRequest based on the request's query string
        /// </summary>
        public PagedDataRequest Create(Uri requestUri)
        {
            int? pageNumber;
            int? pageSize;

            try
            {
                var valueCollection = requestUri.ParseQueryString();

                pageNumber = valueCollection[Constants.CommonParameterNames.PageNumber].Parse<int?>();
                pageSize = valueCollection[Constants.CommonParameterNames.PageSize].Parse<int?>();
            }
            catch (Exception e)
            {
                _Log.Error("Error parsing input", e);

                throw new HttpException((int) HttpStatusCode.BadRequest, e.Message);
            }

            // coerce into reasonable values
            pageNumber = pageNumber.GetBoundedValue(Constants.Paging.DefaultPageNumber, Constants.Paging.MinPageNumber);
            pageSize = pageSize.GetBoundedValue(Constants.Paging.DefaultPageSize, Constants.Paging.MinPageSize, Constants.Paging.MaxPageSize);

            return new PagedDataRequest(pageNumber.Value, pageSize.Value);
        }
    }
}