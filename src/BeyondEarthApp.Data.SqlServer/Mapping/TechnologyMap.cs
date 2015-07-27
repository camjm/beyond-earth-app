using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class TechnologyMap : VersionedClassMap<Technology>
    {
        public TechnologyMap()
        {
            Id(x => x.TechnologyId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            HasMany(x => x.Buildings).KeyColumn("TechnologyId");
            HasMany(x => x.Units).KeyColumn("TechnologyId");

            /* TODO: are the following required for the above mappings
             *      .Table("Building")
             *      .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore) - Buildings and Units have no getter, NHibernate will use reflection to access the private field
             *      .Inverse() - probably required, see: http://stackoverflow.com/questions/713637/inverse-attribute-in-nhibernate
             *      .Cascade
             */
        }
    }
}
