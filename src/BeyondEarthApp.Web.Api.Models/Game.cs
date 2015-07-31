using System;
using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Game : ILinkContaining
    {
        public long GameId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public Faction Faction { get; set; }

        public List<Technology> Technologies { get; set; }

        #region Serialization

        private bool _serializeTechnologies;

        public void SetSerializeTechnologies(bool serialize)
        {
            _serializeTechnologies = serialize;
        }

        public bool SerializeTechnologies()
        {
            return _serializeTechnologies;
        }

        #endregion

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
