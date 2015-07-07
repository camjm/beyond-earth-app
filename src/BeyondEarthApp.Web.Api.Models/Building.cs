namespace BeyondEarthApp.Web.Api.Models
{
    public class Building
    {
        public long BuildingId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public Technology Technology { get; set; }
    }
}
