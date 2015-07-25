using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// Encapsulates the logic for setting the HTTP Response Code and Location Header
    /// </summary>
    public class TechnologyCreatedActionResult : IHttpActionResult
    {
        private readonly Technology _createdTechnology;
        private readonly HttpRequestMessage _requestMessage;

        public TechnologyCreatedActionResult(
            Technology createdTechnology, 
            HttpRequestMessage requestMessage)
        {
            _createdTechnology = createdTechnology;
            _requestMessage = requestMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, _createdTechnology);

            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_createdTechnology);

            return responseMessage;
        }
    }
}