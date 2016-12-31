using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExtension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString<T>(this T instance) where T : class
        {

            if (instance == null)
                return string.Empty;

            var strListType = typeof(List<string>);
            var strArrType = typeof(string[]);

            var arrayTypes = new[] { strListType, strArrType };
            var handledTypes = new[] { typeof(Int32), typeof(String), typeof(bool), typeof(DateTime), typeof(double), typeof(decimal), strListType, strArrType };

            var validProperties = instance.GetType()
                                          .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                          .Where(prop => handledTypes.Contains(prop.PropertyType))
                                          .Where(prop => prop.GetValue(instance, null) != null)
                                          .ToList();

            var format = string.Format("{{0,-{0}}} : {{1}}", validProperties.Max(prp => prp.Name.Length));

            return string.Join(
                     Environment.NewLine,
                     validProperties.Select(prop => string.Format(format,
                                                                  prop.Name,
                                                                  (arrayTypes.Contains(prop.PropertyType) ? string.Join(", ", (IEnumerable<string>)prop.GetValue(instance, null))
                                                                                                          : prop.GetValue(instance, null)))));
        }
    }
}
