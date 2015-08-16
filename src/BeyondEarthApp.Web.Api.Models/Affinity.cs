using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Affinity : ILinkContaining
    {
        public long AffinityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AffinityBonus> AffinityBonuses { get; set; }

        public List<TechnologyAffinity> AffinityTechnologies { get; set; }

        #region Serialization

        private bool _serializeAffinityBonuses;

        public void SetSerializeAffinityBonuses(bool serialize)
        {
            _serializeAffinityBonuses = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeAffinityBonuses()
        {
            return _serializeAffinityBonuses;
        }

        private bool _serializeAffinityTechnologies;

        public void SetSerializeAffinityTechnologies(bool serialize)
        {
            _serializeAffinityTechnologies = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeAffinityTechnologies()
        {
            return _serializeAffinityTechnologies;
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
