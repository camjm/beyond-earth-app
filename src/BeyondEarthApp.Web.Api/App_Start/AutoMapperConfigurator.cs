using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;

namespace BeyondEarthApp.Web.Api
{
    /// <summary>
    /// Run each AutoMapper configurator in the application start-up
    /// </summary>
    public class AutoMapperConfigurator
    {
        public void Configure(IAutoMapper mapper, IEnumerable<IAutoMapperTypeConfigurator> configurators)
        {
            configurators.ToList().ForEach(x => x.Configure(mapper));

            mapper.AssertConfigurationIsValid();
        }
    }
}