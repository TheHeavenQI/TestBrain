using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseFramework
{
    public static class ListExtension
    {
        public static bool Any<T>(this IList<T> self)
        {
            return IsNotNullAndEmpty(self);
        }

        public static bool IsNullOrEmpty<T>(this IList<T> self)
        {
            return self.IsNull() || self.Count == 0;
        }

        public static bool IsNotNullAndEmpty<T>(this IList<T> self)
        {
            return !self.IsNullOrEmpty();
        }

        public static IList<T> Copy<T>(this IList<T> self)
        {
            IList<T> result = new List<T>();
            if (self.IsNotNullAndEmpty())
            {
                self.ForEach((it) => result.Add(it));
            }
            return result;
        }

        public static IList<T> ForEach<T>(this IList<T> self, Action<int, T> action)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ListExtension), "List is null");
            }
            else
            {
                for (int i = 0; i < self.Count; ++i)
                    action(i, self[i]);
            }

            return self;
        }

        public static IList<T> ForEachReverse<T>(this IList<T> self, Action<T> action)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ListExtension), "List is null");
            }
            else
            {
                for (int i = self.Count - 1; i >= 0; --i)
                    action(self[i]);
            }

            return self;
        }

        public static IList<T> ForEachReverse<T>(this IList<T> self, Action<int, T> action)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ListExtension), "List is null");
            }
            else
            {
                for (int i = self.Count - 1; i >= 0; --i)
                    action(i, self[i]);
            }

            return self;
        }

        /// <summary>
        /// return default(T), if list is empty or null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this IList<T> self)
        {
            if (self.IsNullOrEmpty())
            {
                Log.W(typeof(ListExtension), "List is null or empty");
                return default(T);
            }

            return self[UnityEngine.Random.Range(0, self.Count)];
        }

        public static T GetRandomItem<T>(this IList<T> self, Func<T, bool> valid)
        {
            if (valid == null)
            {
                return self.GetRandomItem();
            }

            if (self.IsNullOrEmpty())
            {
                Log.W(typeof(ListExtension), "List is null or empty");
                return default(T);
            }

            List<T> validGroup = new List<T>();
            foreach(T t in self)
            {
                if (valid.Invoke(t))
                {
                    validGroup.Add(t);
                }
            }
            if (validGroup.Count == 0)
            {
                Log.W(typeof(ListExtension), "List valid item or empty");
                return default(T);
            }
            return validGroup.GetRandomItem();
        }

        public static IEnumerable<int> GetRandomItemIndex<T>(this IList<T> self, int count, Func<T, bool> isUsed = null)
        {
            HashSet<int> result = new HashSet<int>();
            
            if (self.IsNullOrEmpty() || count < 1)
            {
                return result;
            }
            int allUsedCount = self.Count((t) => isUsed == null || isUsed.Invoke(t));
            count = UnityEngine.Mathf.Min(count, allUsedCount);
            while (count > 0)
            {
                int index = UnityEngine.Random.Range(0, self.Count);
                if (result.Contains(index) || isUsed != null && !isUsed.Invoke(self[index]))
                {
                    continue;
                }
                result.Add(index);
                --count;
            }
            return result;
        }

        public static T RemoveFirst<T>(this IList<T> self)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ListExtension), "List is null");
                return default(T);
            }
            if (self.Count == 0)
            {
                Log.W(typeof(ListExtension), "List is empty");
                return default(T);
            }

            T item = self[0];
            self.RemoveAt(0);
            return item;
        }

        public static T RemoveLast<T>(this IList<T> self)
        {
            if (self.IsNull())
            {
                Log.W(typeof(ListExtension), "List is null");
                return default(T);
            }
            if (self.Count == 0)
            {
                Log.W(typeof(ListExtension), "List is empty");
                return default(T);
            }

            T item = self[self.Count - 1];
            self.RemoveAt(self.Count - 1);
            return item;
        }

        public static void Shuffle<T>(this IList<T> self, int count = -1)
        {
            if (count == -1)
                count = self.Count;
            Random random = new Random();
            while (count > 0)
            {
                int one = random.Next(0, self.Count);
                int two = random.Next(0, self.Count);
                self.Swap(one, two);
                count--;
            }
        }

        public static void KeepItemInIndex<T>(this IList<T> self, T keepItem, int keepIndex) where T : IEquatable<T>
        {
            if (self.IsNotNullAndEmpty())
            {
                if (keepItem != null && keepIndex >= 0 && keepIndex < self.Count)
                {
                    int index = self.IndexOf(keepItem);
                    if (index < 0)
                    {
                        Log.W(typeof(ListExtension), $"KeepItemInIndex, this item:[{keepIndex}] not in IList");
                    }
                    if (index >= 0 && !self[keepIndex].Equals(keepItem))
                    {
                        self.Swap(index, keepIndex);
                    }
                }
            }
        }

        public static IList<T> Swap<T>(this IList<T> self, int x, int y)
        {
            if (self.IsNotNullAndEmpty() && x >= 0 && x < self.Count && y >= 0 && y < self.Count)
            {
                T temp = self[x];
                self[x] = self[y];
                self[y] = temp;
            }
            return self;
        }
    }
}
