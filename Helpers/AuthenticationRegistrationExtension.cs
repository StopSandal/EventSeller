using EventSeller.Services.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Service;
using System.Text;

namespace EventSeller.Helpers
{
    /// <summary>
    /// Provides extension methods for configuring authentication options.
    /// </summary>
    public static class AuthenticationRegistrationExtension
    {
        /// <summary>
        /// Sets the default authentication options to use JWT bearer authentication.
        /// </summary>
        /// <param name="options">The <see cref="AuthenticationOptions"/> to configure.</param>
        /// <returns>The configured <see cref="AuthenticationOptions"/>.</returns>
        public static AuthenticationOptions SetDefaultAuthenticationOptions(this AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            return options;
        }
        /// <summary>
        /// Sets the default JWT bearer options, including token validation parameters.
        /// </summary>
        /// <param name="options">The <see cref="JwtBearerOptions"/> to configure.</param>
        /// <param name="secretKey">The secret key used to sign the JWT tokens.</param>
        /// <returns>The configured <see cref="JwtBearerOptions"/>.</returns>
        public static JwtBearerOptions SetDefaultJwtBearerOptions(this JwtBearerOptions options, string secretKey)
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            return options;
        }
    }
}
