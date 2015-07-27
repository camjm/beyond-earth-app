namespace BeyondEarthApp.Data.Entities
{
    public class Faction : IVersionedEntity
    {
        public virtual long FactionId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Leader { get; set; }

        public virtual string Capital { get; set; }

        public virtual string Ability { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
