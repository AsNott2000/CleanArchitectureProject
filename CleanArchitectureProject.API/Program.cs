using CleanArchitectureProject.Application.Features.Cars.Handlers;
using CleanArchitectureProject.Application.Features.Users.Handlers;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Domain.Services;
using CleanArchitectureProject.Infrastructure.Presistence;
using CleanArchitectureProject.Infrastructure.Repositories;
using CleanArchitectureProject.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;


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

app.MapControllers();
app.Run();
