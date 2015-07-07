using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Technology
    {
        public long TechnologyId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public List<Building> Buildings { get; set; }

        public List<Unit> Units { get; set; }

        #region Serialization

        private bool _serializeBuildingsAndUnits;

        public void SetSerializeBuildingsAndUnits(bool serialize)
        {
            _serializeBuildingsAndUnits = serialize;
        }

        public bool SerializeBuildingsAndUnits()
        {
            return _serializeBuildingsAndUnits;
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
