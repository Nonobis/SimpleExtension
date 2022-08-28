using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Class LinqExtension.
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// Appends the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] item) => source.Concat(item);

        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ToDataTable<T>(this ICollection<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Yields the one default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns>
        /// IEnumerable&lt;T&gt;.
        /// </returns>
        public static IEnumerable<T> YieldOneDefault<T>(this IEnumerable<T> values)
        {
            yield return default;
            foreach (T item in values)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Randoms the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q">The q.</param>
        /// <param name="e">The e.</param>
        /// <returns>
        /// T.
        /// </returns>
        public static T RandomElement<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            Random r = new();
            q = (IQueryable<T>)q.Where(e as Func<T, bool>);
            return q.Skip(r.Next(q.Count())).FirstOrDefault();
        }

        /// <summary>
        /// Returns a random element from a pList, or null if the pList is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        /// <returns>
        /// T.
        /// </returns>
        public static T Random<T>(this IEnumerable<T> pList, Random pRandomSeed)
        {
            if ((pList != null) && pList.Any())
            {
                return pList.ElementAt(pRandomSeed.Next(pList.Count()));
            }

            return default;
        }

        /// <summary>
        /// Returns a shuffled IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of object being enumerated</typeparam>
        /// <param name="pList">The p list.</param>
        /// <returns>
        /// A shuffled shallow copy of the source items
        /// </returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> pList) => pList.Shuffle(new Random());

        /// <summary>
        /// Returns a shuffled IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        /// <returns>
        /// IEnumerable&lt;T&gt;.
        /// </returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random pRandomSeed)
        {
            List<T> pList = source.ToList();
            pList.Shuffle(pRandomSeed);
            return pList;
        }

        /// <summary>
        /// Shuffles an IList in place.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        public static void Shuffle<T>(this IList<T> pList) => pList.Shuffle(new Random());

        /// <summary>
        /// Shuffles an IList in place.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList">The p list.</param>
        /// <param name="pRandomSeed">The p random seed.</param>
        public static void Shuffle<T>(this IList<T> pList, Random pRandomSeed)
        {
            int count = pList.Count;
            while (count > 1)
            {
                int i = pRandomSeed.Next(count--);
                T temp = pList[count];
                pList[count] = pList[i];
                pList[i] = temp;
            }
        }

        /// <summary>
        /// Retourne des list de X elements
        /// </summary>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="values">The values.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns>
        /// IEnumerable&lt;IEnumerable&lt;TValue&gt;&gt;.
        /// </returns>
        public static IEnumerable<IEnumerable<TValue>> Chunks<TValue>(this IEnumerable<TValue> values, int chunkSize) => values.Select((v, i) => new { v, groupIndex = i / chunkSize })
                   .GroupBy(x => x.groupIndex)
                   .Select(g => g.Select(x => x.v));
    }
}