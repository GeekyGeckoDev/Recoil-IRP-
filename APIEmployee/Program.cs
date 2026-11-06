using Infrastructure;
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
builder.Services.AddOpenApi();

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