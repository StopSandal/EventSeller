using EventSeller.Services.Helpers;
using EventSeller.Services.Interfaces;
using EventSeller.Services.Service;
using Services;
using Services.Service;

namespace EventSeller.Helpers
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IHallSectorService, HallSectorService>();
            services.AddScoped<IPlaceAddressService, PlaceAddressService>();
            services.AddScoped<IPlaceHallService, PlaceHallService>();
            services.AddScoped<ITicketSeatService, TicketSeatService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTFactory, JWTFactory>();

            return services;
        }
    }
}
