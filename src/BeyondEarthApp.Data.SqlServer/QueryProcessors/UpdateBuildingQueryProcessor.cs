using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;
using System.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class UpdateBuildingQueryProcessor : IUpdateBuildingQueryProcessor
    {
        private readonly ISession _session;

        public UpdateBuildingQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Building GetUpdatedBuilding(long buildingId, PropertyValueMapType updatedPropertyValueMap)
        {
            var building = GetValidBuilding(buildingId);

            var propertyInfos = typeof(Game).GetProperties();

            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                propertyInfos
                    .Single(x => x.Name == propertyValuePair.Key)
                    .SetValue(building, propertyValuePair.Value);
            }

            _session.SaveOrUpdate(building);

            return building;
        }

        public Building DeleteBuildingTechnology(long buildingId)
        {
            var building = GetValidBuilding(buildingId);

            UpdateBuildingTechnology(building, null);

            _session.SaveOrUpdate(building);

            return building;
        }

        public Building ReplaceBuildingTechnology(long buildingId, long technologyId)
        {
            var building = GetValidBuilding(buildingId);

            UpdateBuildingTechnology(building, technologyId);

            _session.SaveOrUpdate(building);

            return building;
        }

        public virtual Building GetValidBuilding(long buildingId)
        {
            var building = _session.Get<Building>(buildingId);

            if (building == null)
            {
                throw new RootObjectNotFoundException("Building not found");
            }

            return building;
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

        public virtual void UpdateBuildingTechnology(Building building, long? technologyId)
        {
            building.Technology = null;

            if (technologyId != null)
            {
                var technology = GetValidTechnology(technologyId.Value);
                building.Technology = technology;
            }
        }
    }
}
