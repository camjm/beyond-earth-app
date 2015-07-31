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
    public class CreatedActionResult<T> : IHttpActionResult where T : ILinkContaining
    {
        private readonly T _model;
        private readonly HttpRequestMessage _requestMessage;

        public CreatedActionResult(T model, HttpRequestMessage requestMessage)
        {
            _model = model;
            _requestMessage = requestMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, _model);

            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_model);

            return responseMessage;
        }
    }
}