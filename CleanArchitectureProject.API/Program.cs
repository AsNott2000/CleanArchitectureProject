using System.Text;
using CleanArchitectureProject.Application.Features.Cars.Handlers;
using CleanArchitectureProject.Application.Features.Users.Handlers;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Domain.Services;
using CleanArchitectureProject.Infrastructure.Presistence;
using CleanArchitectureProject.Infrastructure.Repositories;
using CleanArchitectureProject.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Repository kayıtları
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICarsRepository, CarsRepository>();

// Domain Services
builder.Services.AddScoped<IPasswordHasher, SimplePasswordHasher>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

/* 🔐 AUTH: JWT Bearer */
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch API", Version = "v1" });

    // 🔐 JWT Bearer şeması
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description =
            "JWT token'ı 'Bearer {token}' formatında girin.\nÖrnek: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    // Tüm endpoint’lere varsayılan olarak uygula (Authorize butonu çıksın)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();
// Handlers
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<LoginUserHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<GetAllUsersHandler>();
builder.Services.AddScoped<CreateCarHandler>();
builder.Services.AddScoped<GetCarByIdHandler>();
builder.Services.AddScoped<GetAllCarsHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // 🔐
app.UseAuthorization();

app.MapControllers();
app.Run();
