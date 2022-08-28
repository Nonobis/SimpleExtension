using Microsoft.AspNetCore.Http;

namespace SimpleExtension.Core
{
    /// <summary>
    /// HttpRequest Extensions Methods
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetBaseUrl(this HttpRequest request)
        {
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
