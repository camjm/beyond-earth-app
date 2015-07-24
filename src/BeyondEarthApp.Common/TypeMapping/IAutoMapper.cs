using AutoMapper;

namespace BeyondEarthApp.Common.TypeMapping
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);

        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();

        void AssertConfigurationIsValid();
    }
}
