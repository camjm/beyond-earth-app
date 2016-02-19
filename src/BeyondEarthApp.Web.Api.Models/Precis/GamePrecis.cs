using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models.Precis
{
    public class GamePrecis : ILinkContaining
    {
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public string Faction { get; set; }

        public Status Status { get; set; }

        public int TechnologyCount { get; set; }

        #region Links

        private List<Link> _links;

        [Editable(false)]
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
