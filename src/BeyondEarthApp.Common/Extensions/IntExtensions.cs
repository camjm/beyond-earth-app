using System;

namespace BeyondEarthApp.Common.Extensions
{
    public static class IntExtensions
    {
        public static int GetBoundedValue(this int value, int min, int max)
        {
            var boundedValue = Math.Min(Math.Max(value, min), max);

            return boundedValue;
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min)
        {
            var valueToBound = value ?? defaultValue;

            var boundedValue = Math.Max(valueToBound, min);

            return boundedValue;
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min, int max)
        {
            var valueToBound = value ?? defaultValue;

            var boundedValue = GetBoundedValue(valueToBound, min, max);

            return boundedValue;
        }
    }
}
