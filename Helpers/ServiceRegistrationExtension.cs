﻿using EventSeller.Services;
using EventSeller.Services.Helpers;
using EventSeller.Services.Interfaces;
using EventSeller.Services.Interfaces.Exporters;
using EventSeller.Services.Interfaces.Services;
using EventSeller.Services.Service;

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
            services.AddScoped<IExternalPaymentService, ExternalPaymentService>();
            services.AddScoped<ITicketSellerService, TicketSellerService>();
            services.AddScoped<IEventSessionService, EventSessionService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IDayTrafficStatisticService, DayTrafficStatisticService>();
            services.AddScoped<ITicketSalesStatisticService, TicketSalesStatisticService>();
            services.AddScoped<ISeatsPopularityService, SeatsPopularityService>();
            services.AddScoped<ISectorsStatisticsService, SectorsStatisticsService>();
            services.AddScoped<IEventPopularityService, EventPopularityService>();
            services.AddScoped<ICsvFileExport, CsvFileExporter>();
            services.AddScoped<IExcelFileExport, ExcelFileExporter>();
            services.AddScoped<IResultExportService, ResultExportService>();
            services.AddScoped<ITicketRegistrationService, TicketRegistrationService>();

            services.AddSingleton(typeof(ITimerManager<>), typeof(TimerManager<>));

            services.AddHttpClient<ExternalPaymentService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTFactory, JWTFactory>();

            return services;
        }
    }
}
