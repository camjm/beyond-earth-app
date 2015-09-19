using System.ComponentModel;

namespace BeyondEarthApp.Common.Extensions
{
    public static class StringExtensions
    {
        public static T Parse<T>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof (T));

            var result = converter.ConvertFromString(value);

            return (T) result;
        }
    }
}
