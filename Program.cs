using EventSeller.DataLayer.EF;
using EventSeller.DataLayer.Entities;
using EventSeller.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

const string JWT_SECRET = "JWT:Secret";
const string CONNECTION_STRING = "SellerContextConnection";
const string MIGRATION_ASSEMBLY = "EventSeller.DataLayer";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddDbContext<SellerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(CONNECTION_STRING)
    , x => x.MigrationsAssembly(MIGRATION_ASSEMBLY)));

builder.Services.RegisterServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.Password.RequireDigit = false; // Turn off digit requirement
            options.Password.RequiredLength = 4; // Set minimum password length
            options.Password.RequireNonAlphanumeric = false; // Turn off non-alphanumeric character requirement
            options.Password.RequireUppercase = false; // Turn off uppercase letter requirement
            options.Password.RequireLowercase = false; // Turn off lowercase letter requirement
        }
    })
    .AddEntityFrameworkStores<SellerContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
    options.AddAuthorizationPolicies()
    );

builder.Services.AddAuthentication(options =>
    options.SetDefaultAuthenticationOptions()
    ).AddJwtBearer(options =>
        options.SetDefaultJwtBearerOptions(builder.Configuration[JWT_SECRET])
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer [Your token here]\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
