using System.Collections.Generic;

namespace BeyondEarthApp.Data.Entities
{
    public class Technology : IVersionedEntity
    {
        private readonly IList<Building> _buildings = new List<Building>();

        private readonly IList<Unit> _units = new List<Unit>(); 

        private readonly IList<TechnologyAffinity> _technologyAffinities = new List<TechnologyAffinity>(); 

        public virtual long TechnologyId { get; set; }

        public virtual string Name { get; set; }

        public virtual int Cost { get; set; }

        // getter only to prevent developer from replacing the entire collection
        public virtual IList<Building> Buildings
        {
            get { return _buildings; }
        }

        // getter only to prevent developer from replacing the entire collection
        public virtual IList<Unit> Units
        {
            get { return _units; }
        }

        // getter only to prevent developer from replacing the entire collection
        public virtual IList<TechnologyAffinity> TechnologyAffinities
        {
            get { return _technologyAffinities; }
        } 

        public virtual byte[] Version { get; set; }
    }
}
