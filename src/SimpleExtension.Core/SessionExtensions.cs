using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Https Session Extensions Methods
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Sets the specified session.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetObject<string>(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
