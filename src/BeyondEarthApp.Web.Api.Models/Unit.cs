namespace BeyondEarthApp.Web.Api.Models
{
    public class Unit
    {
        public long UnitId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public short Strength { get; set; }

        public Technology Technology { get; set; }
    }
}
