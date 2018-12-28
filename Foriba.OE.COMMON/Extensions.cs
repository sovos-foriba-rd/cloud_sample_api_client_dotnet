using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class Extensions
    {
        public static List<List<T>> Split<T>(this List<T> source, int size)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList()).ToList();
        }
    }


}
