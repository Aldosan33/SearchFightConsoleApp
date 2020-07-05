using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SearchFight.Common.Extensions
{
    public static class Extensions
    {
        public static T MaxValue<T>(this IEnumerable<T> source, Func<T, long> func)
        {
            if (source == null)
                throw new ArgumentNullException();

            using (var en = source.GetEnumerator())
            {
                if (!en.MoveNext())
                    throw new ArgumentException();

                long max = func(en.Current);
                T maxValue = en.Current;

                while (en.MoveNext())
                {
                    var possible = func(en.Current);

                    if (max < possible)
                    {
                        max = possible;
                        maxValue = en.Current;
                    }
                }
                return maxValue;
            }
        }

        public static T DeserializeJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}