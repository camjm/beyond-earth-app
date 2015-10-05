using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models.InquiryResponses
{
    public class GameTechnologiesInquiryResponse : ILinkContaining
    {
        public long GameId { get; set; }

        public List<Technology> Technologies { get; set; }

        public bool ShouldSerializeGameId()
        {
            return false;
        }

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
