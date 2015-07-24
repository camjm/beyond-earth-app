using AutoMapper;

namespace BeyondEarthApp.Common.TypeMapping
{
    /// <summary>
    /// Wrapper around static AutoMapper functions
    /// </summary>
    public class AutoMapperAdapter : IAutoMapper
    {
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            return Mapper.CreateMap<TSource, TDestination>();
        }

        public void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
