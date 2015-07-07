using System.Collections.Generic;

namespace BeyondEarthApp.Data.Entities
{
    public class Technology
    {
        private readonly IList<Building> _buildings = new List<Building>();

        private readonly IList<Unit> _units = new List<Unit>(); 

        public virtual long TechnologyId { get; set; }

        public virtual string Name { get; set; }

        public virtual int Cost { get; set; }

        public virtual IList<Building> Buildings
        {
            get { return _buildings; }
        }

        public virtual IList<Unit> Units
        {
            get { return _units; }
        }

        public virtual byte[] Version { get; set; }
    }
}
