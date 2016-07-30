using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models.Precis
{
    public class GamePrecis : ILinkContaining
    {
        public long GameId { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public string FactionName { get; set; }

        public string StatusName { get; set; }

        public int TechnologiesCount { get; set; }

        public int CitiesCount { get; set; }

        public short Supremacy { get; set; }

        public short Harmony { get; set; }

        public short Purity { get; set; }

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
