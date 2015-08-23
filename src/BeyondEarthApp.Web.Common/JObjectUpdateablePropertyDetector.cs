using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BeyondEarthApp.Web.Common
{
    /// <summary>
    /// JSON specific implementation. To handle other formats (e.g. xml) examine the Content-Type header of the request message.
    /// </summary>
    public class JObjectUpdateablePropertyDetector : IUpdateablePropertyDetector
    {
        public IEnumerable<string> GetNamesOfPropertiesToUpdate<TTarget>(object objectContainingUpdatedData)
        {
            var objectDataAsJObject = (JObject) objectContainingUpdatedData;

            var propertyInfos = typeof (TTarget).GetProperties();

            // Guard against overposting: only allow only editable properties to be updated
            var modifiablePropertyInfos = propertyInfos.Where(x =>
            {
                var editableAttribute = x.GetCustomAttributes(typeof(EditableAttribute)).FirstOrDefault() as EditableAttribute;
                return editableAttribute != null && editableAttribute.AllowEdit;
            });

            // Get the names of properties represented in the Game fragment
            var nameOfSuppliedProperties = objectDataAsJObject.Properties().Select(x => x.Name);

            return modifiablePropertyInfos
                .Select(x => x.Name)
                .Where(x => nameOfSuppliedProperties.Contains(x, StringComparer.InvariantCultureIgnoreCase));
        }
    }
}
