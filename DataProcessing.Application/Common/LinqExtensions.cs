using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.Common
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 0);
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 1);
        }

        public static IEnumerable<TSource> Except<TSource, TThat, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TThat> second,
            Func<TSource, TKey> firstKey,
            Func<TThat, TKey> secondKey)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            var set = new HashSet<TKey>();
            foreach (var element in second)
            {
                set.Add(secondKey(element));
            }

            foreach (var element in first)
            {
                if (set.Add(firstKey(element)))
                {
                    set.Remove(firstKey(element));
                    yield return element;
                }
            }
        }

        public static IEnumerable<TSource> Intersect<TSource, TThat, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TThat> second,
            Func<TSource, TKey> firstKey,
            Func<TThat, TKey> secondKey)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            var set = new HashSet<TKey>();
            foreach (var element in second)
            {
                set.Add(secondKey(element));
            }

            foreach (var element in first)
            {
                if (!set.Add(firstKey(element)))
                {
                    set.Remove(firstKey(element));
                    yield return element;
                }
            }
        }
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> collection, int batchSize)
        {
            var nextbatch = new List<T>(batchSize);
            foreach (T item in collection)
            {
                nextbatch.Add(item);
                if (nextbatch.Count == batchSize)
                {
                    yield return nextbatch;
                    nextbatch = new List<T>(batchSize);
                }
            }

            if (nextbatch.Count > 0)
            {
                yield return nextbatch;
            }
        }
    }
}
