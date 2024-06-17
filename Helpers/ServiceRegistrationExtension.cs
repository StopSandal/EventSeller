using EventSeller.Services.Helpers;
using EventSeller.Services.Interfaces;
using EventSeller.Services.Interfaces.Services;
using EventSeller.Services.Service;
using Services;
using Services.Service;

namespace EventSeller.Helpers
{
    /// <summary>
    /// Provides extension methods for registering services with the dependency injection container.
    /// </summary>
    public static class ServiceRegistrationExtension
    {
        /// <summary>
        /// Registers the application's services with the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IHallSectorService, HallSectorService>();
            services.AddScoped<IPlaceAddressService, PlaceAddressService>();
            services.AddScoped<IPlaceHallService, PlaceHallService>();
            services.AddScoped<ITicketSeatService, TicketSeatService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTFactory, JWTFactory>();

            return services;
        }
    }
}
