using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models
{
    /// <summary>
    /// Editable Attribute used by the Updateable Property Detector
    /// </summary>
    public class Game : ILinkContaining
    {
        [Key]
        public long? GameId { get; set; }

        [Editable(true)]
        public string Description { get; set; }

        [Editable(false)]
        public DateTime CreatedDate { get; set; }

        [Editable(false)]
        public DateTime? StartDate { get; set; }

        [Editable(false)]
        public Status Status { get; set; }

        [Editable(false)]
        public Faction Faction { get; set; }

        [Editable(false)] // modify Technologies via the relationship API
        public List<Technology> Technologies { get; set; }

        #region Serialization

        private bool _serializeTechnologies;

        public void SetSerializeTechnologies(bool serialize)
        {
            _serializeTechnologies = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeTechnologies()
        {
            return _serializeTechnologies;
        }

        #endregion

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
