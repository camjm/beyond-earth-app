using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BeyondEarthApp.Web.Api.Security.DataMasking
{
    public abstract class DataSecurityMessageHandler<T> : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // passes request down chain first
            var response = await base.SendAsync(request, cancellationToken);

            if (CanHandleResponse(response))
            {
                var objectContent = (ObjectContent) response.Content;
                ApplySecurityToResponseData((T)objectContent.Value, objectContent);
            }

            return response;
        }

        /// <summary>
        /// Response contains the generic service model object
        /// </summary>
        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;

            var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof(T);

            return canHandleResponse;
        }

        public abstract void ApplySecurityToResponseData(T dataObject, ObjectContent responseObjectContent);
    }
}