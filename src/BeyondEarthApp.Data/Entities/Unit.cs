namespace BeyondEarthApp.Data.Entities
{
    public class Unit
    {
        public virtual int UnitId { get; set; }

        public virtual string Name { get; set; }

        public virtual int Cost { get; set; }

        public virtual short Strength { get; set; }

        public virtual Technology Technology { get; set; }

        public virtual byte[] Version { get; set; }
    }
}
