using Business.Repositories;
using Business.Entities;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using UsuarioProject.Services;
using AutoMapper;
using Business;
using UsuarioProject.Settings;
using Business.Setting;
using System.Security.Cryptography;
using PopsyMarket.Business;
using UsuarioProject.Business;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Bearer settings
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7291";
        options.Audience = "api-resource"; // Reemplazar con el recurso API.
    });

// SwaggerGen settings
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Agregar esquema de seguridad para Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Token configuration settings
builder.Services.AddSingleton<TokenSettings>();
TokenSettings tokenSettings = builder.Services.BuildServiceProvider().GetRequiredService<TokenSettings>();
configuration.GetSection("TokenSettings").Bind(tokenSettings);

// Random key generation
byte[] randomKey = new byte[32];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(randomKey);
}
builder.Services.AddSingleton(randomKey);

builder.Services.AddDbContext<UsuarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsuarioContext")))
    .AddAutoMapper(typeof(AplicationServiceExtension))
    .AddScoped<CredencialesKeys>()
    .AddScoped<IJwtService, JwtService>()
    .AddScoped<IEncryptService, EncryptService>()
    .AddScoped<ILoginBusiness, LoginBusiness>()
    .AddSingleton(tokenSettings);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();

builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //Botón "Authorize" en el Swagger
        c.OAuthClientId("swagger");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
