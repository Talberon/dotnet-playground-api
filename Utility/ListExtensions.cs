using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace sol_standard_api.Utility
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty(this ICollection me) => me.Count == 0;
        public static bool IsNotEmpty(this ICollection me) => !IsEmpty(me);

        public static Stack<T> Clone<T>(this Stack<T> original)
        {
            var arr = new T[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<T>(arr);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, null);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey>? comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }

                TSource min = sourceIterator.Current;
                TKey minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    TSource candidate = sourceIterator.Current;
                    TKey candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }

                return min;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns>The item with the highest value of the predicate. Null if collection is empty.</returns>
        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector) where TSource : class
        {
            return source.MaxBy(selector, null);
        }

        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey>? comparer) where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    return null;
                }

                TSource max = sourceIterator.Current;
                TKey maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    TSource candidate = sourceIterator.Current;
                    TKey candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }

                return max;
            }
        }

        public static IList<T> Shuffle<T>(this IList<T> list, Random random)
        {
            int count = list.Count;
            while (count > 1)
            {
                --count;
                int index = random.Next(count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }

            return list;
        }

        /// <summary>
        /// Move first item to the end, making the second item the new first item.
        /// </summary>
        /// <param name="me"></param>
        /// <typeparam name="T"></typeparam>
        public static void RotateForward<T>(this List<T> me)
        {
            if (me.IsEmpty() || me.Count == 1) return;

            me.Add(me.First());
            me.RemoveAt(0);
        }

        /// <summary>
        /// Move last item to the top, making the last item the new first item.
        /// </summary>
        /// <param name="me"></param>
        /// <typeparam name="T"></typeparam>
        public static void RotateBackward<T>(this List<T> me)
        {
            if (me.IsEmpty() || me.Count == 1) return;

            me.Insert(0, me.Last());
            me.RemoveAt(me.Count - 1);
        }
    }
}