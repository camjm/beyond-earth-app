namespace BeyondEarthApp.Data.Entities
{
    public class Building : IVersionedEntity
    {
        public virtual long BuildingId { get; set; }

        public virtual string Name { get; set; }

        public virtual int Cost { get; set; }

        public virtual Technology Technology { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
