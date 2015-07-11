using BeyondEarthApp.Data.Entities;
using FluentNHibernate.Mapping;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    /// <summary>
    /// Protect against trying to update dirty records.
    /// </summary>
    public abstract class VersionedClassMap<T> : ClassMap<T> where T : IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version)             // use the 'Version' property as a concurrency/version value
                .Column("ts")                   // the column supporting versioning is 'ts'
                .CustomSqlType("Rowversion")    // with a SQL datatype of 'Rowversion'
                .Generated.Always()             // NHibernate should let the database generate the value
                .UnsavedValue("null");          // prior to a database save, the in-memory value of 'Version' will be null
        }
    }
}
