using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleExtension
{
    public static class LinqExtension
    {
        /// <summary>
        ///     Randoms the element.
        /// </summary>
        public static T RandomElement<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            var r = new Random();
            q = q.Where(e);
            return q.Skip(r.Next(q.Count())).FirstOrDefault();
        }

        /// <summary>
        ///     Returns a random element from a pList, or null if the pList is empty.
        /// </summary>
        public static T Random<T>(this IEnumerable<T> pList, Random rand)
        {
            if ((pList != null) && pList.Any())
                return pList.ElementAt(rand.Next(pList.Count()));
            return default(T);
        }

        /// <summary>
        ///     Returns a shuffled IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of object being enumerated</typeparam>
        /// <returns>A shuffled shallow copy of the source items</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        /// <summary>
        ///     Returns a shuffled IEnumerable.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand)
        {
            var pList = source.ToList();
            pList.Shuffle(rand);
            return pList;
        }

        /// <summary>
        ///     Shuffles an IList in place.
        /// </summary>
        public static void Shuffle<T>(this IList<T> pList)
        {
            pList.Shuffle(new Random());
        }

        /// <summary>
        ///     Shuffles an IList in place.
        /// </summary>
        public static void Shuffle<T>(this IList<T> pList, Random rand)
        {
            var count = pList.Count;
            while (count > 1)
            {
                var i = rand.Next(count--);
                var temp = pList[count];
                pList[count] = pList[i];
                pList[i] = temp;
            }
        }

        /// <summary>
        ///     Chunkses the specified chunk size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> enumerable,
            int chunkSize)
        {
            if (chunkSize < 1) throw new ArgumentException("chunkSize must be positive");

            using (var e = enumerable.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var remaining = chunkSize; // elements remaining in the current chunk
                    var innerMoveNext = new Func<bool>(() => (--remaining > 0) && e.MoveNext());

                    yield return e.GetChunk(innerMoveNext);
                    while (innerMoveNext())
                    {
/* discard elements skipped by inner iterator */
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the chunk.
        /// </summary>
        private static IEnumerable<T> GetChunk<T>(this IEnumerator<T> e,
            Func<bool> innerMoveNext)
        {
            do
            {
                yield return e.Current;
            } while (innerMoveNext());
        }
    }
}