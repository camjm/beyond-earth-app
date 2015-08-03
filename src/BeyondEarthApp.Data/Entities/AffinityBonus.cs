namespace BeyondEarthApp.Data.Entities
{
    public class AffinityBonus : IVersionedEntity
    {
        public virtual long AffinityBonusId { get; set; }

        public virtual Affinity Affinity { get; set; }

        public virtual short Level { get; set; }

        public virtual string Description { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
