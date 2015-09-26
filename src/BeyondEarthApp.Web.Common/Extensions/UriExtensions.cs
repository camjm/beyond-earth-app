using System;

namespace BeyondEarthApp.Web.Common.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Returns the part of the Uri to the left of the query string delimiter
        /// </summary>
        public static Uri GetBaseUri(this Uri originalUri)
        {
            //re-write?
            var queryDelimiterIndex = originalUri.AbsoluteUri.IndexOf("?", StringComparison.Ordinal);

            return queryDelimiterIndex < 0
                ? originalUri
                : new Uri(originalUri.AbsoluteUri.Substring(0, queryDelimiterIndex));
        }

        /// <summary>
        /// Returns the part of the Uri to the right of the query string delimiter
        /// </summary>
        public static string QueryWithoutLeadingQuestionMark(this Uri uri)
        {
            const int indexToSkipQueryDelimiter = 1;

            return uri.Query.Length > 1
                ? uri.Query.Substring(indexToSkipQueryDelimiter)
                : string.Empty;
        }
    }
}
