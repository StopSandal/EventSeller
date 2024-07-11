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
            options.AddPolicy("SuperAdminOnly", policy =>
                policy.RequireRole("Super admin"));
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin", "Super admin"));
            options.AddPolicy("EventManagerOrAdmin", policy =>
                policy.RequireRole("Event manager", "Admin", "Super admin"));
            options.AddPolicy("VenueManagerOrAdmin", policy =>
                policy.RequireRole("Venue manager", "Admin", "Super admin"));
        }
    }
}