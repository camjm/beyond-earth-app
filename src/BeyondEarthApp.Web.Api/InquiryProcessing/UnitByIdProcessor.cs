using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class UnitByIdProcessor : IUnitByIdProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUnitLinkService _unitLinkService;
        private readonly IUnitByIdQueryProcessor _unitByIdQueryProcessor;

        public UnitByIdProcessor(
            IAutoMapper autoMapper,
            IUnitLinkService unitLinkService,
            IUnitByIdQueryProcessor unitByIdQueryProcessor)
        {
            _autoMapper = autoMapper;
            _unitLinkService = unitLinkService;
            _unitByIdQueryProcessor = unitByIdQueryProcessor;
        }

        public Unit GetUnit(long unitId)
        {
            var unitEntity = _unitByIdQueryProcessor.GetUnit(unitId);

            if (unitEntity == null)
            {
                throw new RootObjectNotFoundException("Unit not found");
            }

            var unit = _autoMapper.Map<Unit>(unitEntity);

            _unitLinkService.AddLinks(unit);

            return unit;
        }
    }
}