using Application.AuthApp;
using Application.IRepos;
using Application.IServices;
using Application.Services;
using Application.UserSessionApp;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecoilDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RecoilDb")
    ));

// Configuration
var signingKey = builder.Configuration["Jwt:SigningKey"]
    ?? throw new InvalidOperationException("JWT Signing Key is missing.");

var keyBytes = Convert.FromBase64String(signingKey);

// Controllers & OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//Repos and services

builder.Services.AddScoped<IUserCrudService, UserCrudService>();
builder.Services.AddScoped<IUserGetService, UserGetService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();

builder.Services.AddScoped<IEmailValidationService, EmailValidationService>();
builder.Services.AddScoped<IPasswordValidationService, PasswordValidationService>();
builder.Services.AddScoped<IValidationManagerService, ValidationManagerService>();

builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IUserCrudRepository, UserCrudRepository>();
builder.Services.AddScoped<IUserGetRepository, UserGetRepository>();
builder.Services.AddScoped<IUserValidationRepository, UserValidationRepository>();

//Add Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:7286", "http://localhost:5030")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });



// Authorization
builder.Services.AddAuthorization();

// TODO: Dependency Injection for application services,
// repositories, UnitOfWork etc.

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();