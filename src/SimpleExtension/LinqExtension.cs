using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleExtension
{
    public static class LinqExtension
    {
        /// <summary>
        /// Yields the one default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> YieldOneDefault<T>(this IEnumerable<T> values)
        {
            yield return default(T);
            foreach (var item in values)
                yield return item;
        }

        /// <summary>
        /// Randoms the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q">The q.</param>
        /// <param name="e">The e.</param>
        /// <returns>T.</returns>
        public static T RandomElement<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            var r = new Random();
            q = q.Where(e);
            return q.Skip(r.Next(q.Count())).FirstOrDefault();
        }

        /// <summary>
        /// Randoms the specified p random seed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        /// <returns>T.</returns>
        public static T Random<T>(this IEnumerable<T> pList, Random pRandomSeed)
        {
            if ((pList != null) && pList.Any())
                return pList.ElementAt(pRandomSeed.Next(pList.Count()));
            return default(T);
        }

        /// <summary>
        /// Shuffles the specified p list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> pList)
        {
            return pList.Shuffle(new Random());
        }

        /// <summary>
        /// Shuffles the specified p random seed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random pRandomSeed)
        {
            var pList = source.ToList();
            pList.Shuffle(pRandomSeed);
            return pList;
        }

        /// <summary>
        /// Shuffles the specified p list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        public static void Shuffle<T>(this IList<T> pList)
        {
            pList.Shuffle(new Random());
        }

        /// <summary>
        /// Shuffles the specified p random seed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        public static void Shuffle<T>(this IList<T> pList, Random pRandomSeed)
        {
            var count = pList.Count;
            while (count > 1)
            {
                var i = pRandomSeed.Next(count--);
                var temp = pList[count];
                pList[count] = pList[i];
                pList[i] = temp;
            }
        }

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="pList">The p list.</param>
        /// <param name="pValue">The p value.</param>
        /// <param name="pOrdinal">The p ordinal.</param>
        /// <returns><c>true</c> if [contains] [the specified p value]; otherwise, <c>false</c>.</returns>
        public static bool Contains(this List<string> pList, string pValue, StringComparison pOrdinal)
        {
            foreach (var pItem in pList)
            {
                if (pItem != null && pItem.Contains(pValue, pOrdinal))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Chunkses the specified chunk size.
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="values">The values.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;TValue&gt;&gt;.</returns>
        public static IEnumerable<IEnumerable<TValue>> Chunks<TValue>(this IEnumerable<TValue> values, int chunkSize)
        {
            return values.Select((v, i) => new { v, groupIndex = i / chunkSize })
                   .GroupBy(x => x.groupIndex)
                   .Select(g => g.Select(x => x.v));
        }
        
    }
}