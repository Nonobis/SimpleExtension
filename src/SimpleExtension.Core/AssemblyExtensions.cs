using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Assembly Extensions Methods
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Deserializes the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns></returns>
        public static T DeserializeJson<T>(this Assembly assembly, string resourcePath)
        {
            if (string.IsNullOrEmpty(resourcePath))
            {
                return default;
            }

            using (Stream streamData = assembly.GetEmbeddedFile(resourcePath))
            {
                if (streamData?.Length > 0)
                {
                    using StreamReader _textStreamReader = new(streamData);
                    string json = _textStreamReader?.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        return JsonSerializer.Deserialize<T>(json);
                    }
                }
            }
            return default;
        }

        /// <summary>
        /// Gets the embedded file.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>Stream.</returns>
        public static Stream GetEmbeddedFile(this Assembly assembly, string resourcePath)
        {
            System.Collections.Generic.List<string> filesInAssembly = assembly?.GetManifestResourceNames()?.ToList();
            if (filesInAssembly?.Contains(resourcePath) == true)
            {
                return assembly.GetManifestResourceStream(resourcePath);
            }
            return null;
        }
    }
}
