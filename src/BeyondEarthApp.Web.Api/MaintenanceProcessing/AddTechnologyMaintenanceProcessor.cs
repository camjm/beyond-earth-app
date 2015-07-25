using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class AddTechnologyMaintenanceProcessor : IAddTechnologyMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAddTechnologyQueryProcessor _queryProcessor;

        public AddTechnologyMaintenanceProcessor(
            IAutoMapper autoMapper, 
            IAddTechnologyQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
        }

        public Technology AddTechnology(NewTechnology newTechnology)
        {
            // Map service model to entity model
            var technologyEntity = _autoMapper.Map<Data.Entities.Technology>(newTechnology);

            // Persist entity model
            _queryProcessor.AddTechnology(technologyEntity);

            // Map new entity model back to full service model
            var technology = _autoMapper.Map<Technology>(technologyEntity);

            //TODO: implement Link Service
            technology.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost:52204/api/v1/technologies/" + technology.TechnologyId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return technology;
        }
    }
}