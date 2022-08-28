using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Url Extensions Methods
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Get server certificate as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A Task&lt;X509Certificate2&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public static async Task<X509Certificate2> GetServerCertificateAsync(this string url)
        {
            X509Certificate2 certificate = null;
            var httpClientHandler = new HttpClientHandler
            {
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
                ServerCertificateCustomValidationCallback = (_, cert, __, ___) =>
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections
                {
                    certificate = new X509Certificate2(cert.GetRawCertData());
                    return true;
                }
            };

            HttpClient httpClient = new(httpClientHandler);
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            return certificate ?? throw new NullReferenceException();
        }

        /// <summary>
        /// Get server certificate public key as an asynchronous operation.
        /// </summary>
        /// <param name="urlBaseApi">The URL base API.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public static async Task<string> GetServerCertificatePublicKeyAsync(this string urlBaseApi)
        {
            var localCErtificate = await GetServerCertificateAsync(urlBaseApi);
            return localCErtificate?.GetPublicKeyString();
        }
    }
}
