using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Class ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the poco.
        /// </summary>
        /// <typeparam name="TConfig">The type of the t configuration.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="pocoProvider">The poco provider.</param>
        /// <returns>TConfig.</returns>
        /// <exception cref="ArgumentNullException">
        /// services
        /// or
        /// configuration
        /// or
        /// pocoProvider
        /// </exception>
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration, Func<TConfig> pocoProvider) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (pocoProvider == null) throw new ArgumentNullException(nameof(pocoProvider));

            var config = pocoProvider();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

        /// <summary>
        /// Configures the poco.
        /// </summary>
        /// <typeparam name="TConfig">The type of the t configuration.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="config">The configuration.</param>
        /// <returns>TConfig.</returns>
        /// <exception cref="ArgumenNullException">
        /// services
        /// or
        /// configuration
        /// or
        /// config
        /// </exception>
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration, TConfig config) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (config == null) throw new ArgumentNullException(nameof(config));

            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
