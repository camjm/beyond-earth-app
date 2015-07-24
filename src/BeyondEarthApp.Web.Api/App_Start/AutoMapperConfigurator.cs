using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;

namespace BeyondEarthApp.Web.Api
{
    public class AutoMapperConfigurator
    {
        public void Configure(IAutoMapper mapper, IEnumerable<IAutoMapperTypeConfigurator> configurators)
        {
            configurators.ToList().ForEach(x => x.Configure(mapper));

            mapper.AssertConfigurationIsValid();
        }
    }
}