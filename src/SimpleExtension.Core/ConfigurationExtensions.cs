using Microsoft.Extensions.Configuration;
using SimpleExtension.Core.Exceptions;
using System;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Extensions Methods for IOptions
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            IConfigurationSection section = configuration.GetConfigurationSection(sectionName);
            T options = new();
            section.Bind(options);
            return options;
        }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// configuration
        /// or
        /// sectionName
        /// </exception>
        /// <exception cref="MissingConfigurationException">Section de configuration '{sectionName}' manquante.</exception>
        public static IConfigurationSection GetConfigurationSection(this IConfiguration configuration, string sectionName)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new ArgumentNullException(nameof(sectionName));
            }

            return configuration.GetSection(sectionName)
                ?? throw new MissingConfigurationException($"Missing configuration section '{sectionName}'.");
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T Get<T>(this IConfiguration config, string key) where T : new()
        {
            T instance = new();
            config.GetSection(key).Bind(instance);
            return instance;
        }
    }
}
