using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;

namespace SimpleExtension.Core
{
    /// <summary>
    /// HttpContext Extensions Methods
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the remote ip address.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="allowForwarded">if set to <c>true</c> [allow forwarded].</param>
        /// <returns></returns>
        /// <remarks>
        private static IPAddress GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
        {
            if (allowForwarded)
            {
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                {
                    return ip;
                }
            }
            return context.Connection.RemoteIpAddress;
        }

        /// <summary>
        /// Gets the client ip address.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        public static string GetClientIPAddress(this HttpContext context)
        {
            return context?.GetRemoteIPAddress()?.ToString().Replace("::ffff:", string.Empty);
        }
    }
}
