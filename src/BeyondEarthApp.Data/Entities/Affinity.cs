using System.Collections.Generic;

namespace BeyondEarthApp.Data.Entities
{
    public class Affinity : IVersionedEntity
    {
        private readonly IList<AffinityBonus> _affinityBonuses = new List<AffinityBonus>(); 

        private readonly IList<TechnologyAffinity> _affinityTechnologies = new List<TechnologyAffinity>(); 

        public virtual long AffinityId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        // getter only to prevent developer from replacing the entire collection
        public virtual IList<AffinityBonus> AffinityBonuses
        {
            get { return _affinityBonuses; }
        }

        // getter only to prevent developer from replacing the entire collection
        public virtual IList<TechnologyAffinity> AffinityTechnologies
        {
            get { return _affinityTechnologies; }
        }

        public virtual byte[] Version { get; set; }
    }
}
