namespace BeyondEarthApp.Data.Entities
{
    public class TechnologyAffinity : IVersionedEntity
    {
        public virtual Technology Technology { get; set; }

        public virtual Affinity Affinity { get; set; }

        public virtual int Amount { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
