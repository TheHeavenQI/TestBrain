using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace BaseFramework
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ArrayExtension), "IEnumerable is null!");
            }
            else
            {
                foreach (T item in self)
                {
                    action(item);
                }
            }

            return self;
        }

        public static IEnumerable<T> Copy<T>(this IEnumerable<T> self)
        {
            IList<T> result = new List<T>();
            if (self.IsNotNull())
            {
                self.ForEach((it) => result.Add(it));
            }
            return result;
        }
    }
}
