using System;

namespace BeyondEarthApp.Web.Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri GetBaseUri(this Uri originalUri)
        {
            //re-write?
            var queryDelimiterIndex = originalUri.AbsoluteUri.IndexOf("?", StringComparison.Ordinal);

            return queryDelimiterIndex < 0
                ? originalUri
                : new Uri(originalUri.AbsoluteUri.Substring(0, queryDelimiterIndex));
        }
    }
}
