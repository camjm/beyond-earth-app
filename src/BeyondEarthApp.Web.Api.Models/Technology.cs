using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Technology : ILinkContaining
    {
        public long TechnologyId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public List<Building> Buildings { get; set; }

        public List<Unit> Units { get; set; }

        public List<TechnologyAffinity> TechnologyAffinities { get; set; }

        #region Serialization

        private bool _serializeBuildings;

        public void SetSerializeBuildings(bool serialize)
        {
            _serializeBuildings = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeBuildings()
        {
            return _serializeBuildings;
        }

        private bool _serializeUnits;

        public void SetSerializeUnits(bool serialize)
        {
            _serializeUnits = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeUnits()
        {
            return _serializeUnits;
        }

        private bool _serializeTechnologyAffinities;

        public void SetSerializeTechnologyAffinities(bool serialize)
        {
            _serializeTechnologyAffinities = serialize;
        }

        // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to detirmine if specific public properties should be serialized
        public bool ShouldSerializeTechnologyAffinities()
        {
            return _serializeTechnologyAffinities;
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
