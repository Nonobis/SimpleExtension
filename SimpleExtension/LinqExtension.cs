using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleExtension
{
    public static class LinqExtension
    {
        /// <summary>
        /// Randoms the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q">The q.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static T RandomElement<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            var r = new Random();
            q = q.Where(e);
            return q.Skip(r.Next(q.Count())).FirstOrDefault();
        }

        /// <summary>
        /// Returns a random element from a list, or null if the list is empty.
        /// </summary>
        /// <typeparam name="T">The type of object being enumerated</typeparam>
        /// <param name="rand">An instance of a random number generator</param>
        /// <returns>A random element from a list, or null if the list is empty</returns>
        public static T Random<T>(this IEnumerable<T> list, Random rand)
        {
            if (list != null && list.Count() > 0)
                return list.ElementAt(rand.Next(list.Count()));
            return default(T);
        }

        /// <summary>
        /// Returns a shuffled IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of object being enumerated</typeparam>
        /// <returns>A shuffled shallow copy of the source items</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        /// <summary>
        /// Returns a shuffled IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of object being enumerated</typeparam>
        /// <param name="rand">An instance of a random number generator</param>
        /// <returns>A shuffled shallow copy of the source items</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand)
        {
            var list = source.ToList();
            list.Shuffle(rand);
            return list;
        }

        /// <summary>
        /// Shuffles an IList in place.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list</typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            list.Shuffle(new Random());
        }

        /// <summary>
        /// Shuffles an IList in place.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list</typeparam>
        /// <param name="rand">An instance of a random number generator</param>
        public static void Shuffle<T>(this IList<T> list, Random rand)
        {
            int count = list.Count;
            while (count > 1)
            {
                int i = rand.Next(count--);
                T temp = list[count];
                list[count] = list[i];
                list[i] = temp;
            }
        }
    }
}
