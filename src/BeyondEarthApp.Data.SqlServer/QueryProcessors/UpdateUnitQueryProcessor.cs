using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;
using System.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class UpdateUnitQueryProcessor : IUpdateUnitQueryProcessor
    {
        private readonly ISession _session;

        public UpdateUnitQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Unit GetUpdatedUnit(long unitId, PropertyValueMapType updatedPropertyValueMap)
        {
            var unit = GetValidUnit(unitId);

            var propertyInfos = typeof(Game).GetProperties();

            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                propertyInfos
                    .Single(x => x.Name == propertyValuePair.Key)
                    .SetValue(unit, propertyValuePair.Value);
            }

            _session.SaveOrUpdate(unit);

            return unit;
        }

        public Unit DeleteUnitTechnology(long unitId)
        {
            var unit = GetValidUnit(unitId);

            UpdateUnitTechnology(unit, null);

            _session.SaveOrUpdate(unit);

            return unit;
        }
        
        public Unit ReplaceUnitTechnology(long unitId, long technologyId)
        {
            var unit = GetValidUnit(unitId);

            UpdateUnitTechnology(unit, technologyId);

            _session.SaveOrUpdate(unit);

            return unit;
        }
        
        public virtual Unit GetValidUnit(long unitId)
        {
            var unit = _session.Get<Unit>(unitId);

            if (unit == null)
            {
                throw new RootObjectNotFoundException("Unit not found");
            }

            return unit;
        }

        public virtual Technology GetValidTechnology(long technologyId)
        {
            var technology = _session.Get<Technology>(technologyId);

            if (technology == null)
            {
                throw new ChildObjectNotFoundException("Technology not found");
            }

            return technology;
        }
        
        public virtual void UpdateUnitTechnology(Unit unit, long? technologyId)
        {
            unit.Technology = null;

            if (technologyId != null)
            {
                var technology = GetValidTechnology(technologyId.Value);
                unit.Technology = technology;
            }
        }
    }
}
