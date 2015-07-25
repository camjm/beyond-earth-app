using System;
using System.Linq;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// Utility class that returns the Location url from the service model's collection of Links
    /// </summary>
    public static class LocationLinkCalculator
    {
        public static Uri GetLocationLink(ILinkContaining linkContaining)
        {
            var locationLink = linkContaining.Links.FirstOrDefault(x => x.Rel == Constants.CommonLinkRelValues.Self);
            return locationLink != null 
                ? new Uri(locationLink.Href) 
                : null;
        }
    }
}