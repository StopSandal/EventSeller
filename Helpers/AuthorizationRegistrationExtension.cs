using EventSeller.Helpers.Constants;
using Microsoft.AspNetCore.Authorization;

namespace EventSeller.Helpers
{
    /// <summary>
    /// Extension for Building properties at authorization <c> builder.Services.AddAuthorization();</c>
    /// </summary>
    public static class AuthorizationRegistrationExtension
    {
        /// <summary>
        /// Register polices for Authorization
        /// </summary>
        /// <param name="options">Instance of Authorization options</param>
        public static void AddAuthorizationPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(PoliciesConstants.SuperAdminOnlyPolicy, policy =>
                policy.RequireRole(RolesConstants.SuperAdminRole));

            options.AddPolicy(PoliciesConstants.AdminOnlyPolicy, policy =>
                policy.RequireRole(
                    RolesConstants.AdminRole
                    , RolesConstants.SuperAdminRole));

            options.AddPolicy(PoliciesConstants.EventManagerOrAdminPolicy, policy =>
                policy.RequireRole(
                    RolesConstants.EventManagerRole
                    , RolesConstants.AdminRole
                    , RolesConstants.SuperAdminRole));

            options.AddPolicy(PoliciesConstants.VenueManagerOrAdminPolicy, policy =>
                policy.RequireRole(
                    RolesConstants.VenueManagerRole
                    , RolesConstants.AdminRole
                    , RolesConstants.SuperAdminRole));
        }
    }
}