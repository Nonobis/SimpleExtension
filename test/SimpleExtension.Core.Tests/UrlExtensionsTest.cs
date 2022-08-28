using System.Threading.Tasks;
using Xunit;

namespace SimpleExtension.Core.Tests
{
    /// <summary>
    /// Unit Test on Url Extensions
    /// </summary>
    public class UrlExtensionsTest
    {
        /// <summary>When get server certificate asynchronous is returned asynchronous.</summary>
        /// <param name="url">URL of the resource.</param>
        /// <returns>A Task.</returns>
        [Theory]
        [InlineData("https://www.google.fr")]
        public async Task WhenGetServerCertificateAsyncIsReturnedAsync(string url)
        {
            var DownloadedCertificate = await url.GetServerCertificateAsync();
            Assert.NotNull(DownloadedCertificate);
        }

        /// <summary>
        /// When get server certificate public key asynchronous is returned asynchronous.
        /// </summary>
        /// <param name="url">              URL of the resource.</param>
        /// <returns>A Task.</returns>
        [Theory]
        [InlineData("https://www.google.fr")]
        public async Task WhenGetServerCertificatePublicKeyAsyncIsReturnedAsync(string url)
        {
            var DownloadedCertificate = await url.GetServerCertificateAsync();
            Assert.NotNull(DownloadedCertificate);

            string? publicKey = await url.GetServerCertificatePublicKeyAsync();
            Assert.NotNull(publicKey);
            Assert.Equal(publicKey, DownloadedCertificate?.GetPublicKeyString());
        }
    }
}
