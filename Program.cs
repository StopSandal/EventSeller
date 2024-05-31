using DataLayer.Model;
using DataLayer.Model.EF;
using EventSeller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Service;
using System.Drawing.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<SellerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SellerContextConnection")));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
RegisterServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IEventService, EventService>();
    services.AddScoped<IHallSectorService, HallSectorService>();
    services.AddScoped<IPlaceAddressService, PlaceAddressService>();
    services.AddScoped<IPlaceHallService, PlaceHallService>();
    services.AddScoped<ITicketSeatService, TicketSeatService>();
    services.AddScoped<ITicketService, TicketService>();
}