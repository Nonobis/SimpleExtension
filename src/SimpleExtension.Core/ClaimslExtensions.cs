using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SimpleExtension.Core
{
    /// <summary>
    /// Claims Extensions Method
    /// </summary>
    public static class ClaimslExtensions
    {
        /// <summary>
        /// Determines whether [is user logged].
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns><c>true</c> if [is user logged] [the specified claims principal]; otherwise, <c>false</c>.</returns>
        public static bool IsUserLogged(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Identity?.IsAuthenticated == true;
        }

        /// <summary>
        /// Determines whether [is user in role] [the specified role].
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="role">The role.</param>
        /// <returns><c>true</c> if [is user in role] [the specified role]; otherwise, <c>false</c>.</returns>
        public static bool IsUserInRole(this ClaimsPrincipal claimsPrincipal, string role)
        {
            return claimsPrincipal.IsUserLogged()
                && claimsPrincipal.ClaimExists("Role")
                && claimsPrincipal.ClaimsIsExpectedValue("Role", role);
        }

        /// <summary>
        /// Claims the exists.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ClaimExists(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                return false;
            }

            Claim claim = principal.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim != null;
        }

        /// <summary>
        /// Claimses the is expected value.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <param name="claimValue">The claim value.</param>
        /// <param name="issuer">The issuer.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool ClaimsIsExpectedValue(this ClaimsPrincipal principal, string claimType,
                                    string claimValue, string issuer = null)
        {
            if (principal == null)
            {
                return false;
            }

            Claim claim = principal.Claims.FirstOrDefault(x => x.Type == claimType
                                                 && x.Value == claimValue
                                                 && (issuer == null || x.Issuer == issuer));
            return claim != null;
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimName">Name of the claim.</param>
        /// <returns>System.String.</returns>
        public static string GetClaimValue(this ClaimsPrincipal principal, string claimName)
        {
            Claim currentClaim = principal.Claims.FirstOrDefault(c => c.Type == claimName);
            return currentClaim != null ? (currentClaim?.Value) : "";
        }
    }
}
