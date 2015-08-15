using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class TechnologyByIdProcessor : ITechnologyByIdProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ITechnologyByIdQueryProcessor _technologyByIdQueryProcessor;

        public TechnologyByIdProcessor(
            IAutoMapper autoMapper, 
            ITechnologyByIdQueryProcessor technologyByIdQueryProcessor)
        {
            _autoMapper = autoMapper;
            _technologyByIdQueryProcessor = technologyByIdQueryProcessor;
        }

        public Technology GetTechnology(long technologyId)
        {
            var technologyEntity = _technologyByIdQueryProcessor.GetTechnology(technologyId);

            if (technologyEntity == null)
            {
                throw new RootObjectNotFoundException("Technology not found");
            }

            var technology = _autoMapper.Map<Technology>(technologyEntity);

            return technology;
        }
    }
}