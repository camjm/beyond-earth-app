using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models.InquiryResponses
{
    public class StatusesInquiryResponse : ILinkContaining
    {
        public List<Status> Statuses { get; set; } 

        #region Links

        private List<Link> _links;

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion
    }
}
